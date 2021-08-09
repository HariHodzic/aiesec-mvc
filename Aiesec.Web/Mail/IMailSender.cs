using System.Threading.Tasks;

namespace Aiesec.Web.Mail
{
    public interface IMailSender
    {
        Task SendEmailAsync(Message message);
    }
}