using System.Net.Mail;
using System.Net;
using OTSC_ui.Tools.AppSettingJsonPhars.Temaplates;
using Serilog;

namespace OTSC_ui.Tools.SendMailWithcode.CodeSend
{
    public class EmailServiceWithTemplate(EmailSettings emailSettings ) : IEmailService
    {
        private readonly string _senderEmail = emailSettings.SenderEmail;
        private readonly string _senderPassword = emailSettings.SenderPassword;
        private readonly string _smtpHost = emailSettings.SmtpServer;
        private readonly int _smtpPort = emailSettings.SmtpPort;
         


        private MailMessage GenerateMail(string recipientEmail, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_senderEmail, "OTSC"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mail.To.Add(recipientEmail);
            return mail;
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var mail = GenerateMail(recipientEmail, subject, body);
                using var smtpClient = CreateSmtpClient();
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Log.Error($"Email send error {nameof(EmailServiceWithTemplate)} ERROR: {ex.Message}");
            }
            Log.Information($"Email sent in {nameof(EmailServiceWithTemplate)} to ({recipientEmail})");
        }

        void IEmailService.SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                var mail = GenerateMail(recipientEmail, subject, body);
                using var smtpClient = CreateSmtpClient();
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Log.Error($"Email send error {nameof(EmailServiceWithTemplate)} ERROR: {ex.Message}");
            }
            Log.Information($"Email sent in {nameof(EmailServiceWithTemplate)} to ({recipientEmail})");
        }

    }
}
