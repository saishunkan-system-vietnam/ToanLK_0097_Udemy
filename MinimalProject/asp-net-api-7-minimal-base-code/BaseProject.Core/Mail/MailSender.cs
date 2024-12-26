using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.Mail
{
    public class MailSender : IMailSender
    {
        private readonly MailConfiguration _emailConfig;
        public MailSender(MailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public bool SendEmail(Message message, bool isHtml)
        {
            var emailMessage = CreateEmailMessage(message,isHtml);
            Send(emailMessage);
            return true;
        }

        private MimeMessage CreateEmailMessage(Message message,bool isHtml)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(message.FromName, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            if (!isHtml)
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            else
                emailMessage.Body = new TextPart("html") { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
