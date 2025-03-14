using StellarDs.SDK.Api;
using StellarDs.SDK.Client;

namespace StellarDS.Demos.WPF.Oauth;

public class RefreshTimer(OAuthApi oAuthApi, OauthConfiguration configuration)
{
    private readonly OAuthApi _oAuthApi = oAuthApi;
    private readonly OauthConfiguration _configuration = configuration;
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    private DateTime RefreshTime { get; set; } = DateTime.MinValue;
    
    public async Task<string> RefreshAccessToken(string authCode = "")
    {
        var grantType = "authorization_code";
        if (AccessToken != null && authCode == "")
        {
            if (RefreshTime > DateTime.Now) return AccessToken;
            
            var oauthApi = new OAuthApi(new Configuration());
            var token = await oauthApi.TokenPostAsync("refresh_token",
                Guid.Parse(_configuration.ClientId),
                _configuration.Secret,
                _configuration.ReturnUrl, AccessToken,RefreshToken
            );
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
            RefreshTime = DateTime.Now.AddSeconds(token.ExpiresIn - 60);
        }
        else
        {
            var oauthApi = new OAuthApi(new Configuration());
            var token = await oauthApi.TokenPostAsync("authorization_code",
                Guid.Parse(_configuration.ClientId),
                _configuration.Secret,
                _configuration.ReturnUrl, authCode,RefreshToken
            );
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
            RefreshTime = DateTime.Now.AddSeconds(token.ExpiresIn - 60);
        }
        
        
        
        

        return AccessToken;
    }
}