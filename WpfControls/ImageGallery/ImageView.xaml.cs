using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Tom4u.Toolkit.WpfControls.Images
{
    public partial class ImageView : UserControl
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(ImageViewModel),
            typeof(ImageView));

        public ImageViewModel ViewModel
        {
            get => (ImageViewModel)GetValue(ViewModelProperty); 
            set => SetValue(ViewModelProperty, value);
        }

        public ImageView()
        {
            SetupViewModel(new ImageViewModel());
        }

        public ImageView(ImageViewModel viewModel)
        {
            SetupViewModel(viewModel);
        }

        private void SetupViewModel(ImageViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }
    }
}
