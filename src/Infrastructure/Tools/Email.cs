using System.Net;
using System.Net.Mail;
using Application.Common.Tools;

namespace Infrastructure.Tools;

public class Email(string senderEmail, string senderPassword) : IEmail
{
    public Task SendEmailAsync(string email, string subject, string body, CancellationToken cancellationToken)
    {
        // Create a new MailMessage object
        MailMessage mail = new(senderEmail, email)
        {
            // Set the subject and body of the email
            Subject = subject,
            Body = body
        };

        // Create an SMTP client to send the email
        SmtpClient smtpClient = new("smtp.gmail.com")
        {
            Port = 587, // Port for Gmail SMTP
            Credentials = new NetworkCredential(senderEmail, senderPassword),
            EnableSsl = true // Use SSL for secure connection
        };

        try
        {
            // Send the email
            smtpClient.SendAsync(mail, cancellationToken);
            return Task.CompletedTask;
        }
        finally
        {
            // Dispose of resources
            mail.Dispose();
            smtpClient.Dispose();
        }
    }
}