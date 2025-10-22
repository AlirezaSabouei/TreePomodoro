using System.Net;
using System.Net.Mail;
using Application.Common.Tools;

namespace Infrastructure.Tools;

public class Email(string senderEmail, string senderPassword) : IEmail
{
    private readonly string _senderEmail = senderEmail;
    private readonly string _senderPassword = senderPassword;

    public async Task SendEmailAsync(string email,string subject, string body)
    {
        // Create a new MailMessage object
        MailMessage mail = new(_senderEmail, email)
        {
            // Set the subject and body of the email
            Subject = subject,
            Body = body
        };

        // Create an SMTP client to send the email
        SmtpClient smtpClient = new("smtp.gmail.com")
        {
            Port = 587, // Port for Gmail SMTP
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true // Use SSL for secure connection
        };

        try
        {
            // Send the email
            smtpClient.Send(mail);
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            // Dispose of resources
            mail.Dispose();
            smtpClient.Dispose();
        }
    }
}