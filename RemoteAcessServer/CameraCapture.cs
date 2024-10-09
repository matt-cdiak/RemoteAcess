using System;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;

namespace RemoteAcessServer
{
    class CameraCapture
    {
        private VideoCaptureDevice videoSource;

        public void StartCapture(Action<Bitmap> frameCaptured)
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += (sender, eventArgs) =>
            {
                frameCaptured?.Invoke((Bitmap)eventArgs.Frame.Clone());
            };
            videoSource.Start();
        }

        public void StopCapture()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
        }
    }
}
