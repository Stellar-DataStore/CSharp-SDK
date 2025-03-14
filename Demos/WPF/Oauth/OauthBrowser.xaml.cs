using System.Windows;
using System.Windows.Navigation;
using Microsoft.Web.WebView2.Core;

namespace StellarDS.Demos.WPF.Oauth;

public partial class OauthBrowser : Window
{
        private readonly string _destinationUrl; 
        private string _redirectUrl; 
        public string? AuthCode { get; set; } 
     
        public OauthBrowser(string destinationUrl, string redirectUrl) 
        { 
            InitializeComponent(); 
            _destinationUrl = destinationUrl; 
            _redirectUrl = redirectUrl; 
        } 
        /// <summary> 
        /// After the browser loads, we navigate to our destination URL which needs the app key and the type of  
        /// response needed to give us the token. 
        /// Note: this varies from API to API, so be sure to read the documentation for it! 
        /// </summary> 
        private async void OAuthBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            await WbOauth.EnsureCoreWebView2Async();
            WbOauth.CoreWebView2.Navigate(_destinationUrl); 
        } 
        /// <summary> 
        /// Event to obtain the authorization token sent by the authorization site.  
        /// </summary> 
       
        private void WbOauth_OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!WbOauth.Source.ToString().Contains("?code")) 
            { 
                return; 
            }

            var queryParams = GetQueryParams(WbOauth.Source.ToString());
            if (queryParams.Keys.Contains("code")) 
            { 
                AuthCode = queryParams["code"]; 
            } 
            this.Close(); 
        }

        private Dictionary<string, string> GetQueryParams(string url)
        {
            var queryParams = url.Split('?')[1].Split('&');
           return queryParams.Select(s => s.Split('=')).ToDictionary(queryParameter => queryParameter[0], queryParameter => queryParameter[1]);
        }
}