using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageMetadata = System.Windows.Media.ImageMetadata;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public partial class ImagesCategoryView : UserControl
    {
        private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel),
            typeof(ImagesCategoryViewModel),
            typeof(ImagesCategoryView),
            new PropertyMetadata(OnViewModelChanged));

        public ImagesCategoryViewModel ViewModel
        {
            get => (ImagesCategoryViewModel)GetValue(ViewModelProperty); 
            set => SetValue(ViewModelProperty, value);
        }

        public ImagesCategoryView()
        {
            SetupViewModel(new ImagesCategoryViewModel());
        }

        public ImagesCategoryView(ImagesCategoryViewModel viewModel)
        {
            SetupViewModel(viewModel);
        }

        private void SetupViewModel(ImagesCategoryViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.WriteLine($"{nameof(ImagesCategoryView)} view model changed");

            var me = (ImagesCategoryView) d;
            me.UpdateImages();
        }

        private void UpdateImages()
        {
            Debug.WriteLine("Updating images");

            ImagesPanel.Children.Clear();

            foreach (var image in ViewModel.Images)
                ImagesPanel
                    .Children
                    .Add(new ImageView(image));
        }
    }
}
