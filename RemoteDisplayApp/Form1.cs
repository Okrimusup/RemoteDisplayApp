using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RemoteDisplayApp
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        NetworkStream stream;
        private bool isProcessing = false;
        private string GetLocalIp()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                var endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint?.Address.ToString();
            }
        }
        private async void StartSever()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();

            var client = await listener.AcceptTcpClientAsync();
            stream = client.GetStream();

        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int x; public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINT pt;
        }

        const int CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pCursorInfo);
        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);
        private void DrawCursor(Graphics g, Screen screen)
        {
            CURSORINFO ci = new CURSORINFO();
            ci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

            if (GetCursorInfo(out ci) && (ci.flags & CURSOR_SHOWING) != 0)
            {
                int x = ci.pt.x - screen.Bounds.X; int y = ci.pt.y - screen.Bounds.Y;
                IntPtr hdc = g.GetHdc();
                DrawIcon(hdc, x, y, ci.hCursor);
                g.ReleaseHdc(hdc);
            }
        }
        private Bitmap CaptureScreen(Screen screen)
        {
            var bounds = screen.Bounds;

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                DrawCursor(g, Screen.PrimaryScreen);
            }

            return bitmap;
        }
        private Bitmap ResizeImage(Bitmap source, int maxWidth, int maxHeight)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (maxWidth <= 0 || maxHeight <= 0)
            {
                // fallback: return a clone or a minimal 1x1 bitmap to avoid exceptions
                return new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }

            double ratioX = (double)maxWidth / source.Width;
            double ratioY = (double)maxHeight / source.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = Math.Max(1, (int)(source.Width * ratio));
            int newHeight = Math.Max(1, (int)(source.Height * ratio));

            var result = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(source, 0, 0, newWidth, newHeight);
            }
            return result;
        }
        private byte[] EncodeToJpeg(Bitmap bmp)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        private async Task SendFrameToClientAsycnc(byte[] data)
        {
            if (stream != null)
            {
                try
                {
                    byte[] length = BitConverter.GetBytes(data.Length);
                    await stream.WriteAsync(length, 0, 4);
                    await stream.WriteAsync(data, 0, data.Length);
                    await stream.FlushAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Send error: {ex.Message}");
                    stream?.Dispose();
                    stream = null;
                }
            }
        }
        private void UpdatePreview(Bitmap resized)
        {
            var old = pictureBox1.Image;
            pictureBox1.Image = resized;
            old?.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 10;

            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var screens = Screen.AllScreens;
            foreach (var screen in screens)
            {
                System.Diagnostics.Debug.WriteLine(screen);
            }
            label1.Text = GetLocalIp();
            StartSever();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (isProcessing) return;
            isProcessing = true;

            try
            {
                //Захват кадра монитора
                using (Bitmap bmp = CaptureScreen(Screen.PrimaryScreen))
                {
                    //Ресайз для отправки и превью
                    using (Bitmap resized = ResizeImage(bmp, pictureBox1.Width, pictureBox1.Height))
                    {
                        byte[] encodedFrame = EncodeToJpeg(resized);
                        await SendFrameToClientAsycnc(encodedFrame);
                        UpdatePreview((Bitmap)resized.Clone());
                    }
                }
            }
            finally
            {
                isProcessing = false;
            }
        }
    }
}
