using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MealOrdering.Client.Utilities;

public class LocalAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationState _anonymous;
    private readonly HttpClient _httpClient;

    public LocalAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _httpClient = httpClient;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string apiToken = await _localStorageService.GetItemAsStringAsync("token");

        if (string.IsNullOrEmpty(apiToken))
            return _anonymous;

        string currentUserEmail = await _localStorageService.GetItemAsStringAsync("email");

        var claimPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[] 
                {
                    new Claim(ClaimTypes.Email, currentUserEmail)
                },
                "jwtAuthType"
            )
        );

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

        return new AuthenticationState(claimPrincipal);
    }

    public void NotifyUserLogin(string email)
    {
        var claimPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Email, email)
                },
                "jwtAuthType"
            )
        );

        var authState = Task.FromResult(new AuthenticationState(claimPrincipal));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);

        NotifyAuthenticationStateChanged(authState);
    }
}