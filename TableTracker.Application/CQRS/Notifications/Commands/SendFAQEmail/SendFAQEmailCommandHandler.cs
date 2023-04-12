using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Interfaces;

namespace TableTracker.Application.CQRS.Notifications.Commands.SendFAQEmail
{
    public class SendFAQEmailCommandHandler : IRequestHandler<SendFAQEmailCommand, CommandResponse<EmailDTO>>
    {
        private readonly IEmailHandler _emailHandler;

        public SendFAQEmailCommandHandler(IEmailHandler email)
        {
            _emailHandler = email;
        }

        public async Task<CommandResponse<EmailDTO>> Handle(SendFAQEmailCommand request, CancellationToken cancellationToken)
        {
            request.Email.Subject = "FAQ Question";
            request.Email.Body = "Thank you for your question. We will get to you as soon as possible!";
            await _emailHandler.SendEmail(request.Email);

            return new CommandResponse<EmailDTO>();
        }
    }
}
