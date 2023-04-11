using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Interfaces.Repositories;

namespace TableTracker.Application.CQRS.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDTO>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(
            IUnitOfWork<long> unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork
                .GetRepository<IUserRepository>()
                .GetUserByEmail(request.Email);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
