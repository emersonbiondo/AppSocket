namespace AppServer
{
    public class ClientConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public ClientConfiguration()
        {
            Server = "127.0.0.1";
            Port = 8888;
        }
    }
}
