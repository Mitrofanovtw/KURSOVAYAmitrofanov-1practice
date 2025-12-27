using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views
{
    public partial class CreateRequestPage : ContentPage
    {
        private readonly CreateRequestViewModel _viewModel;

        public CreateRequestPage(CreateRequestViewModel viewModel)
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