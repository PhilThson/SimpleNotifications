using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace NotificationHub.Helpers;

public class MyAuthScheme : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MyAuthScheme(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if(!Context.Request.Query.TryGetValue(Constants.UserIdClaim, out var founds))
        {
            return Task.FromResult(AuthenticateResult.Fail("No user id found in query string"));
        }

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new List<Claim> { new Claim(Constants.UserIdClaim, founds[0]) },
                Constants.MyAuthScheme));

        var ticket = new AuthenticationTicket(principal, Constants.MyAuthScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

