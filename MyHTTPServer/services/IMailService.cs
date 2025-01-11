using System.Net.Mail;

namespace HttpServerLibrary;

public interface IMailService
{
    Task SendAsync(string? email, MailMessage m);
}