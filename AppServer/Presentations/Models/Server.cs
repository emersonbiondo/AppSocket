using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;

namespace AppServer.Presentations.Models
{
    public partial class Server: ObservableObject
    {
        private TcpListener tcpListener;
        private CancellationTokenSource? cancellationTokenSource;

        [ObservableProperty]
        public ObservableCollection<string> messages;

        public Server(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            Messages = new ObservableCollection<string>();
            Start();
        }

        public void Start()
        {
            if (cancellationTokenSource == null)
            {
                cancellationTokenSource = new CancellationTokenSource();

                var cancellationToken = cancellationTokenSource.Token;

                Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var cancel = false;

                    tcpListener.Start();

                    while (!cancel)
                    {
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();
                        Thread clientThread = new Thread(() => HandleClient(tcpClient));
                        clientThread.Start();
                    }

                    if (cancel)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                }, cancellationToken);
            }
        }

        public void Stop()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
        }

        private void HandleClient(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream networkStream = tcpClient.GetStream())
                {
                    var streamReader = new StreamReader(networkStream);

                    if (streamReader != null)
                    {
                        var message = streamReader.ReadLine();

                        if (message != null)
                        {
                            Messages.Add(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
