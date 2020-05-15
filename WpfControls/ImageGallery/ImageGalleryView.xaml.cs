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

using System;
using System.Reactive.Disposables;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ReactiveUI;
using Splat;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public partial class ImageGalleryView
    {
        private bool headerLoaded;

        public ImageGalleryView()
        {
            SetupView();
        }

        private double TabItemHeaderHeight { get; set; }

        public event EventHandler GalleryClosed;

        public event EventHandler<ImageViewModel> ImageClicked;

        private void SetupView()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            Locator.CurrentMutable.RegisterConstant(
                new EmptyStringImageSourceConverter(),
                typeof(IBindingTypeConverter));

            InitializeComponent();
            ViewModel = new ImageGalleryViewModel();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.MaxThumbnailSize,
                        view => view.SliderControl.Maximum)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.CurrentThumbnailSize,
                        view => view.SliderControl.Value)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.CurrentThumbnailSize,
                        view => view.CurrentThumbnailSizeControl.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.Categories,
                        view => view.CategoriesTabControl.ItemsSource)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        vm => vm.CloseGallery,
                        view => view.CloseButton)
                    .DisposeWith(disposables);

                ViewModel.CloseGallery.Subscribe(unit => GalleryClosed?.Invoke(this, EventArgs.Empty));
            });
        }

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue.Equals(e.NewValue)) return;

            UpdateImageSizes((int) e.NewValue);
        }

        private void UpdateImageSizes(int newSize)
        {
            if (ViewModel == null) return;

            foreach (var category in ViewModel.Categories)
            foreach (var image in category.Images)
                image.ImageSize = newSize;
        }

        private void CategoriesTabControl_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var source = e.OriginalSource as FrameworkElement;

            if (!(source?.DataContext is ImageViewModel)) return;

            ImageClicked?.Invoke(this, (ImageViewModel) source.DataContext);
        }

        private void TabItemHeaderTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            var textBlock = (TextBlock) sender;

            TabItemHeaderHeight = textBlock.ActualHeight;
            UpdateTabItemHeight();
            headerLoaded = true;
        }

        private void UpdateTabItemHeight()
        {
            if (CategoriesTabControl.Items.Count == 0 || !headerLoaded) return;

            if (CategoriesTabControl.SelectedIndex < 0) CategoriesTabControl.SelectedIndex = 0;

            var categoryViewModel = (ImagesCategoryViewModel) CategoriesTabControl.SelectedItem;
            var realHeight = GalleryPanel.ActualHeight - ActionDock.ActualHeight - TabItemHeaderHeight - 50;

            categoryViewModel.TabItemHeight = realHeight;
        }

        private void CategoriesTabControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.WidthChanged) return;

            UpdateTabItemHeight();
        }

        private void CategoriesTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTabItemHeight();
        }
    }
}