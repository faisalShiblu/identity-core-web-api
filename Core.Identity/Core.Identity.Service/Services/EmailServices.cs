using Core.Identity.Service.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace Core.Identity.Service.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfiguration _configuration;
        public EmailServices(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MimeMessage CreateMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _configuration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage messageMail)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_configuration.SmtpServer, _configuration.SmtpPort, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_configuration.UserName, _configuration.Password);
                client.Send(messageMail);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
        public void SendEmail(Message message)
        {
            Send(CreateMessage(message));
        }
    }
}
