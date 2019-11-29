using Demo.EmailNotifications.Entities;
using System.Threading.Tasks;

namespace Demo.EmailNotifications.Business
{
    public interface IEmailSender
    {
        Task<BaseResponse> SendEmailAsync(MailMessage mailMessage);
    }
}
