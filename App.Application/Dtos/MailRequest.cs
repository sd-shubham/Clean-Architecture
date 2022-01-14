using Microsoft.AspNetCore.Http;

namespace App.Application.Dtos
{
    public record MailRequest(string ToEmail, string Subject, string Body, IEnumerable<IFormFile> Attachments);
}
