using GameStore.Models.EmailSender;

namespace GameStore.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
