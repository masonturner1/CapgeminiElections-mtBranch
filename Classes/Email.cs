using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;


namespace CapgeminiElections.Email
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message);
    }

    public class MessageService : IMessageService
    {
        public async Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };

            email.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Start of provider specific settings
                await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);
                await client.AuthenticateAsync("capgeminielections@gmail.com", "Gamecocks2019").ConfigureAwait(false);
                // End of provider specific settings

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }

}


