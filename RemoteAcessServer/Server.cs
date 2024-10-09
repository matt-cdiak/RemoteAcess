using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RemoteAcessServer
{
    class Server
    {
        private const int Port = 9000;

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            ScreenCapture screenCapture = new ScreenCapture();
            CameraCapture cameraCapture = new CameraCapture();

            cameraCapture.StartCapture((Bitmap cameraFrame) =>
            {
                // Captura a tela e envia a câmera e a tela
                Bitmap screenBitmap = screenCapture.CaptureScreen();

                using (MemoryStream msScreen = new MemoryStream())
                {
                    screenBitmap.Save(msScreen, System.Drawing.Imaging.ImageFormat.Png);
                    SendData(msScreen.ToArray(), listener);
                }

                using (MemoryStream msCamera = new MemoryStream())
                {
                    cameraFrame.Save(msCamera, System.Drawing.Imaging.ImageFormat.Png);
                    SendData(msCamera.ToArray(), listener);
                }
            });
        }

        private void SendData(byte[] data, TcpListener listener)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
