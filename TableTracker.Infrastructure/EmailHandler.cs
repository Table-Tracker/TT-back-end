using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;
using TableTracker.Domain.Settings;

namespace TableTracker.Infrastructure
{
    public class EmailHandler : IEmailHandler
    {
        private readonly EmailConfig _emailConfig;

        public EmailHandler(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmail(EmailDTO email, bool html = false)
        {
            using var mailMessage = new MailMessage
            {
                Subject = email.Subject,
                Body = email.Body,
                From = new MailAddress(_emailConfig.Address),
                IsBodyHtml = html,
            };

            if (email.Cc is not null && email.Cc.Any())
            {
                mailMessage.CC.Add(email.Cc.Aggregate((x, y) => x + "," + y));
            }

            if (email.Bcc is not null && email.Bcc.Any())
            {
                mailMessage.Bcc.Add(email.Bcc.Aggregate((x, y) => x + "," + y));
            }

            if (email.To is not null && email.To.Any())
            {
                mailMessage.To.Add(email.To.Aggregate((x, y) => x + "," + y));
            }

            var smtp = new SmtpClient(_emailConfig.Host, _emailConfig.Port)
            {
                Credentials = new NetworkCredential()
                {
                    UserName = _emailConfig.Address,
                    Password = _emailConfig.Password,
                },

                EnableSsl = true,
            };

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
