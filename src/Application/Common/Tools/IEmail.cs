namespace Application.Common.Tools;

public interface IEmail
{
    public Task SendEmailAsync(string email, string subject, string body);
}