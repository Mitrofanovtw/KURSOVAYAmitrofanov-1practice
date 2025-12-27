using StudioStatistic.Client.ViewModels;

namespace StudioStatistic.Client.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
    }
}