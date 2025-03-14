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
    private const int Table = 288;
    private readonly DataApi _dataApi;
    private readonly RefreshTimer _refreshTimer;
    private readonly OauthConfiguration _oauthConfiguration;
    private long _max = 0;
    private int _page;
    private int _totalPage;
    private int _offset = 0;

    public MainWindow(DataApi dataApi, RefreshTimer refreshTimer, OauthConfiguration oauthConfiguration)
    {
        _dataApi = dataApi;
        _refreshTimer = refreshTimer;
        _oauthConfiguration = oauthConfiguration;

        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var authCode = OauthCaller.GetAuthCode(_oauthConfiguration.GetDestinationUrl(), _oauthConfiguration.ReturnUrl);
        _dataApi.Configuration.ApiKey["Authorization"] = await _refreshTimer.RefreshAccessToken(authCode);
        await GetData(10, _offset);
    }

    private async Task GetData(int take = 0, int offset = 0)
    {
        var result = await _dataApi.GetAsync(_project, Table, offset, take);

        if (result is not { IsSuccess: true, Data: not null }) return;
        var data = result.Data;
        _max = result.Count;
        (_page, _totalPage) = CalculatePagination(_offset, 10, (int)_max);
        PageLabel.Text = $"Page {_page} of {_totalPage}";
        PaginationLabel.Text = $"Page {_page} of {_totalPage}";

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
            _dataApi.Configuration.ApiKey["Authorization"] = await _refreshTimer.RefreshAccessToken();
            var result = await _dataApi.PutAsync(_project, Table,
                new UpdateRecordRequest([rep.Id], request));

            if (result.IsSuccess)
            {
                await GetData(10, _offset);
            }
        }
        else
        {
            _dataApi.Configuration.ApiKey["Authorization"] = await _refreshTimer.RefreshAccessToken();
            var result = await _dataApi.PostAsync(_project, Table,
                new CreateRecordRequest(new List<Dictionary<string, object>> { request }));

            if (result.IsSuccess)
            {
                await GetData(10, _offset);
            }
        }

        dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var rep = (Customer)DataGrid.SelectedItem;
        await _dataApi.DeleteAsync(Guid.Parse("5f28959b-f6cd-43f1-64eb-08dcc0e23b55"), 288, (int)rep.Id);

        await GetData(10, _offset);
    }


    private async void OnPreviousClicked(object sender, RoutedEventArgs e)
    {
        if (_offset == 0) return;

        _offset -= 10;
        await GetData(10, _offset);
    }

    private async void OnNextClicked(object sender, RoutedEventArgs e)
    {
        if (_offset >= (int)_max) return;

        _offset += 10;
        await GetData(10, _offset);
    }
}