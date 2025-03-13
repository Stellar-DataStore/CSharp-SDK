using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StellarDs.SDK.Api;
using StellarDs.SDK.Client;

namespace StellarDS.Demos.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    public IConfiguration Configuration { get; private set; }

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
        serviceCollection.AddTransient(typeof(MainWindow));
        var config = new StellarDs.SDK.Client.Configuration();
        config.ApiKey.Add("Authorization", Configuration.GetSection("StellarDSSettings").GetValue<string>("APIKey") ?? "");
        config.ApiKeyPrefix.Add("Authorization", "Bearer");
        
        var dataApi = new DataApi(config);
        
        serviceCollection.AddSingleton<DataApi>(dataApi);
    }
}