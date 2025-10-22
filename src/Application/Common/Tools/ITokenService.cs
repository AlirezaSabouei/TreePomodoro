namespace Application.Common.Tools
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string role, string email, string name);
        string GenerateRefreshToken();
    }
}