using System.Threading.Tasks;

using TableTracker.Domain.DataTransferObjects;

namespace TableTracker.Domain.Interfaces
{
    public interface IEmailHandler
    {
        Task SendEmail(EmailDTO email, bool html = false);
    }
}
