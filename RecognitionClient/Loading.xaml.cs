using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecognitionClient
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        public Loading()
        {
            worker.RunWorkerAsync();
            InitializeComponent();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

        }
        private void loadCameraScreen()
        {
            
            
        }
    private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
            MainWindow tempWindow = new MainWindow();
            tempWindow.Activate();
            App.Current.MainWindow = tempWindow;
            tempWindow.Show();
        }

    private void worker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
    {
            this.Close();
    }

}
}
