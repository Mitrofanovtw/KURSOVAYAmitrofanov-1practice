using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views
{
    public partial class ClientsPage : ContentPage
    {
        private readonly ClientsViewModel _viewModel;

        public ClientsPage(ClientsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadClientsAsync();
        }
    }
}