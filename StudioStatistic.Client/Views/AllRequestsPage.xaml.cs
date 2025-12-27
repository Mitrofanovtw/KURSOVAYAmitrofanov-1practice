using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views
{
    public partial class AllRequestsPage : ContentPage
    {
        private readonly AllRequestsViewModel _viewModel;

        public AllRequestsPage(AllRequestsViewModel viewModel)
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
}