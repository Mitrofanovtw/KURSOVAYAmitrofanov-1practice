using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views
{
    public partial class EngineersPage : ContentPage
    {
        private readonly EngineersViewModel _viewModel;

        public EngineersPage(EngineersViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadEngineersAsync();
        }
    }
}