using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Tom4u.Toolkit.WpfControls.Images;
using WpfControls.Demo.Views;
using Path = System.IO.Path;

namespace WpfControls.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGalleryWindow_OnClick(object sender, RoutedEventArgs e)
        {
            ElementsFrame.Navigate(new ImageGalleryPage());
        }
    }
}
