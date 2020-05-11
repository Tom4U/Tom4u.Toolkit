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
using MaterialDesignThemes.Wpf;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public partial class ImageGalleryView : UserControl
    {
        public event EventHandler GalleryClosed; 
        public event EventHandler<ImageViewModel> ImageClicked; 

        public ImageGalleryViewModel ViewModel { get; set; }

        public int DialogWidth
        {
            get
            {
                var parentWidth = GalleryDialogHost.ActualWidth;
                return (int) (parentWidth - 150);
            }
        }

        public int DialogHeight
        {
            get
            {
                var parentHeight = GalleryDialogHost.ActualHeight;
                return (int)(parentHeight - 200);
            }
        }

        public ImageGalleryView()
        {
            SetupView(new ImageGalleryViewModel());
        }

        public ImageGalleryView(ImageGalleryViewModel viewModel)
        {
            SetupView(viewModel);
        }

        private void SetupView(ImageGalleryViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
            SizeChanged += (sender, args) =>
            {
                CategoriesTabControl.Visibility = Visibility.Collapsed;
                CategoriesTabControl.Items.Clear();
                CategoriesTabControl_OnLoaded(this, null);
                CategoriesTabControl.Visibility = Visibility.Visible;
            };
        }

        private void GalleryDialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            GalleryClosed?.Invoke(this, EventArgs.Empty);
        }

        private void CategoriesTabControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (CategoriesTabControl.Items.Count > 0) return;

            foreach (var category in ViewModel.Categories)
            {
                var tabItem = new TabItem()
                {
                    Header = category.CategoryName,
                    Content = new ImagesCategoryView(category)
                    {
                        Width = DialogWidth,
                        Height = DialogHeight - ActionDock.ActualHeight - 50
                    }
                };

                CategoriesTabControl.Items.Add(tabItem);
            }
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ViewModel == null) return;

            foreach (var category in ViewModel.Categories)
            {
                foreach (var image in category.Images)
                {
                    image.ImageSize = (int)e.NewValue;
                }
            }
        }

        private void CategoriesTabControl_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;

            if (source?.DataContext == null) return;

            if (!(source.DataContext is ImageViewModel)) return;

            ImageClicked?.Invoke(this, (ImageViewModel) source.DataContext);
        }
    }
}
