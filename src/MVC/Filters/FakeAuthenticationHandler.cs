// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Options;
// using System.Security.Claims;
// using System.Text.Encodings.Web;
// using Domain.Entities.Accounts;
//
// namespace API.Filters;
//
// public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
// {
//     public const string AuthenticationScheme = "TestScheme";
//     public static Guid PlayerId = Guid.NewGuid();
//
//     public FakeAuthenticationHandler(
//         IOptionsMonitor<AuthenticationSchemeOptions> options,
//         ILoggerFactory logger,
//         UrlEncoder encoder,
//         ISystemClock clock)
//         : base(options, logger, encoder, clock)
//     {
//     }
//
//     protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//     {
//         var claims = new[]
//         {
//             new Claim(ClaimTypes.Email, "test@gmail.com"),
//             new Claim(ClaimTypes.Role, Role.SimplePlayer.ToString()),
//             new Claim("id", PlayerId.ToString())
//         };
//         var identity = new ClaimsIdentity(claims, Scheme.Name);
//         var principal = new ClaimsPrincipal(identity);
//         var ticket = new AuthenticationTicket(principal, Scheme.Name);
//
//         return AuthenticateResult.Success(ticket);
//     }
// }
