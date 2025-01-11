using System.Net;
using System.Net.Mail;

namespace HttpServerLibrary.services;

public class MailService : IMailService
{
    public static string SenderEmail = "foxgames1987@gmail.com";
    public async Task SendAsync(string? email, MailMessage m)
    {
        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Timeout = 1000 * 60 * 20;
        using (smtp)
        {
            smtp.Credentials = new NetworkCredential(SenderEmail, "xmkr pdvl njvj lrjm");
            smtp.EnableSsl = true;

            using (m)
            {
                m.From = new MailAddress(SenderEmail);
                m.To.Add(email);
                
                try
                {
                    smtp.Send(m);
                    Console.WriteLine("Сообщение отправлено");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка отправки сообщения: {e.Message}\n{e}");
                }
            }
        }
    }
    
    public static MailMessage BuildRegisterMessage(string message, string email)
    {
        var m = new MailMessage();
        m.Subject = "Сообщение от сайта";
        m.IsBodyHtml = true;
        m.Body = $"Получено сообщения с сайта\n Здравствуйте, {email}. Вы успешно зарегистрированы под паролем: {message}";
        return m;
    }
    
    public static MailMessage BuildLolMessage(string message, string email)
    {
        var m = new MailMessage();
        m.Subject = "Сообщение от сайта";
        m.IsBodyHtml = true;
        m.Body = $"Ха-ха вы попались, ваш логин {email} ваш пароль {message} теперь знаю я Низамов Алмаз";
        return m;
    }
    
    public static MailMessage BuildRequestMessage(string message, string email)
    {
        var m = new MailMessage();
        m.Subject = "Сообщение от сайта";
        m.IsBodyHtml = true;
        m.Body = $"Вы подписались на рассылку ваш логин {email} ваш пароль {message}";
        return m;
    }

    public static MailMessage BuildMailMessageWithAttachment(string? email, string attachment)
    {
        var m = new MailMessage();
        m.Subject = "Сообщение от сайта";
        m.IsBodyHtml = true;
        m.Body = $"Здравствуйте, {email}.\nДомашняя работа:";
        m.Attachments.Add(new Attachment(attachment));
        return m;
    }
}