namespace OTSC_ui.Tools.SendMailWithcode.CodeSend
{
    internal interface IEmailService
    {
        void SendEmail(string recipientEmail, string subject, string body);
        Task SendEmailAsync(string recipientEmail, string subject, string body);
    }
}
