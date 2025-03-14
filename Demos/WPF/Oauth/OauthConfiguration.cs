namespace StellarDS.Demos.WPF.Oauth;

public class OauthConfiguration
{
    public string ApiKey { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string Secret { get; set; } = "";
    public string ReturnUrl { get; set; } = "";
    public string AuthCodeUrl { get; set; } = "";
    public  string GetDestinationUrl() 
    { 
        return AuthCodeUrl + ($"?client_id={ClientId}&redirect_uri={ReturnUrl}&response_type=code"); 
    } 
}