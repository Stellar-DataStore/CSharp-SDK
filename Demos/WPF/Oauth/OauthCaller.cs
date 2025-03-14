namespace StellarDS.Demos.WPF.Oauth;

public class OauthCaller
{
   // public static OauthToken AuthToken;

    /// <summary> 
    /// Opens a browser that navigates to a specific url to obtain permissions from a user to access their data.  
    /// </summary> 
    /// <returns>An authentication code that lets the API know that we have permission from the user to access their data</returns> 
    public static string GetAuthCode(string destinationUrl, string redirectUrl) 
    { 
        var browser = new OauthBrowser(destinationUrl,redirectUrl); 
        browser.ShowDialog();
        return browser.AuthCode ?? "";
    } 
}