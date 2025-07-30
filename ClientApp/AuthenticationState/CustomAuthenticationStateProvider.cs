

namespace ClientApp.AuthenticationState;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ClientApp;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	private readonly IJSRuntime _js;

	public CustomAuthenticationStateProvider(IJSRuntime js)
	{
		_js = js;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "authToken");

		var identity = new ClaimsIdentity();

		if (!string.IsNullOrWhiteSpace(token))
		{
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			if (jwtToken.ValidTo > DateTime.UtcNow)
			{
				identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
			}
		}

		var user = new ClaimsPrincipal(identity);
		return new AuthenticationState(user);
	}

	public void NotifyUserAuthentication(string token)
	{
		var handler = new JwtSecurityTokenHandler();
		var jwtToken = handler.ReadJwtToken(token);
		var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
		var user = new ClaimsPrincipal(identity);
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}

	public void NotifyUserLogout()
	{
		var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
	}
}
