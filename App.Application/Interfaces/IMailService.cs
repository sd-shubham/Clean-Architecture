using App.Application.Dtos;

namespace App.Application.Interfaces
{
    public interface IMailService
    {
        Task SendMailAsync(MailRequest mailRequset);
    }
}
