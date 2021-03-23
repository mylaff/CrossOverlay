using System.Windows;
using System.IO;
using System.Reflection;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;


namespace MyCrossProject
{
    public partial class MainWindow : Window
    {
        private class CrossHairSettings
        {
            public float Height { get => 20 * HeightMultiplier; }

            public float HeightMultiplier { get; set; }

            private float opacity;
            public float Opacity { 
                get => opacity; 
                set
                {
                    if (0 <= value && value <= 1)
                        opacity = value;
                    else
                        opacity = 1;
                } 
            }

            public string RelPath { get; set; }

            public string FullPath { get => GetCrosshairPath(RelPath);}

            public BitmapImage Source { get => new BitmapImage(new Uri(FullPath, UriKind.Absolute));  }

            public double MarginLeft { get; set; }

            public double MarginRight { get; set; }

            public double MarginTop { get; set; }

            public double MarginBottom { get; set; }

            public Thickness Margin { get => new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);  }

            private string GetCrosshairPath(string relPath = "xhair.png")
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relPath);
            }

            public CrossHairSettings()
            {
                HeightMultiplier = float.Parse(ConfigurationManager.AppSettings["scaleMultiplier"]);
                Opacity = float.Parse(ConfigurationManager.AppSettings["crosshairOpacity"]);
                RelPath = ConfigurationManager.AppSettings["crosshairRelPath"];

                MarginLeft = double.Parse(ConfigurationManager.AppSettings["crosshairMarginLeft"]);
                MarginRight = double.Parse(ConfigurationManager.AppSettings["crosshairMarginRight"]);
                MarginTop = double.Parse(ConfigurationManager.AppSettings["crosshairMarginTop"]);
                MarginBottom = double.Parse(ConfigurationManager.AppSettings["crosshairMarginBottom"]);
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            var crossSettings = new CrossHairSettings();

            Crosshair.Source = crossSettings.Source;
            Crosshair.Height = crossSettings.Height;
            Crosshair.Opacity = crossSettings.Opacity;
            Crosshair.Margin = crossSettings.Margin;

            Console.WriteLine(Crosshair.Source.ToString());
        }

        public static class WindowsServices
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = (-20);

            [DllImport("user32.dll")]
            static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            public static void SetWindowExTransparent(IntPtr hwnd)
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
        }
    }
}
