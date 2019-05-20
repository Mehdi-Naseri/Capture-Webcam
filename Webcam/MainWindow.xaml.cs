using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AForge.Video;
using AForge.Video.DirectShow;
//using System.draw


namespace Webcam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        System.Drawing.Bitmap  Bitmap1;
        private FilterInfoCollection FilterInfoCollection1;
        private VideoCaptureDevice VideoCaptureDevice1;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FilterInfoCollection1 = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Vidoedevice in FilterInfoCollection1)
            {
                comboBox1.Items.Add(Vidoedevice.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VideoCaptureDevice1 = new VideoCaptureDevice(FilterInfoCollection1[comboBox1.SelectedIndex].MonikerString);
            VideoCaptureDevice1.NewFrame += new NewFrameEventHandler(Vidoedevice_newframe);
            VideoCaptureDevice1.Start();
        }

        void Vidoedevice_newframe(object sender, NewFrameEventArgs eventargs)
        {
            Bitmap1 = (System.Drawing.Bitmap)eventargs.Frame.Clone();
            System.Windows.Forms.PictureBox PictureBox1 = (System.Windows.Forms.PictureBox)windowsFormsHost1.Child;
            PictureBox1.Image = Bitmap1;
       }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoCaptureDevice1!=null)
            {
            VideoCaptureDevice1.Stop();
            VideoCaptureDevice1 = new VideoCaptureDevice(FilterInfoCollection1[comboBox1.SelectedIndex].MonikerString);
            VideoCaptureDevice1.NewFrame += new NewFrameEventHandler(Vidoedevice_newframe);
            VideoCaptureDevice1.Start();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (VideoCaptureDevice1.IsRunning)
            {
                VideoCaptureDevice1.Stop();
            }
        }

    }
}
