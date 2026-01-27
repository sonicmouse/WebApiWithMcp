using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebApiWithMcp.Infrastructure
{
	internal sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private const string ApiKeyHeaderName = "X-Api-Key";

		public ApiKeyAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder) 
			: base(options, logger, encoder)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var expectedApiKey = Context.RequestServices
				.GetRequiredService<IConfiguration>()["McpServer:ApiKey"];

			if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
			{
				return Task.FromResult(AuthenticateResult.Fail("Missing API Key"));
			}

			var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

			if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != expectedApiKey)
			{
				return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));
			}

			var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
