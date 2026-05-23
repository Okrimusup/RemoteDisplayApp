namespace RemoteDisplayApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            StreamTimer = new System.Windows.Forms.Timer(components);
            MainControl = new TabControl();
            Main_tp = new TabPage();
            panel2 = new Panel();
            RestartStreamBT = new Button();
            StreamBtn = new Button();
            panel1 = new Panel();
            LocalIpL = new Label();
            label7 = new Label();
            StreamStatusL = new Label();
            Monitor_tp = new TabPage();
            label15 = new Label();
            MonitorCB = new ComboBox();
            label16 = new Label();
            Preview_tp = new TabPage();
            pictureBox1 = new PictureBox();
            Stats_tp = new TabPage();
            EncodeTimeStatL = new Label();
            NetworkStatL = new Label();
            FrameSizeStatL = new Label();
            SentFpsStatL = new Label();
            CapturedFpsStatL = new Label();
            Logs_tp = new TabPage();
            LogRTS = new RichTextBox();
            Settings_tp = new TabPage();
            ResetSettingsBT = new Button();
            label14 = new Label();
            ExtendedLogCB = new CheckBox();
            ReconnectTB = new NumericUpDown();
            label12 = new Label();
            label13 = new Label();
            PortTB = new NumericUpDown();
            LocalIpTB = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label8 = new Label();
            label9 = new Label();
            ApplySettingsBT = new Button();
            label5 = new Label();
            CompressionCB = new ComboBox();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            ResolutionCB = new ComboBox();
            label1 = new Label();
            FpsCB = new ComboBox();
            label2 = new Label();
            PreviewTimer = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            StatisticTimer = new System.Windows.Forms.Timer(components);
            MainControl.SuspendLayout();
            Main_tp.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            Monitor_tp.SuspendLayout();
            Preview_tp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            Stats_tp.SuspendLayout();
            Logs_tp.SuspendLayout();
            Settings_tp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ReconnectTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PortTB).BeginInit();
            SuspendLayout();
            // 
            // StreamTimer
            // 
            StreamTimer.Tick += StreamTimer_Tick;
            // 
            // MainControl
            // 
            MainControl.Controls.Add(Main_tp);
            MainControl.Controls.Add(Monitor_tp);
            MainControl.Controls.Add(Preview_tp);
            MainControl.Controls.Add(Stats_tp);
            MainControl.Controls.Add(Logs_tp);
            MainControl.Controls.Add(Settings_tp);
            MainControl.Dock = DockStyle.Fill;
            MainControl.Location = new Point(0, 0);
            MainControl.Name = "MainControl";
            MainControl.SelectedIndex = 0;
            MainControl.Size = new Size(414, 456);
            MainControl.TabIndex = 0;
            MainControl.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // Main_tp
            // 
            Main_tp.Controls.Add(panel2);
            Main_tp.Controls.Add(panel1);
            Main_tp.Location = new Point(4, 24);
            Main_tp.Name = "Main_tp";
            Main_tp.Size = new Size(406, 428);
            Main_tp.TabIndex = 2;
            Main_tp.Text = "Главная";
            Main_tp.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(RestartStreamBT);
            panel2.Controls.Add(StreamBtn);
            panel2.Location = new Point(8, 124);
            panel2.Name = "panel2";
            panel2.Size = new Size(390, 109);
            panel2.TabIndex = 2;
            // 
            // RestartStreamBT
            // 
            RestartStreamBT.Cursor = Cursors.Hand;
            RestartStreamBT.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            RestartStreamBT.Location = new Point(196, 3);
            RestartStreamBT.Name = "RestartStreamBT";
            RestartStreamBT.Size = new Size(187, 101);
            RestartStreamBT.TabIndex = 3;
            RestartStreamBT.Text = "⟲\r\nПерезапустить трансляцию";
            RestartStreamBT.UseVisualStyleBackColor = true;
            RestartStreamBT.Click += RestartStreamBT_Click;
            // 
            // StreamBtn
            // 
            StreamBtn.Cursor = Cursors.Hand;
            StreamBtn.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            StreamBtn.Location = new Point(3, 3);
            StreamBtn.Name = "StreamBtn";
            StreamBtn.Size = new Size(187, 101);
            StreamBtn.TabIndex = 0;
            StreamBtn.Text = "▶\r\nНачать трансляцию";
            StreamBtn.UseVisualStyleBackColor = true;
            StreamBtn.Click += StreamBtn_ClickAsync;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(LocalIpL);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(StreamStatusL);
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(8, 13);
            panel1.Name = "panel1";
            panel1.Size = new Size(390, 105);
            panel1.TabIndex = 1;
            // 
            // LocalIpL
            // 
            LocalIpL.AutoSize = true;
            LocalIpL.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            LocalIpL.ForeColor = Color.Gray;
            LocalIpL.Location = new Point(25, 70);
            LocalIpL.Name = "LocalIpL";
            LocalIpL.Size = new Size(18, 21);
            LocalIpL.TabIndex = 2;
            LocalIpL.Text = "ll";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label7.ForeColor = Color.Gray;
            label7.Location = new Point(119, 5);
            label7.Name = "label7";
            label7.Size = new Size(152, 21);
            label7.TabIndex = 1;
            label7.Text = "Статус трансляции";
            // 
            // StreamStatusL
            // 
            StreamStatusL.AutoSize = true;
            StreamStatusL.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            StreamStatusL.ForeColor = Color.Gray;
            StreamStatusL.Location = new Point(25, 30);
            StreamStatusL.Name = "StreamStatusL";
            StreamStatusL.Size = new Size(340, 32);
            StreamStatusL.TabIndex = 0;
            StreamStatusL.Text = "ТРАНСЛЯЦИЯ НЕ АКТИВНА";
            // 
            // Monitor_tp
            // 
            Monitor_tp.Controls.Add(label15);
            Monitor_tp.Controls.Add(MonitorCB);
            Monitor_tp.Controls.Add(label16);
            Monitor_tp.Location = new Point(4, 24);
            Monitor_tp.Name = "Monitor_tp";
            Monitor_tp.Padding = new Padding(3);
            Monitor_tp.Size = new Size(406, 428);
            Monitor_tp.TabIndex = 0;
            Monitor_tp.Text = "Монитор";
            Monitor_tp.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Cursor = Cursors.Help;
            label15.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label15.Location = new Point(295, 15);
            label15.Name = "label15";
            label15.Size = new Size(42, 30);
            label15.TabIndex = 10;
            label15.Text = "ℹ️";
            toolTip1.SetToolTip(label15, "Выберите монитор, с которого будет транслироваться изображение");
            // 
            // MonitorCB
            // 
            MonitorCB.DropDownStyle = ComboBoxStyle.DropDownList;
            MonitorCB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MonitorCB.FormattingEnabled = true;
            MonitorCB.Location = new Point(135, 15);
            MonitorCB.Name = "MonitorCB";
            MonitorCB.Size = new Size(158, 29);
            MonitorCB.TabIndex = 9;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label16.Location = new Point(10, 15);
            label16.Name = "label16";
            label16.Size = new Size(76, 21);
            label16.TabIndex = 8;
            label16.Text = "Монитор";
            // 
            // Preview_tp
            // 
            Preview_tp.Controls.Add(pictureBox1);
            Preview_tp.Location = new Point(4, 24);
            Preview_tp.Name = "Preview_tp";
            Preview_tp.Padding = new Padding(3);
            Preview_tp.Size = new Size(406, 428);
            Preview_tp.TabIndex = 1;
            Preview_tp.Text = "Предпросмотр";
            Preview_tp.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 422);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Stats_tp
            // 
            Stats_tp.Controls.Add(EncodeTimeStatL);
            Stats_tp.Controls.Add(NetworkStatL);
            Stats_tp.Controls.Add(FrameSizeStatL);
            Stats_tp.Controls.Add(SentFpsStatL);
            Stats_tp.Controls.Add(CapturedFpsStatL);
            Stats_tp.Location = new Point(4, 24);
            Stats_tp.Name = "Stats_tp";
            Stats_tp.Size = new Size(406, 428);
            Stats_tp.TabIndex = 3;
            Stats_tp.Text = "Статистика";
            Stats_tp.UseVisualStyleBackColor = true;
            // 
            // EncodeTimeStatL
            // 
            EncodeTimeStatL.AutoSize = true;
            EncodeTimeStatL.Font = new Font("Segoe UI", 12F);
            EncodeTimeStatL.Location = new Point(8, 113);
            EncodeTimeStatL.Name = "EncodeTimeStatL";
            EncodeTimeStatL.Size = new Size(0, 21);
            EncodeTimeStatL.TabIndex = 4;
            // 
            // NetworkStatL
            // 
            NetworkStatL.AutoSize = true;
            NetworkStatL.Font = new Font("Segoe UI", 12F);
            NetworkStatL.Location = new Point(8, 88);
            NetworkStatL.Name = "NetworkStatL";
            NetworkStatL.Size = new Size(0, 21);
            NetworkStatL.TabIndex = 3;
            // 
            // FrameSizeStatL
            // 
            FrameSizeStatL.AutoSize = true;
            FrameSizeStatL.Font = new Font("Segoe UI", 12F);
            FrameSizeStatL.Location = new Point(8, 63);
            FrameSizeStatL.Name = "FrameSizeStatL";
            FrameSizeStatL.Size = new Size(0, 21);
            FrameSizeStatL.TabIndex = 2;
            // 
            // SentFpsStatL
            // 
            SentFpsStatL.AutoSize = true;
            SentFpsStatL.Font = new Font("Segoe UI", 12F);
            SentFpsStatL.Location = new Point(8, 37);
            SentFpsStatL.Name = "SentFpsStatL";
            SentFpsStatL.Size = new Size(0, 21);
            SentFpsStatL.TabIndex = 1;
            // 
            // CapturedFpsStatL
            // 
            CapturedFpsStatL.AutoSize = true;
            CapturedFpsStatL.Font = new Font("Segoe UI", 12F);
            CapturedFpsStatL.Location = new Point(8, 10);
            CapturedFpsStatL.Name = "CapturedFpsStatL";
            CapturedFpsStatL.Size = new Size(189, 21);
            CapturedFpsStatL.TabIndex = 0;
            CapturedFpsStatL.Text = "Трансляция не запущена";
            // 
            // Logs_tp
            // 
            Logs_tp.Controls.Add(LogRTS);
            Logs_tp.Location = new Point(4, 24);
            Logs_tp.Name = "Logs_tp";
            Logs_tp.Size = new Size(406, 428);
            Logs_tp.TabIndex = 4;
            Logs_tp.Text = "Логи";
            Logs_tp.UseVisualStyleBackColor = true;
            // 
            // LogRTS
            // 
            LogRTS.Dock = DockStyle.Fill;
            LogRTS.Location = new Point(0, 0);
            LogRTS.Name = "LogRTS";
            LogRTS.ReadOnly = true;
            LogRTS.Size = new Size(406, 428);
            LogRTS.TabIndex = 0;
            LogRTS.Text = "";
            // 
            // Settings_tp
            // 
            Settings_tp.BackColor = Color.Transparent;
            Settings_tp.Controls.Add(ResetSettingsBT);
            Settings_tp.Controls.Add(label14);
            Settings_tp.Controls.Add(ExtendedLogCB);
            Settings_tp.Controls.Add(ReconnectTB);
            Settings_tp.Controls.Add(label12);
            Settings_tp.Controls.Add(label13);
            Settings_tp.Controls.Add(PortTB);
            Settings_tp.Controls.Add(LocalIpTB);
            Settings_tp.Controls.Add(label10);
            Settings_tp.Controls.Add(label11);
            Settings_tp.Controls.Add(label8);
            Settings_tp.Controls.Add(label9);
            Settings_tp.Controls.Add(ApplySettingsBT);
            Settings_tp.Controls.Add(label5);
            Settings_tp.Controls.Add(CompressionCB);
            Settings_tp.Controls.Add(label6);
            Settings_tp.Controls.Add(label4);
            Settings_tp.Controls.Add(label3);
            Settings_tp.Controls.Add(ResolutionCB);
            Settings_tp.Controls.Add(label1);
            Settings_tp.Controls.Add(FpsCB);
            Settings_tp.Controls.Add(label2);
            Settings_tp.Location = new Point(4, 24);
            Settings_tp.Name = "Settings_tp";
            Settings_tp.Size = new Size(406, 428);
            Settings_tp.TabIndex = 5;
            Settings_tp.Text = "Настройки";
            // 
            // ResetSettingsBT
            // 
            ResetSettingsBT.AutoSize = true;
            ResetSettingsBT.Cursor = Cursors.Hand;
            ResetSettingsBT.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResetSettingsBT.Location = new Point(8, 394);
            ResetSettingsBT.Name = "ResetSettingsBT";
            ResetSettingsBT.Size = new Size(181, 31);
            ResetSettingsBT.TabIndex = 26;
            ResetSettingsBT.Text = "По умолчанию";
            ResetSettingsBT.UseVisualStyleBackColor = true;
            ResetSettingsBT.Click += ResetSettingsBT_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Cursor = Cursors.Help;
            label14.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label14.Location = new Point(209, 295);
            label14.Name = "label14";
            label14.Size = new Size(42, 30);
            label14.TabIndex = 25;
            label14.Text = "ℹ️";
            toolTip1.SetToolTip(label14, "Добавляет в логи подробную информацию о работе программы.");
            // 
            // ExtendedLogCB
            // 
            ExtendedLogCB.AutoSize = true;
            ExtendedLogCB.Cursor = Cursors.Hand;
            ExtendedLogCB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ExtendedLogCB.Location = new Point(13, 295);
            ExtendedLogCB.Name = "ExtendedLogCB";
            ExtendedLogCB.Size = new Size(206, 25);
            ExtendedLogCB.TabIndex = 24;
            ExtendedLogCB.Text = "Подробная запись логов";
            ExtendedLogCB.UseVisualStyleBackColor = true;
            // 
            // ReconnectTB
            // 
            ReconnectTB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ReconnectTB.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            ReconnectTB.Location = new Point(209, 240);
            ReconnectTB.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            ReconnectTB.Minimum = new decimal(new int[] { 500, 0, 0, 0 });
            ReconnectTB.Name = "ReconnectTB";
            ReconnectTB.Size = new Size(84, 29);
            ReconnectTB.TabIndex = 23;
            ReconnectTB.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Cursor = Cursors.Help;
            label12.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label12.Location = new Point(295, 240);
            label12.Name = "label12";
            label12.Size = new Size(42, 30);
            label12.TabIndex = 22;
            label12.Text = "ℹ️";
            toolTip1.SetToolTip(label12, "Время, через которое трансляция возобновит подключение\r\nподключение в случае разрыва.\r\n\r\n");
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label13.Location = new Point(10, 240);
            label13.Name = "label13";
            label13.Size = new Size(192, 63);
            label13.TabIndex = 21;
            label13.Text = "Время переподключения\r\n(мс)\r\n\r\n";
            // 
            // PortTB
            // 
            PortTB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PortTB.Location = new Point(135, 150);
            PortTB.Maximum = new decimal(new int[] { 65530, 0, 0, 0 });
            PortTB.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            PortTB.Name = "PortTB";
            PortTB.Size = new Size(158, 29);
            PortTB.TabIndex = 20;
            PortTB.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // LocalIpTB
            // 
            LocalIpTB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            LocalIpTB.Location = new Point(135, 195);
            LocalIpTB.Name = "LocalIpTB";
            LocalIpTB.ReadOnly = true;
            LocalIpTB.Size = new Size(158, 29);
            LocalIpTB.TabIndex = 19;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Cursor = Cursors.Help;
            label10.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label10.Location = new Point(295, 195);
            label10.Name = "label10";
            label10.Size = new Size(42, 30);
            label10.TabIndex = 18;
            label10.Text = "ℹ️";
            toolTip1.SetToolTip(label10, "Локальный IP-Адрес данного устройтсва. \r\nВ случае неправильного определения адреса попробуйте\r\nотключить VPN.\r\n");
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label11.Location = new Point(10, 195);
            label11.Name = "label11";
            label11.Size = new Size(108, 21);
            label11.TabIndex = 17;
            label11.Text = "Локальный IP";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Cursor = Cursors.Help;
            label8.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label8.Location = new Point(295, 150);
            label8.Name = "label8";
            label8.Size = new Size(42, 30);
            label8.TabIndex = 15;
            label8.Text = "ℹ️";
            toolTip1.SetToolTip(label8, "Порт, на котором работает трансляция. \r\nПорт должен совпадать со значением на мобильном клиенте.\r\nИзмените, если порт по умолчанию занят.\r\nЗначение по умолчанию: 5000\r\n");
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.Location = new Point(10, 150);
            label9.Name = "label9";
            label9.Size = new Size(46, 21);
            label9.TabIndex = 13;
            label9.Text = "Порт";
            // 
            // ApplySettingsBT
            // 
            ApplySettingsBT.AutoSize = true;
            ApplySettingsBT.Cursor = Cursors.Hand;
            ApplySettingsBT.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ApplySettingsBT.Location = new Point(218, 394);
            ApplySettingsBT.Name = "ApplySettingsBT";
            ApplySettingsBT.Size = new Size(181, 31);
            ApplySettingsBT.TabIndex = 12;
            ApplySettingsBT.Text = "Применить настройки";
            ApplySettingsBT.UseVisualStyleBackColor = true;
            ApplySettingsBT.Click += ApplySettingsBT_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Cursor = Cursors.Help;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(295, 105);
            label5.Name = "label5";
            label5.Size = new Size(42, 30);
            label5.TabIndex = 11;
            label5.Text = "ℹ️";
            toolTip1.SetToolTip(label5, "Высокая степень уменьшает задержку, но ухудшает качество картинки.\r\n");
            // 
            // CompressionCB
            // 
            CompressionCB.DropDownStyle = ComboBoxStyle.DropDownList;
            CompressionCB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CompressionCB.FormattingEnabled = true;
            CompressionCB.Items.AddRange(new object[] { "Низкая", "Обычная", "Высокая" });
            CompressionCB.Location = new Point(135, 105);
            CompressionCB.Name = "CompressionCB";
            CompressionCB.Size = new Size(158, 29);
            CompressionCB.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(10, 105);
            label6.Name = "label6";
            label6.Size = new Size(124, 21);
            label6.TabIndex = 9;
            label6.Text = "Степень сжатия";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Cursor = Cursors.Help;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(295, 60);
            label4.Name = "label4";
            label4.Size = new Size(42, 30);
            label4.TabIndex = 8;
            label4.Text = "ℹ️";
            toolTip1.SetToolTip(label4, "Изменяет частоту обновления картинки. \r\nДля высокой частоты обновления требуется\r\nсеть с высокой пропускной способностью.\r\nПри слабой сети рекомендуется значение 30.\r\n");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Cursor = Cursors.Help;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(295, 15);
            label3.Name = "label3";
            label3.Size = new Size(42, 30);
            label3.TabIndex = 7;
            label3.Text = "ℹ️";
            toolTip1.SetToolTip(label3, "Изменяет разрешение. \r\nПри повышении разрешения увеличиваются задержка и нагрузка на сеть.\r\nНе влияет на окно предпросмотра.");
            // 
            // ResolutionCB
            // 
            ResolutionCB.DropDownStyle = ComboBoxStyle.DropDownList;
            ResolutionCB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ResolutionCB.FormattingEnabled = true;
            ResolutionCB.Items.AddRange(new object[] { "640x360(SD)", "1280x720(HD)", "1920x1080(FHD)", "2560x1440(QHD)" });
            ResolutionCB.Location = new Point(135, 15);
            ResolutionCB.Name = "ResolutionCB";
            ResolutionCB.Size = new Size(158, 29);
            ResolutionCB.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(10, 15);
            label1.Name = "label1";
            label1.Size = new Size(98, 21);
            label1.TabIndex = 5;
            label1.Text = "Разрешение";
            // 
            // FpsCB
            // 
            FpsCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FpsCB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FpsCB.FormattingEnabled = true;
            FpsCB.Items.AddRange(new object[] { "30", "60", "120" });
            FpsCB.Location = new Point(135, 60);
            FpsCB.Name = "FpsCB";
            FpsCB.Size = new Size(158, 29);
            FpsCB.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(10, 60);
            label2.Name = "label2";
            label2.Size = new Size(91, 21);
            label2.TabIndex = 3;
            label2.Text = "Кадров/сек";
            // 
            // PreviewTimer
            // 
            PreviewTimer.Interval = 33;
            PreviewTimer.Tick += PreviewTimer_Tick;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 100;
            toolTip1.UseFading = false;
            // 
            // StatisticTimer
            // 
            StatisticTimer.Interval = 1000;
            StatisticTimer.Tick += StatisticTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(414, 456);
            Controls.Add(MainControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "Мобильный дисплей сервер";
            Load += Form1_Load;
            MainControl.ResumeLayout(false);
            Main_tp.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Monitor_tp.ResumeLayout(false);
            Monitor_tp.PerformLayout();
            Preview_tp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            Stats_tp.ResumeLayout(false);
            Stats_tp.PerformLayout();
            Logs_tp.ResumeLayout(false);
            Settings_tp.ResumeLayout(false);
            Settings_tp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ReconnectTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)PortTB).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer StreamTimer;
        private TabControl MainControl;
        private TabPage Main_tp;
        private TabPage Monitor_tp;
        private TabPage Preview_tp;
        private Button StreamBtn;
        private PictureBox pictureBox1;
        private TabPage Stats_tp;
        private TabPage Logs_tp;
        private TabPage Settings_tp;
        private System.Windows.Forms.Timer PreviewTimer;
        private RichTextBox LogRTS;
        private Label label2;
        private ComboBox FpsCB;
        private ToolTip toolTip1;
        private ComboBox ResolutionCB;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox CompressionCB;
        private Label label6;
        private Button ApplySettingsBT;
        private Label label8;
        private Label label9;
        private TextBox LocalIpTB;
        private Label label10;
        private Label label11;
        private NumericUpDown PortTB;
        private NumericUpDown ReconnectTB;
        private Label label12;
        private Label label13;
        private CheckBox ExtendedLogCB;
        private Label label14;
        private Button ResetSettingsBT;
        private System.Windows.Forms.Timer StatisticTimer;
        private Panel panel2;
        private Panel panel1;
        private Label StreamStatusL;
        private Label label7;
        private Button RestartStreamBT;
        private Label LocalIpL;
        private Label label15;
        private ComboBox MonitorCB;
        private Label label16;
        private Label CapturedFpsStatL;
        private Label EncodeTimeStatL;
        private Label NetworkStatL;
        private Label FrameSizeStatL;
        private Label SentFpsStatL;
    }
}
