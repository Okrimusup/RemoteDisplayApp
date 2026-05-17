using HPPH;
using ScreenCapture.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using static RemoteDisplayApp.Logic;
using static RemoteDisplayApp.Statistic;
namespace RemoteDisplayApp
{
    internal partial class Form1 : Form, IUIController
    {
        private static bool isProcessing = false;
        private static bool shouldRestartServer = true;
        private static Size settingResolution = new Size();
        private static int settingFps;
        private static int settingCompression;
        private static int settingPort;
        private static int settingReconnectDelay;
        private static bool settingExtendedLog;


        private void LoadSettings(string group)
        {
            try
            {

                var settings = ReadFromJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "Settings.json"), group);
                if (settings.Count > 0)
                {
                    ResolutionCB.SelectedIndex = Convert.ToInt16(settings[0]);
                    FpsCB.SelectedIndex = Convert.ToInt16(settings[1]);
                    CompressionCB.SelectedIndex = Convert.ToInt16(settings[2]);
                    PortTB.Text = settings[3].ToString();
                    ReconnectTB.Text = settings[4].ToString();
                    ExtendedLogCB.Checked = Convert.ToBoolean(settings[5]);
                    Log($"Загружены настройки: {string.Join(", ", settings)}", LogLevel.Info, true);
                }
                else
                {
                    Log($"Настройки не найдены или пусты", LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при загрузке настроек: {ex}", LogLevel.Error);
            }

        }
        private void SaveSettings(string group)
        {
            try
            {
                var settings = new Dictionary<string, object>
                {
                    { "resolution", ResolutionCB.SelectedIndex },
                    { "fps", FpsCB.SelectedIndex },
                    { "compression", CompressionCB.SelectedIndex },
                    { "port", int.Parse(PortTB.Text) },
                    {"reconnection", int.Parse(ReconnectTB.Text) },
                    { "extendedLogs", ExtendedLogCB.Checked }
                };
                WriteToJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "Settings.json"), group, settings);
                Log($"Сохранены настройки: {string.Join(", ", settings)}", LogLevel.Info, true);
            }
            catch (Exception ex)
            {
                Log($"Ошибка при сохранении настроек: {ex}", LogLevel.Error);
            }
        }

        private void UpdateSettingsVariables()
        {
            try
            {
                switch (ResolutionCB.SelectedIndex)
                {
                    case 0:
                        settingResolution = new Size(640, 360);
                        break;
                    case 1:
                        settingResolution = new Size(1280, 720);
                        break;
                    case 2:
                        settingResolution = new Size(1920, 1080);
                        break;
                    case 3:
                        settingResolution = new Size(2560, 1440);
                        break;
                    default:
                        settingResolution = new Size(1280, 720);
                        break;
                }
                switch (FpsCB.SelectedIndex)
                {
                    case 0:
                        settingFps = Convert.ToInt16(Math.Floor((double)1000 / 30));
                        break;
                    case 1:
                        settingFps = Convert.ToInt16(Math.Floor((double)1000 / 60));
                        break;
                    case 2:
                        settingFps = Convert.ToInt16(Math.Floor((double)1000 / 120));
                        break;
                    default:
                        settingFps = Convert.ToInt16(Math.Floor((double)1000 / 30));
                        break;
                }
                switch (CompressionCB.SelectedIndex)
                {
                    case 0:
                        settingCompression = 80;
                        break;
                    case 1:
                        settingCompression = 60;
                        break;
                    case 2:
                        settingCompression = 20;
                        break;
                    default:
                        settingCompression = 60;
                        break;
                }
                settingPort = Convert.ToInt32(PortTB.Text);
                settingReconnectDelay = Convert.ToInt32(ReconnectTB.Text);
                settingExtendedLog = ExtendedLogCB.Checked;
            }
            catch (Exception ex)
            {
                Log($"Ошибка при загрузке настроек в переменные: {ex}", LogLevel.Error);
            }
        }
        public void WriteLog(string message, LogLevel level = LogLevel.Info, bool extended = false)
        {
            if (extended == false || ExtendedLogCB.Checked)
            {
                string dt = DateTime.Now.ToString("dd.MM HH:mm:ss");
                string prefix = level switch
                {
                    LogLevel.Info => "[INFO]",
                    LogLevel.Warning => "[WARNING]",
                    LogLevel.Error => "[ERROR]",
                    _ => "[INFO]"
                };
                string logMessage = ($"[{dt}] {prefix} {message}\n");
                int startindex = LogRTS.TextLength;
                LogRTS.AppendText(logMessage);
                LogRTS.Select(startindex, logMessage.Length);
                LogRTS.SelectionColor = level switch
                {
                    LogLevel.Info => Color.Black,
                    LogLevel.Warning => Color.Orange,
                    LogLevel.Error => Color.Red,
                    _ => Color.Black
                };
                LogRTS.Select(LogRTS.TextLength, 0);
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
            this.DoubleBuffered = true;
            SetUIController(this);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(430, 305);
            this.MinimumSize = new Size(430, 305);
            this.Size = new Size(430, 305);
            LoadSettings("currentSettings");
            UpdateSettingsVariables();
            LocalIpTB.Text = GetLocalIp();
            //label1.Text = GetLocalIp();
            Log("Запуск приложения");
            InitializeDX11Capture(0);
            StreamTimer.Interval = settingFps;
            LocalIpL.Text = $"Локальный IP: {GetLocalIp()}";
            LocalIpL.Location = new Point((panel1.Size.Width - LocalIpL.Width) / 2, LocalIpL.Location.Y);
            foreach (var display in GetDisplayList()) { 
                MonitorCB.Items.Add(display);
            }
            MonitorCB.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log("Закрытие приложения");
            shouldRestartServer = false;
            StreamTimer.Stop();
            PreviewTimer.Stop();
            StopServer();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MainControl.SelectedTab.Name)
            {
                case "Preview_tp":
                    InitializeDX11Capture(MonitorCB.SelectedIndex);
                    PreviewTimer.Start();
                    pictureBox1.Size = new Size(1280, 720);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    int formWidth = 1280 + 20;
                    int formHeight = 720 + MainControl.Top + 40;

                    this.MaximumSize = new Size(formWidth, formHeight);
                    this.MinimumSize = new Size(formWidth, formHeight);
                    this.Size = new Size(formWidth, formHeight);
                    break;
                case "Settings_tp":
                    PreviewTimer.Stop();
                    this.MaximumSize = new Size(430, 500);
                    this.MinimumSize = new Size(430, 500);
                    this.Size = new Size(430, 500);
                    break;
                case "Monitor_tp":
                    PreviewTimer.Stop();
                    this.MaximumSize = new Size(430, 125);
                    this.MinimumSize = new Size(430, 125);
                    this.Size = new Size(430, 125);
                    break;
                case "Main_tp":
                    PreviewTimer.Stop();
                    this.MaximumSize = new Size(430, 305);
                    this.MinimumSize = new Size(430, 305);
                    this.Size = new Size(430, 305);
                    break;
                case "Stats_tp":
                    PreviewTimer.Stop();
                    this.MaximumSize = new Size(430, 210);
                    this.MinimumSize = new Size(430, 210);
                    this.Size = new Size(430, 200);
                    break;
                default:
                    PreviewTimer.Stop();
                    this.MaximumSize = new Size(700, 600);
                    this.MinimumSize = new Size(700, 600);
                    this.Size = new Size(700, 600);
                    break;
            }
        }

        private async void StreamTimer_Tick(object sender, EventArgs e)
        {
            if(!isServerRunning || isProcessing)
                return;
            try
            {
                isProcessing = true;
                var stopwatch = Stopwatch.StartNew();
                Statistic.RecordCapturedFrame();
                Bitmap bmp = CaptureScreenDX11();
                stopwatch.Stop();
                Statistic.RecordEncodeTime(stopwatch.ElapsedMilliseconds);
                if (bmp != null)
                {

                    using (Bitmap resized = ResizeImage(bmp, settingResolution.Width, settingResolution.Height))
                    {

                        byte[] encodedFrame = EncodeToJpeg(resized, settingCompression);
                        Statistic.RecordSentFrame(encodedFrame);

                        await SendFrameToClientAsync(encodedFrame);
                    }
                    bmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка таймера трансляции: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Stream timer error: {ex.Message}");
            }
            finally { isProcessing = false; }
        }

        

        private void PreviewTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = CaptureScreenDX11();
                if (bmp != null)
                {
                    using (Bitmap resized = ResizeImage(bmp, 1280, 720))
                    {
                        UpdatePreview((Bitmap)resized.Clone());
                    }
                    bmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка таймера предпросмотра: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Preview timer error: {ex.Message}");
            }
        }

        private void ApplySettingsBT_Click(object sender, EventArgs e)
        {
            SaveSettings("currentSettings");
            UpdateSettingsVariables();
            StreamTimer.Interval = settingFps;
        }

        private void ResetSettingsBT_Click(object sender, EventArgs e)
        {
            LoadSettings("defaultSettings");
            UpdateSettingsVariables();
            SaveSettings("currentSettings");
            StreamTimer.Interval = settingFps;
        }

        private async void StreamBtn_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (!isServerRunning)
                {
                    InitializeDX11Capture(MonitorCB.SelectedIndex);
                    _ = StartSever(settingPort, settingReconnectDelay);
                    StreamTimer.Interval = settingFps;
                    StreamTimer.Start();
                    StatisticTimer.Start();
                    StreamBtn.Text = "⏹\nОстановить трансляцию";
                    StreamStatusL.ForeColor = Color.FromArgb(255, 64, 64);
                    StreamStatusL.Text = "ТРАНСЛЯЦИЯ АКТИВНА";

                }
                else
                {
                    StopServer();
                    StreamTimer.Stop();
                    StatisticTimer.Stop();
                    StreamBtn.Text = "▶\nНачать трансляцию";
                    StreamStatusL.ForeColor = Color.Gray;
                    StreamStatusL.Text = "ТРАНСЛЯЦИЯ НЕ АКТИВНА";
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка при запуске стрима: {ex.Message}", LogLevel.Error);
                System.Diagnostics.Debug.WriteLine($"Start server error: {ex.Message}");

            }
        }

        private void StatisticTimer_Tick(object sender, EventArgs e)
        {
            if (isServerRunning)
            {
                if (StreamStatusL.Text.Contains("..."))
                {
                    StreamStatusL.Text = "ТРАНСЛЯЦИЯ АКТИВНА";

                }
                else
                {
                    StreamStatusL.Text += ".";
                }

                Statistic.UpdateStatistics();
                CapturedFpsStatL.Text = $"Кадров/с захвата: {Statistic.CapturedFPS}";
                SentFpsStatL.Text = $"Кадров/с отправки: {Statistic.SentFPS}";
                FrameSizeStatL.Text = $"Средний размер кадра: {Statistic.AverageFrameSize/1024} КБайт";
                NetworkStatL.Text = $"Использование сети: {Statistic.NetworkUsage / 1024} КБайт/сек";
                EncodeTimeStatL.Text = $"Среднее время кодирования: {Statistic.AverageDecodeTime} мс";
            }
        }

        private void RestartStreamBT_Click(object sender, EventArgs e)
        {
            
            isServerRunning = false;
            CloseConnection();
            InitializeDX11Capture(MonitorCB.SelectedIndex);
            Log("Сервер перезагружен");
        }
    }
}
