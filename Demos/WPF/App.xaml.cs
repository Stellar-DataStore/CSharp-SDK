using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StellarDS.Demos.WPF.Oauth;
using StellarDs.SDK.Api;
using StellarDs.SDK.Client;

namespace StellarDS.Demos.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    private IConfiguration? Configuration { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        _serviceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<MainWindow>();
        var config = new StellarDs.SDK.Client.Configuration();
        config.ApiKey.Add("Authorization", "");
        config.ApiKeyPrefix.Add("Authorization", "Bearer");

        var oauthConfig = Configuration?.GetSection("StellarDSSettings").Get<OauthConfiguration>();

        if (oauthConfig == null) return;
       
        // with the authCode we set the authetication token to use in api calls to dropbox.  
        var dataApi = new DataApi(config);
        var oauthApi = new OAuthApi( new StellarDs.SDK.Client.Configuration());
           
        serviceCollection.AddSingleton<DataApi>(dataApi);
        serviceCollection.AddSingleton<OAuthApi>(oauthApi);
        serviceCollection.AddSingleton<OauthConfiguration>(oauthConfig);

        serviceCollection.AddSingleton<RefreshTimer>();
    }
}