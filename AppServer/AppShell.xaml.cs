using AppServer.Presentations.Views;

namespace AppServer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ServerView), typeof(ServerView));
        }
    }
}
