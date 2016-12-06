using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;

namespace RecognitionClient
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen 
    {
        public LoginScreen()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1");
                dynamic obj = JObject.Parse(json);
                var bingURL = obj["images"][0]["url"];

                ImageBrush background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("http://www.bing.com/" + bingURL, UriKind.Absolute)),
                    Stretch = Stretch.UniformToFill
                };


                this.Background = background;

            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void LoginWindowLoaded(object sender, RoutedEventArgs e)
        {
            Window window = (Window)sender;
            DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(2));
            window.BeginAnimation(Window.OpacityProperty, animation);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToShortTimeString();
            lblDate.Content = DateTime.Now.DayOfWeek + ", " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day;
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            ShowMaxRestoreButton = false;
            ShowMinButton = false;
            Loaded -= OnLoaded;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            MainMenu MainWindow = new MainMenu();
            App.Current.MainWindow = MainWindow;
            MainWindow.Show();
            this.Close();





        }
    }
    public class JSONimages
    {
        public string url { get; set; }
    }
}
