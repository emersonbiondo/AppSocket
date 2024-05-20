using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;

namespace AppClient.Presentations.Models
{
    public partial class Client : ObservableObject
    {
        private string ip;

        private int port;

        [ObservableProperty]
        public string message;

        public Client(string ip, int port)
        {
            Message = string.Empty;
            this.ip = ip;
            this.port = port;
        }

        public void SendMessage()
        {
            using (TcpClient tcpClient = new TcpClient(ip, port))
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(networkStream))
                    {
                        streamWriter.WriteLine(Message);
                        streamWriter.Flush();
                    }
                }
            }
        }
    }
}
