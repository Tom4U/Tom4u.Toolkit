using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tom4u.Toolkit.WpfControls.Images;
using Path = System.IO.Path;

namespace WpfControls.Demo.Views
{
    public partial class ImageGalleryPage : Page
    {
        public ImageGalleryPage()
        {
            InitializeComponent();
            var gallery = new ImageGalleryView(GetSimulatedViewModel());
            gallery.ImageClicked += ImageGalleryView_OnImageClicked;
            gallery.GalleryClosed += ImageGalleryView_OnGalleryClosed;
            MainGrid.Children.Add(gallery);
        }

        private void ImageGalleryView_OnImageClicked(object sender, ImageViewModel e)
        {
            MessageBox.Show($"{e.Title} clicked");
        }

        private void ImageGalleryView_OnGalleryClosed(object sender, EventArgs e)
        {
            if (NavigationService == null || !NavigationService.CanGoBack) return;

            NavigationService?.GoBack();
        }

        private static ImageGalleryViewModel GetSimulatedViewModel()
        {
            var viewModel = new ImageGalleryViewModel();

            for (var i = 1; i < 3; i++)
                viewModel.Categories.Add(GetSimulatedCategory($"Category {i}", viewModel.CurrentThumbnailSize));

            return viewModel;
        }

        private static ImagesCategoryViewModel GetSimulatedCategory(string categoryName, int defaultImageSize)
        {
            var viewModel = new ImagesCategoryViewModel { CategoryName = categoryName };
            var images = new List<ImageViewModel>();

            for (var i = 1; i < 10; i++)
            {
                var image = GetSimulatedImage(defaultImageSize, $"Image{i}");
                images.Add(image);
            }

            viewModel.Images = new ObservableCollection<ImageViewModel>(images);

            return viewModel;
        }

        private static ImageViewModel GetSimulatedImage(int defaultSize, string imageTitle)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = System.IO.Path.GetDirectoryName(assembly.CodeBase) ?? string.Empty;
            var filePath = Path.Combine(path, "Images", "DummyImage.png");
            var viewModel = new ImageViewModel()
            {
                Title = imageTitle,
                ImageSize = defaultSize,
                Path = filePath,
                Tags = ""
            };

            return viewModel;
        }
    }
}
