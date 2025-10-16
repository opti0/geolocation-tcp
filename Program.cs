using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpLib;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace GeolocationTCP
{
    class Program
    {
        private static TcpServer server;
        private static GeolocationProvider provider;

        [STAThread]
        static void Main()
        {

            CultureInfo systemCulture = CultureInfo.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = systemCulture;
            Thread.CurrentThread.CurrentUICulture = systemCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow w = new MainWindow();
            Locator locator = new Locator(w);
            provider = new GeolocationProvider();
            server = new TcpServer(provider, 15555);
            server.SetLocator(locator);
            server.Start();

            Application.Run(w);
        }
    }
}
