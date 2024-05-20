using AppClient.Presentations.Views;

namespace AppClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientView), typeof(ClientView));
        }
    }
}
