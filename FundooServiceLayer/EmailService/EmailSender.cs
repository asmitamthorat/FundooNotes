
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace FundooServiceLayer.EmailService
{
    public class EmailSender:IEmailSender
    {
        private EmailConfiguration _config;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _config = emailConfiguration;
        }

        public void SendEmail(Message message)
        {
            var email = CreateEmail(message);
           Send(email);
        }

        public  void Send(MimeMessage email)
        {
            using (var client = new SmtpClient())
            {

                    client.Connect(_config.SmtpServer, _config.Port, true);
                    client.Authenticate(_config.UserName, Environment.GetEnvironmentVariable("Password", EnvironmentVariableTarget.User));
                    client.AuthenticationMechanisms.Remove("X0AUTH2");
                    client.Send(email);
                    client.Disconnect(true);
                    client.Dispose();
            }
        }

        public MimeMessage CreateEmail(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_config.From));
            emailMessage.To.AddRange(message.To.Select(user => MailboxAddress.Parse(user)));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format(message.Content) };
            return emailMessage;
        }
    }
}
