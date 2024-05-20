using AppServer.Presentations.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Util;

namespace AppServer.Presentations.ViewModels
{
    public partial class ServerViewModel: ObservableObject
    {
        [ObservableProperty]
        public Server server;

        public ServerViewModel(ServerConfiguration serverConfiguration)
        {
            Server = new Server(serverConfiguration.Port);
        }
    }
}
