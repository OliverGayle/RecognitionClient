using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using MahApps.Metro.Controls.Dialogs;

namespace RecognitionClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CustomDialog _customDialog;
        private MainWindow _loginwindow;
        private Capture capture;
        private CascadeClassifier haarCascade;
        DispatcherTimer timer;
        BitmapSource[] TrainingImages = new BitmapSource[2];

        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            capture = new Capture();
            haarCascade = new CascadeClassifier(System.AppDomain.CurrentDomain.BaseDirectory + "/haarcascade_frontalface_default.xml"); ;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame().ToImage<Bgr, Byte>();
            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();

                var detectedFaces = haarCascade.DetectMultiScale(grayFrame, 1.1, 10);
                BitmapSource BmpInput = ToBitmapSource(grayFrame);
                Bitmap ExtractedFace;

                Bitmap extractCopy = grayFrame.ToBitmap();

                foreach (var face in detectedFaces)
                {

                    ExtractedFace = extractCopy.Clone(face, extractCopy.PixelFormat);
                    //currentFrame.Draw(face.rect, new Bgr(0, double.MaxValue, 0), 3);
                    currentFrame.Draw(face, new Bgr(256, 256, 256), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them
                    Image<Gray, Byte> normalizedMasterImage = new Image<Gray, Byte>(ExtractedFace);
                    BitmapSource trainingImage = ToBitmapSource(normalizedMasterImage);
                    if (TrainingImages[0] == null)
                    {
                        TrainingImages[0] = trainingImage;
                        TrainPreview.Source = TrainingImages[0];
                    }
                    else
                    {
                        TrainingImages[1] = trainingImage;
                    }

                }

                    image1.Source = ToBitmapSource(currentFrame);
              
                
            }

        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap) 
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop
                  .Imaging.CreateBitmapSourceFromHBitmap(
                  ptr,
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            ShowMaxRestoreButton = false;
            ShowMinButton = false;
            Loaded -= OnLoaded;
        }

        private void onClickNext(object sender, RoutedEventArgs e)
        {
            TrainingImages[0] = TrainingImages[1];
            TrainPreview.Source = TrainingImages[0];
        }

        private async void onClickTrain(object sender, RoutedEventArgs e)
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            _customDialog = new CustomDialog();
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",
            };
            _loginwindow = new Login1();
            _loginwindow.ButtonCancel.Click += ButtonCancelOnClick;
            _loginwindow.ButtonLogin.Click += ButtonLoginOnClick;
            _customDialog.Content = _loginwindow;
            await this.ShowMessageAsync("This is the title", "Some message");
            //TrainingImages[0]

        }
    }
}
