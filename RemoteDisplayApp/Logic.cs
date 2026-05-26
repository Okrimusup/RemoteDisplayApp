using ScreenCapture.NET;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Diagnostics;

namespace RemoteDisplayApp
{
    public static class Logic
    {
        static IUIController? uiController;
        public static IScreenCaptureService? screenCaptureService;
        public static IScreenCapture? screenCapture;
        private static ICaptureZone? captureZone;
        private static TcpListener? listener;
        public static NetworkStream? stream;
        private static TcpClient? currentClient;
        private static bool isProcessing = false;
        public static bool isServerRunning = false;
        public static bool shouldRestartServer = true;
        private static Screen? currentScreen;
        private static int buffer = -1;
        public static bool isDriverInstalled = false;

        public static List<string> ReadFromJsonFile(string path, string group)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Log($"Файл не найден: {path}", LogLevel.Warning);
                    return new List<string>();
                }

                string jsonContent = File.ReadAllText(path);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    JsonElement root = doc.RootElement;

                    if (root.TryGetProperty(group, out JsonElement groupElement))
                    {
                        List<string> result = new List<string>();

                        foreach (var property in groupElement.EnumerateObject())
                        {
                            string value = property.Value.GetRawText();
                            result.Add(value);
                        }

                        Log($"Успешно прочитаны настройки из группы '{group}': {result.Count} полей", LogLevel.Info);
                        return result;
                    }
                    else
                    {
                        Log($"Группа '{group}' не найдена в JSON файле", LogLevel.Warning);
                        return new List<string>();
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при чтении JSON файла: {ex.Message}", LogLevel.Error);
                return new List<string>();
            }
        }

        public static void WriteToJsonFile(string path, string group, Dictionary<string, object> settings)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Log($"Файл не найден: {path}", LogLevel.Warning);
                    return;
                }

                string jsonContent = File.ReadAllText(path);
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    JsonElement root = doc.RootElement;

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var jsonDict = new Dictionary<string, JsonElement>();
                    foreach (var property in root.EnumerateObject())
                    {
                        if (property.Name == group)
                        {
                            var settingsJson = JsonSerializer.Serialize(settings);

                            using (JsonDocument settingsDoc = JsonDocument.Parse(settingsJson))
                            {
                                jsonDict[group] = settingsDoc.RootElement.Clone();
                            }
                        }
                        else
                        {
                            jsonDict[property.Name] = property.Value.Clone();
                        }
                    }

                    string updatedJson = JsonSerializer.Serialize(jsonDict, options);
                    File.WriteAllText(path, updatedJson);

                    Log($"Успешно записаны настройки в группу '{group}': {settings.Count} полей", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при записи JSON файла: {ex.Message}", LogLevel.Error);
            }
        }



        public static void SetUIController(IUIController controller)
        {
            uiController = controller;
        }
        public static void Log(string message, LogLevel level = LogLevel.Info, bool extended = false)
        {
            uiController?.WriteLog(message, level, extended);
        }
        public static string GetLocalIp()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                var endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint?.Address.ToString();
            }
        }
        public static List<object> GetDisplayList()
        {
                try
                {
                    List<object> list = new List<object>();
                screenCaptureService = new DX11ScreenCaptureService();
                var graphicsCards = screenCaptureService?.GetGraphicsCards();
                    if (graphicsCards != null && graphicsCards.Any())
                    {
                        foreach (var card in graphicsCards)
                        {
                            var displays = screenCaptureService?.GetDisplays(card).ToList();
                            if (displays != null)
                            {
                                foreach (var display in displays)
                                {
                                    list.Add(display.DeviceName);
                                }
                            }
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при получении списка мониторов: {ex.Message}", LogLevel.Error);
                    return null;
                }
            }
        public static void InitializeDX11Capture(int displayN)
        {
                try
                {
                    if (displayN != buffer)
                    {

                        DisposeDX11Capture();

                        screenCaptureService = new DX11ScreenCaptureService();

                        var graphicsCards = screenCaptureService.GetGraphicsCards();
                        if (!graphicsCards.Any())
                        {
                            Log($"Не найдено графических адаптеров", LogLevel.Error);
                            return;
                        }

                        // Собрать все дисплеи со всех графических карт
                        var allDisplays = new List<Display>();
                        foreach (var card in graphicsCards)
                        {
                            var displays = screenCaptureService.GetDisplays(card).ToList();
                            allDisplays.AddRange(displays);
                        }

                        if (!allDisplays.Any())
                        {
                            Log($"Не найдено мониторов", LogLevel.Error);
                            return;
                        }

                        if (displayN >= allDisplays.Count)
                        {
                            Log($"Монитор с индексом {displayN} не найден", LogLevel.Error);
                            return;
                        }

                        screenCapture = screenCaptureService.GetScreenCapture(allDisplays[displayN]);
                        currentScreen = Screen.AllScreens.ElementAtOrDefault(displayN) ?? Screen.PrimaryScreen;

                        captureZone = screenCapture.RegisterCaptureZone(0, 0, screenCapture.Display.Width, screenCapture.Display.Height);
                        Log($"Захват экрана монитора {allDisplays[displayN].DeviceName} инициализирован");
                    }
                    buffer = displayN;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DX11 initialization error: {ex.Message}");
                    Log($"Ошибка инициализации DX11: {ex.Message}", LogLevel.Error);
                }
            }

        private static void DisposeDX11Capture()
        {
            try
            {
                captureZone = null;
                screenCapture?.Dispose();
                screenCapture = null;
                screenCaptureService?.Dispose();
                screenCaptureService = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DX11 dispose error: {ex.Message}");
                Log($"Ошибка освобождения DX11 ресурсов: {ex.Message}", LogLevel.Error);
            }
        }
        public static async Task StartSever(int serverPort, int restartDelay)
        {
            shouldRestartServer = true;
            await StartServerAsync(serverPort, restartDelay);
        }

        public static async Task StartServerAsync(int serverPort, int restartDelay)
        {
            while (shouldRestartServer)
            {
                try
                {
                    if (listener == null)
                    {
                        listener = new TcpListener(IPAddress.Any, serverPort);
                    }

                    isServerRunning = true;
                    listener.Start();
                    Log("Сервер прослушивает на порте " + serverPort);
                    System.Diagnostics.Debug.WriteLine("Server listening on port " + serverPort);

                    var client = await listener.AcceptTcpClientAsync();
                    currentClient = client;
                    stream = client.GetStream();
                    Log("Клиент подключен");
                    System.Diagnostics.Debug.WriteLine("Client connected");
                    await WaitForConnectionLossAsync();
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при запуске сервера: {ex.Message}", LogLevel.Error);
                    System.Diagnostics.Debug.WriteLine($"Server error: {ex.Message}");
                    isServerRunning = false;
                }
                finally
                {
                    CloseConnection();
                }

                if (shouldRestartServer && !isServerRunning)
                {
                    Log($"Перезапуск сервера через {restartDelay}мс");
                    System.Diagnostics.Debug.WriteLine($"Restarting server in {restartDelay}ms...");
                    await Task.Delay(restartDelay);
                }
            }
        }

        public static async Task WaitForConnectionLossAsync()
        {
            try
            {
                byte[] buffer = new byte[1];
                while (stream != null && stream.CanRead)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, 1);
                    if (bytesRead == 0)
                    {
                        Log("Клиент отключился", LogLevel.Warning);
                        System.Diagnostics.Debug.WriteLine("Client disconnected");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Потеряно соединение с клиентом: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Connection loss detected: {ex.Message}");
            }
        }

        public static void CloseConnection()
        {
            try
            {
                stream?.Dispose();
                stream = null;
                Log($"Стрим отключен");
            }
            catch (Exception ex)
            {
                Log($"Ошибка при отключении стрима: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Error disposing stream: {ex.Message}");
            }

            try
            {
                currentClient?.Dispose();
                currentClient = null;
                Log($"Клиент отключен");
            }
            catch (Exception ex)
            {
                Log($"Ошибка при отключении клиента: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Error disposing client: {ex.Message}");
            }

            try
            {
                listener?.Stop();
                Log($"Слушатель отключен");
            }
            catch (Exception ex)
            {
                Log($"Ошибка при отключении слушателя: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Error stopping listener: {ex.Message}");
            }
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
        private static void DrawCursor(Graphics g, Screen screen)
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
        public static Bitmap CaptureScreenDX11()
        {
                try
                {
                    if (screenCapture == null || captureZone == null)
                    {
                        Log("screenCapture или captureZone не инициализирована", LogLevel.Warning);
                        return null;
                    }

                    screenCapture.CaptureScreen();

                    using (captureZone.Lock())
                    {
                        int width = screenCapture.Display.Width;
                        int height = screenCapture.Display.Height;

                        ReadOnlySpan<byte> rawData = captureZone.RawBuffer;

                        Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                        var bitmapData = bitmap.LockBits(
                            new System.Drawing.Rectangle(0, 0, width, height),
                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                        Marshal.Copy(
                            rawData.ToArray(), 0,
                            bitmapData.Scan0,
                            Math.Min(rawData.Length, width * height * 4));

                        bitmap.UnlockBits(bitmapData);

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            DrawCursor(g, currentScreen ?? Screen.PrimaryScreen);
                        }

                        return bitmap;
                    }
                }
                catch (Exception ex)
                {
                    Log($"Ошибка при захвате экрана: {ex.Message}", LogLevel.Error);
                    System.Diagnostics.Debug.WriteLine($"DX11 Capture error: {ex.Message}");
                }
                return null;
            }
        public static Bitmap ResizeImage(Bitmap source, int maxWidth, int maxHeight)
        {
            try
            {
                if (maxWidth <= 0 || maxHeight <= 0)
                {
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
            catch(Exception ex) 
            { 
                Log("Ошибка при изменении размера изображения: " + ex.Message, LogLevel.Error);
                return null;
            }
            
        }
        public static byte[] EncodeToJpeg(Bitmap bmp, int quality = 75)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                var jpegEncoder = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.FormatID == ImageFormat.Jpeg.Guid);

                if (jpegEncoder == null)
                    throw new InvalidOperationException("JPEG encoder not found");


                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Math.Clamp(quality, 0, 100));

                bmp.Save(ms, jpegEncoder, encoderParams);
                return ms.ToArray();
            }
        }
        public static async Task SendFrameToClientAsync(byte[] data)
        {
            if (stream != null && stream.CanWrite && isServerRunning)
            {
                try
                {
                    byte[] length = BitConverter.GetBytes(data.Length);
                    await stream.WriteAsync(length, 0, 4);
                    await stream.WriteAsync(data, 0, data.Length);
                    await stream.FlushAsync();
                    Log($"Отправлен кадр: {data.Length} байт", LogLevel.Info, true);
                }
                catch (Exception ex)
                {
                    Log($"Ошибка отправки: {ex.Message}", LogLevel.Error);
                    System.Diagnostics.Debug.WriteLine($"Send error: {ex.Message}");
                    stream?.Dispose();
                    stream = null;
                    isServerRunning = false;
                }
            }
        }

        public static void StopServer()
        {
            shouldRestartServer = false;
            isServerRunning = false;
            CloseConnection();
            DisposeDX11Capture();
            Log("Сервер остановлен");
        }
    }
}
