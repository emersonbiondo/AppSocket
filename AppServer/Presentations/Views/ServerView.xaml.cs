using AppServer.Presentations.ViewModels;

namespace AppServer.Presentations.Views;

public partial class ServerView : ContentPage
{
    public ServerView(ServerViewModel serverViewModel)
    {
        InitializeComponent();
        BindingContext = serverViewModel;
    }
}