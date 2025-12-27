using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views
{
    public partial class ServicesPage : ContentPage
    {
        private readonly ServicesViewModel _viewModel;

        public ServicesPage(ServicesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadServicesAsync();
        }
    }
}