using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;
using TableTracker.Infrastructure.Identity;
using TableTracker.JwtFeatures;

namespace TableTracker.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<TableTrackerIdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IEmailHandler _emailHandler;

        public AccountsController(
            UserManager<TableTrackerIdentityUser> userManager,
            IMapper mapper,
            JwtHandler jwtHandler,
            IUnitOfWork<long> unitOfWork,
            IEmailHandler emailHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _unitOfWork = unitOfWork;
            _emailHandler = emailHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDTO userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                return Unauthorized(new AuthResponseDTO { ErrorMessage = "Invalid Authentication" });
            }

            var userDTO = _mapper.Map<UserDTO>(await _unitOfWork
                .GetRepository<IUserRepository>()
                .GetUserByEmail(user.Email));

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user, userDTO);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDTO
            {
                IsAuthSuccessful = true,
                Token = token,
                User = _mapper.Map<VisitorDTO>((await _unitOfWork
                    .GetRepository<IVisitorRepository>()
                    .FilterVisitors(userForAuthentication.Email)).FirstOrDefault())
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserForSignupDTO user)
        {
            var identityUser = new TableTrackerIdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await _unitOfWork.GetRepository<IVisitorRepository>().Insert(new Visitor
                {
                    Email = user.Email,
                    FullName = user.FirstName + " " + user.LastName,
                });

                await _unitOfWork.Save();
            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost("reset-password/email")]
        public async Task<IActionResult> ResetPasswordEmail([FromBody] ForgotPasswordDTO forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", forgotPasswordDto.Email }
                };

                var queries = new QueryBuilder(param).ToQueryString();

                string body =
                @$"
                    <!DOCTYPE html>
                    <html>
                        <body>
                    
                        <p>To reset your password, follow this <a href='{forgotPasswordDto.ClientURI + queries}'>link.</a></p>
                    
                        </body>
                    </html>
                ";

                await _emailHandler.SendEmail(new EmailDTO
                {
                    To = new[] { forgotPasswordDto.Email },
                    Body = body,
                    Subject = "Password recovery",
                }, true);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (resetPasswordDto.Password != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest("Invalid Request");
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user is null)
            {
                return BadRequest("Invalid Request");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok();
        }
    }
}
