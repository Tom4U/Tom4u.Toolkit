// Tom4u.Toolkit
// Copyright (C) 2020  Thomas Ohms
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public partial class ImageGalleryView
    {
        public ImageGalleryView()
        {
            SetupView(new ImageGalleryViewModel());
        }

        public ImageGalleryView(ImageGalleryViewModel viewModel)
        {
            SetupView(viewModel);
        }

        private ImageGalleryViewModel ViewModel { get; set; }

        private int DialogWidth
        {
            get
            {
                var parentWidth = GalleryDialogHost.ActualWidth;
                return (int)(parentWidth - 150);
            }
        }

        private int DialogHeight
        {
            get
            {
                var parentHeight = GalleryDialogHost.ActualHeight;
                return (int)(parentHeight - 200);
            }
        }

        public event EventHandler GalleryClosed;
        public event EventHandler<ImageViewModel> ImageClicked;

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
            if (CategoriesTabControl.Items.Count > 0)
            {
                return;
            }

            foreach (var category in ViewModel.Categories)
            {
                var tabItem = new TabItem
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
            if (ViewModel == null)
            {
                return;
            }

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

            if (!(source?.DataContext is ImageViewModel))
            {
                return;
            }

            ImageClicked?.Invoke(this, (ImageViewModel)source.DataContext);
        }
    }
}