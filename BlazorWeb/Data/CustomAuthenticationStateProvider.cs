
using DAL_Dapper.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;


public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorageService;
    private readonly IDapperData _db;


    public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorageService, IDapperData db)
    {
        _localStorageService = localStorageService;
        _db = db;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity = new();
        try
        {
            var accessToken = await _localStorageService.GetAsync<string>("accessToken");

            if (accessToken.Success && accessToken.Value != null)
            {
                User user = await _db.UserByAccessToken(accessToken.Value);

                if (user != null)
                {
                    await _db.GetTags(new List<User> { user }, "User");
                    identity = GetClaimsIdentity(user);
                    var cp = new ClaimsPrincipal(identity);
                    return await Task.FromResult(new AuthenticationState(cp));
                }

            }

        }
        catch (Exception)
        {
            MarkUserAsLoggedOut();
        }

        identity = new ClaimsIdentity();
        var claimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));


    }

    public async Task MarkUserAsAuthenticated(User user)
    {
        await _localStorageService.SetAsync("accessToken", user.AccessToken!);
        await _localStorageService.SetAsync("accessDate", DateTime.UtcNow);
        await _localStorageService.SetAsync("userProfile", user);

        var identity = GetClaimsIdentity(user);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var result = Task.FromResult(new AuthenticationState(claimsPrincipal));
        NotifyAuthenticationStateChanged(result);
    }

    public async void MarkUserAsLoggedOut()
    {
        try
        {
            await _localStorageService.DeleteAsync("accessToken");
            await _localStorageService.DeleteAsync("userProfile");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
        catch
        {


        }
    }

    private ClaimsIdentity GetClaimsIdentity(User user)
    {
        var claimsIdentity = new ClaimsIdentity("custom");

        if (user.EmailAddress != null)
        {
            var userClaim = new Claim(ClaimTypes.Name, user.EmailAddress);
            var givenNameClaim = new Claim(ClaimTypes.GivenName, user.Name);
            var roleClaims = user.Tags.Select(s => new Claim(ClaimTypes.Role, s.Title!));

            claimsIdentity.AddClaim(userClaim);
            claimsIdentity.AddClaim(givenNameClaim);
            claimsIdentity.AddClaims(roleClaims);

        }

        return claimsIdentity;
    }
}
