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
    private readonly int _table = 87;
    private readonly DataApi _api;
    
    public MainWindow(DataApi dataApi)
    {
        _api = dataApi;
        InitializeComponent();
    }
    
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        var data = await GetData();
        DataGrid.ItemsSource = data;
    }

    private async Task<IList<Representative>> GetData()
    {
        var result = await _api.GetAsync(_project, 87);

        if (result is not { IsSuccess: true, Data: not null }) return new List<Representative>();
        var data = result.Data;
        return data.Select(d => d.ToObject<Representative>()).ToList();
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

        if (e.Row.DataContext is not Representative rep)
        {
            dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
            return;
        }

        var request = rep.AsDictionary();
        request.Remove("Id");

        if (rep.Id != null)
        {
            var result = await _api.PutWithHttpInfoAsync(_project, _table,
                new UpdateRecordRequest([rep.Id], request));

            if (result.Data.IsSuccess)
            {
                await LoadData();
            }
        }
        else
        {
            var result = await _api.PostAsync(_project, _table,
                new CreateRecordRequest(new List<Dictionary<string, object>> { request }));

            if (result.IsSuccess)
            {
                await LoadData();
            }
        }

        dataGrid.RowEditEnding += DataGrid_OnRowEditEnding;
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var rep = (Representative)DataGrid.SelectedItem;
        await _api.DeleteAsync(Guid.Parse("5f28959b-f6cd-43f1-64eb-08dcc0e23b55"), 87, (int)rep.Id);

        await LoadData();
    }


}