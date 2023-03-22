using MediatR;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Application.CQRS.Notifications.Commands.SendFAQEmail
{
    public class SendFAQEmailCommand : IRequest<CommandResponse<EmailDTO>>
    {
        public SendFAQEmailCommand(EmailDTO email)
        {
            Email = email;
        }

        public EmailDTO Email { get; set; }
    }
}
