using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ITIRR.Web.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");

                if (string.IsNullOrEmpty(token))
                    return Unauthenticated();

                if (IsTokenExpired(token))
                {
                    await MarkUserAsLoggedOut();
                    return Unauthenticated();
                }

                SetHttpClientToken(token);

                var claims = ParseClaimsFromJwt(token);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                return new AuthenticationState(user);
            }
            catch
            {
                return Unauthenticated();
            }
        }

        public async Task MarkUserAsAuthenticated(string accessToken, string refreshToken)
        {
            await _localStorage.SetItemAsync("accessToken", accessToken);
            await _localStorage.SetItemAsync("refreshToken", refreshToken);

            SetHttpClientToken(accessToken);

            var claims = ParseClaimsFromJwt(accessToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            await _localStorage.RemoveItemAsync("refreshToken");

            _http.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(Task.FromResult(Unauthenticated()));
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");

            if (string.IsNullOrEmpty(token)) return null;

            if (IsTokenExpired(token))
            {
                await MarkUserAsLoggedOut();
                return null;
            }

            return token;
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var payload = token.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var claims = JsonSerializer
                    .Deserialize<Dictionary<string, object>>(jsonBytes);

                if (claims != null && claims.TryGetValue("exp", out var expObj))
                {
                    var exp = long.Parse(expObj.ToString()!);
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                    return DateTime.UtcNow > expDate;
                }

                return true; 
            }
            catch
            {
                return true;
            }
        }

        private void SetHttpClientToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private static AuthenticationState Unauthenticated()
        {
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer
                .Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
                foreach (var kvp in keyValuePairs)
                    claims.Add(new Claim(kvp.Key, kvp.Value?.ToString() ?? ""));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}