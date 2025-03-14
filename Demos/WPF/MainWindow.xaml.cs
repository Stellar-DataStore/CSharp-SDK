using System.Collections.ObjectModel;
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
using StellarDS.Demos.WPF.Oauth;
using StellarDs.SDK.Api;
using StellarDs.SDK.Client;
using StellarDs.SDK.Model;

namespace StellarDS.Demos.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Guid _project = Guid.Parse("5f28959b-f6cd-43f1-64eb-08dcc0e23b55");
    private readonly int _table = 288;
    private readonly DataApi _dataApi;
    private readonly OauthConfiguration _configuration;
    private DateTime _refreshTime;
    private string _refreshToken = "";
    private long _max = 0;
    public int Page, TotalPage, Offset = 0;

    public MainWindow(DataApi dataApi, OauthConfiguration configuration)
    {
        _dataApi = dataApi;
        _configuration = configuration;
        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        string authCode = OauthCaller.GetAuthCode(_configuration.GetDestinationUrl(), _configuration.ReturnUrl);
        // with the authCode we set the authetication token to use in api calls to dropbox.  
        var oauthApi = new OAuthApi(new Configuration());
        var token = await oauthApi.TokenPostAsync("authorization_code",
            Guid.Parse(_configuration.ClientId),
            _configuration.Secret,
            _configuration.ReturnUrl,
            authCode);
        _dataApi.Configuration.ApiKey["Authorization"] = token.AccessToken;
        _refreshToken = token.RefreshToken;
        _refreshTime = DateTime.Now.AddMilliseconds(token.ExpiresIn - 60);

        await GetData(10, Offset);
    }

    private async void RefreshToken()
    {
        var oauthApi = new OAuthApi(new Configuration());
        var token = await oauthApi.TokenPostAsync("refresh_token",
            Guid.Parse(_configuration.ClientId),
            _configuration.Secret,
            _configuration.ReturnUrl, _dataApi.Configuration.ApiKey["Authorization"], _refreshToken
        );
        _dataApi.Configuration.ApiKey["Authorization"] = token.AccessToken;
        _refreshToken = token.RefreshToken;
        _refreshTime = DateTime.Now.AddMilliseconds(token.ExpiresIn - 60);
    }

    private async Task GetData(int take = 0, int offset = 0)
    {
        if (_refreshTime <= DateTime.Now) RefreshToken();
        
        var result = await _dataApi.GetAsync(_project, _table, offset, take);

        if (result is not { IsSuccess: true, Data: not null }) return;
        var data = result.Data;
        _max = result.Count;
        (Page, TotalPage) = CalculatePagination(Offset, 10, (int)_max);
        PageLabel.Text = $"Page {Page} of {TotalPage}";
        PaginationLabel.Text = $"Page {Page} of {TotalPage}";

        var source = data.Select(d => d.ToObject<Customer>()).ToList();
        DataGrid.ItemsSource = source;
    }

    private static (int CurrentPage, int MaxPages) CalculatePagination(int offset, int pageSize, int totalRecords)
    {
        if (pageSize <= 0) throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
        if (offset < 0) throw new ArgumentException("Offset cannot be negative.", nameof(offset));
        if (totalRecords < 0) throw new ArgumentException("Total records cannot be negative.", nameof(totalRecords));

        var currentPage = (offset / pageSize) + 1;
        var maxPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        return (currentPage, maxPages);
    }

    private async void DataGrid_OnRowEditEnding(object? sender, DataGridRowEditEndingEventArgs e)
    {
        if (sender is not DataGrid dataGrid || dataGrid.SelectedItem == null)
        {
            return;
        }

        dataGrid.RowEditEnding -= DataGrid_OnRowEditEnding;
        dataGrid.CommitEdit();

        if (e.EditAction != DataGridEditAction.Commit)
        {
            dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
            return;
        }

        if (e.Row.DataContext is not Customer rep)
        {
            dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
            return;
        }

        var request = rep.AsDictionary();
        request.Remove("Id");

        if (rep.Id != null)
        {
            if (_refreshTime <= DateTime.Now) RefreshToken();
            var result = await _dataApi.PutAsync(_project, _table,
                new UpdateRecordRequest([rep.Id], request));

            if (result.IsSuccess)
            {
                await GetData(10, Offset);
            }
        }
        else
        {
            if (_refreshTime <= DateTime.Now) RefreshToken();
            var result = await _dataApi.PostAsync(_project, _table,
                new CreateRecordRequest(new List<Dictionary<string, object>> { request }));

            if (result.IsSuccess)
            {
                await GetData(10, Offset);
            }
        }

        dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var rep = (Customer)DataGrid.SelectedItem;
        await _dataApi.DeleteAsync(Guid.Parse("5f28959b-f6cd-43f1-64eb-08dcc0e23b55"), 288, (int)rep.Id);

        await LoadData();
    }


    private async void OnPreviousClicked(object sender, RoutedEventArgs e)
    {
        if (Offset == 0) return;

        Offset -= 10;
        await GetData(10, Offset);
    }

    private async void OnNextClicked(object sender, RoutedEventArgs e)
    {
        if (Offset >= (int)_max) return;

        Offset += 10;
        await GetData(10, Offset);
    }
}