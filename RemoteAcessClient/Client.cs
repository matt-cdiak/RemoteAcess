using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;

namespace RemoteAcessClient
{
    class Client
    {
        private const int Port = 9000;
        private const string ServerIp = "M154DSBRTH0101";

        public Bitmap ReceiveData()
        {
            try
            {
                TcpClient client = new TcpClient(ServerIp, Port);
                NetworkStream stream = client.GetStream();
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    client.Close();
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
