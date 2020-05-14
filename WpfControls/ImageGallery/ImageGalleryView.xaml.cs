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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using ReactiveUI;
using Splat;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public partial class ImageGalleryView
    {
        private bool headerLoaded;

        //public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        //    nameof(ViewModel),
        //    typeof(ImageGalleryViewModel),
        //    typeof(ImageGalleryView));

        public double TabItemHeaderHeight { get; private set; }

        public ImageGalleryView()
        {
            SetupView();
        }

        //public ImageGalleryViewModel ViewModel
        //{
        //    get => (ImageGalleryViewModel) GetValue(ViewModelProperty); 
        //    set => SetValue(ViewModelProperty, value);
        //}

        public event EventHandler GalleryClosed;
        public event EventHandler<ImageViewModel> ImageClicked;
        //public event EventHandler<TabItem> CategoryTabItemLoaded;

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

            //SizeChanged += (sender, args) =>
            //{
            //    CategoriesTabControl.Visibility = Visibility.Collapsed;
            //    CategoriesTabControl.Items.Clear();
            //    CategoriesTabControl_OnLoaded(this, null);
            //    CategoriesTabControl.Visibility = Visibility.Visible;
            //};

            //CategoryTabItemLoaded += (sender, item) =>
            //{
            //    CategoriesTabControl.Items.Add(item);
            //};
        }

        //private void CategoriesTabControl_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    if (CategoriesTabControl.Items.Count > 0)
        //    {
        //        return;
        //    }

        //    var stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    var categories = ViewModel.Categories.ToArray();
        //    Debug.WriteLine("Adding categories to tabs");

        //    var firstTabItem = CategoryToTabItem(categories.First(), true);

        //    CategoryTabItemLoaded?.Invoke(this, firstTabItem);
        //    Debug.WriteLine($"First tab created after {stopwatch.Elapsed.Seconds} seconds");

        //    AddCategoriesToTabControl(categories.Skip(1));

        //    stopwatch.Stop();
        //    Debug.WriteLine($"Adding categories in {stopwatch.Elapsed.Seconds} seconds");
        //}

        //private void AddCategoriesToTabControl(IEnumerable<ImagesCategoryViewModel> categories)
        //{
        //    foreach (var category in categories)
        //    {
        //        var tabItem = CategoryToTabItem(category, false);
        //        CategoryTabItemLoaded?.Invoke(this, tabItem);
        //    }
        //}

        //private TabItem CategoryToTabItem(ImagesCategoryViewModel category, bool isSelected)
        //{
        //    var tabItem = new TabItem
        //    {
        //        Header = category.CategoryName,
        //        Content = new ImagesCategoryView(category)
        //        {
        //            Width = GalleryPanel.ActualWidth - 60,
        //            Height = GalleryPanel.ActualHeight - ActionDock.Height - 200
        //        },
        //        IsSelected = isSelected
        //    };
        //    return tabItem;
        //}

        private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue.Equals(e.NewValue)) return;

            UpdateImageSizes((int)e.NewValue);
        }

        private void UpdateImageSizes(int newSize)
        {
            if (ViewModel == null)
            {
                return;
            }

            foreach (var category in ViewModel.Categories)
            {
                foreach (var image in category.Images)
                {
                    image.ImageSize = newSize;
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

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            GalleryClosed?.Invoke(this, EventArgs.Empty);
        }

        private void TabItemHeaderTextBlock_OnLoaded(object sender, RoutedEventArgs e)
        {
            var textBlock = (TextBlock)sender;

            TabItemHeaderHeight = textBlock.ActualHeight;
            UpdateTabItemHeight();
            headerLoaded = true;
        }

        private void UpdateTabItemHeight()
        {
            if (CategoriesTabControl.Items.Count == 0 || !headerLoaded) return;

            if (CategoriesTabControl.SelectedIndex < 0)
                CategoriesTabControl.SelectedIndex = 0;

            var categoryViewModel = (ImagesCategoryViewModel) CategoriesTabControl.SelectedItem;
            var realHeight = GalleryPanel.ActualHeight - ActionDock.ActualHeight - TabItemHeaderHeight - 50;

            categoryViewModel.TabItemHeight = realHeight;
        }

        private void GalleryPanel_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!headerLoaded) return;
            UpdateTabItemHeight();
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