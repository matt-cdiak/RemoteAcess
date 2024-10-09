using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RemoteAcessClient
{
    public partial class ViewerForm : Form
    {
        private Client client;
        private Thread screenThread;
        //private Thread cameraThread;

        public ViewerForm()
        {
            InitializeComponent();
            client = new Client(); // Inicializa o cliente
        }

        private void ViewerForm_Load(object sender, EventArgs e)
        {
            screenThread = new Thread(ReceiveScreen);
            screenThread.Start();

            //cameraThread = new Thread(ReceiveCamera);
            //cameraThread.Start();
        }

        private void ReceiveScreen()
        {
            while (true)
            {
                Bitmap screen = client.ReceiveData(); // Recebe os dados do servidor
                if (screen != null)
                {
                    // Atualiza o PictureBox com a captura da tela
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBox1.Image = screen; // Usa pictureBox1 para a tela
                    });
                }
            }
        }

        //private void ReceiveCamera()
        //{
        //    while (true)
        //    {
        //        Bitmap camera = client.ReceiveData(); // Recebe os dados do servidor
        //        if (camera != null)
        //        {
        //            // Atualiza o PictureBox com o feed da câmera
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                pictureBox2.Image = camera; // Usa pictureBox2 para a câmera
        //            });
        //        }
        //    }
        //}
    }
}
