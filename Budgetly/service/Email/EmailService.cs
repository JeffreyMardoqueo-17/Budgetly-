using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Budgetly.service.Auth
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tucorreo@gmail.com")); // cambia esto
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, false); // o el servidor que uses
            await smtp.AuthenticateAsync("tucorreo@gmail.com", "tupassword"); // variables de entorno idealmente
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}