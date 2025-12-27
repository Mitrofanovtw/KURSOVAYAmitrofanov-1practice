using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views;

public partial class MyRequestsPage : ContentPage
{
    private readonly MyRequestsViewModel _viewModel;

    public MyRequestsPage(MyRequestsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadRequestsAsync();
    }
}