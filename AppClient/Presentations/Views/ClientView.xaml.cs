using AppClient.Presentations.ViewModels;

namespace AppClient.Presentations.Views;

public partial class ClientView : ContentPage
{
	public ClientView(ClientViewModel clientViewModel)
	{
		InitializeComponent();
        BindingContext = clientViewModel;
    }
}