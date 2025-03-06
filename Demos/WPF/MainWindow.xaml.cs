using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StellarDs.SDK.Api;
using StellarDs.SDK.Client;

namespace StellarDS.Demos.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public async Task GetData()
    {

        var config = new Configuration();
        config.ApiKey.Add("Authorization", "YOUR_API_KEY");
        config.ApiKeyPrefix.Add("Authorization", "Bearer");
        
        var dataApi = new DataApi(config);
        var result = await dataApi.GetAsync(Guid.NewGuid(), 0);

        if (result is { IsSuccess: true, Data: not null })
        {
            var data = result.Data;
            var l = data.Select(d => d.ToObject<Border>()).ToList();
        }
        
        
    }
}