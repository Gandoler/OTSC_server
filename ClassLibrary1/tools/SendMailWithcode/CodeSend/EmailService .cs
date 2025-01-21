using System.Net.Mail;
using System.Net;
using Serilog;

namespace OTSC_ui.Tools.SendMailWithcode.CodeSend
{
    internal class EmailService(string senderEmail, string senderPassword, string smtpHost, int smtpPort) : IEmailService
    {
        private readonly string _senderEmail = senderEmail;
        private readonly string _senderPassword = senderPassword;
        private readonly string _smtpHost = smtpHost;
        private readonly int _smtpPort = smtpPort;



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
