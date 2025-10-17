using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeolocationTCP
{
    public partial class MainWindow : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public MainWindow()
        {
            InitializeComponent();

            // Ustawienia tłumaczeń
            groupBox1.Text = Resources.Strings.groupBox1_Text;
            groupBox2.Text = Resources.Strings.groupBox2_Text;
            label1.Text = Resources.Strings.label1_Text;
            label2.Text = Resources.Strings.label2_Text;
            label5.Text = Resources.Strings.label5_Text;
            label6.Text = Resources.Strings.label6_Text;
            label8.Text = Resources.Strings.label8_Text;
            label9.Text = Resources.Strings.label9_Text;
            label10.Text = Resources.Strings.label10_Text;
            label11.Text = Resources.Strings.label11_Text;
            label15.Text = Resources.Strings.label15_Text;

            // Ikona w zasobniku
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add(Resources.Strings.Tray_Open, OnOpen);
            trayMenu.MenuItems.Add(Resources.Strings.Tray_Exit, OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Geolocation TCP";
            trayIcon.Icon = new Icon(GetType(), "location.ico");
            trayIcon.Click += new EventHandler(OnTrayIconClick);
            trayIcon.Visible = true;
            trayIcon.ContextMenu = trayMenu;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Visible = true;
            ShowInTaskbar = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                ShowInTaskbar = false;
                trayIcon.BalloonTipTitle = "Geolocation TCP";
                trayIcon.BalloonTipText = "Aplikacja działa w tle.";
                trayIcon.ShowBalloonTip(2000);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            trayIcon.Visible = false; // usuń ikonę z traya przy zamknięciu
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                trayIcon.Dispose();
            }
            base.Dispose(disposing);
        }

        // ---- Obsługa ikony traya ----

        private void OnOpen(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void OnTrayIconClick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
                ShowInTaskbar = false;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }

        // ---- Metody pomocnicze UI ----

        internal void SetLocationLog(String text)
        {
            if (this.locationLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLocationLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.locationLog.AppendText(text + Environment.NewLine);
            }
        }

        delegate void SetTextCallback(string text);

        internal void SetLatLon(double lat, double lon)
        {
            String deg = "°";

            double lata = Math.Abs(lat);
            double latd = Math.Truncate(lata);
            double latm = (lata - latd) * 60;
            string lath = lat > 0 ? "N" : "S";
            double lnga = Math.Abs(lon);
            double lngd = Math.Truncate(lnga);
            double lngm = (lnga - lngd) * 60;
            string lngh = lon > 0 ? "E" : "W";

            string latitude = latd.ToString("00") + deg + latm.ToString(" 00.00") + "' " + lath;
            string longitude = lngd.ToString("000") + deg + lngm.ToString(" 00.00") + "' " + lngh;

            SetLat(latitude);
            SetLon(longitude);
        }

        internal void SetLat(String text)
        {
            if (this.labelLat.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLat);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelLat.Text = text;
            }
        }

        internal void SetLon(string text)
        {
            if (this.labelLon.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLon);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelLon.Text = text;
            }
        }

        internal void SetSpeed(string text)
        {
            if (this.labelSpeed.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetSpeed);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelSpeed.Text = text;
            }
        }

        internal void SetDatetime(string text)
        {
            if (this.labelDate.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetDatetime);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelDate.Text = text;
            }
        }

        internal void SetStatus(string text)
        {
            if (this.labelStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatus);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelStatus.Text = text;
            }
        }

        internal void SetSource(string text)
        {
            if (this.labelSource.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetSource);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelSource.Text = text;
            }
        }

        internal void SetAccuracy(string text)
        {
            if (this.labelAccuracy.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetAccuracy);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelAccuracy.Text = text;
            }
        }
    }
}
