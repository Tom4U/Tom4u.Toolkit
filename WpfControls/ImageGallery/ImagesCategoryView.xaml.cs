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

using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public partial class ImagesCategoryView
    {
        //private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        //    nameof(ViewModel),
        //    typeof(ImagesCategoryViewModel),
        //    typeof(ImagesCategoryView));
        
        //public ImagesCategoryViewModel ViewModel
        //{
        //    get => (ImagesCategoryViewModel)GetValue(ViewModelProperty);
        //    set => SetValue(ViewModelProperty, value);
        //}

        public ImagesCategoryView()
        {
            InitializeComponent();
            ViewModel = new ImagesCategoryViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                        vm => vm.Images,
                        view => view.ImagePanel.ItemsSource)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.TabItemHeight,
                        view => view.Height)
                    .DisposeWith(disposables);
            });
        }

        //private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    Debug.WriteLine($"{nameof(ImagesCategoryView)} view model changed");

        //    var me = (ImagesCategoryView)d;
        //    me.UpdateImages();
        //}

        //private void UpdateImages()
        //{
        //    //var stopwatch = new Stopwatch();
        //    //stopwatch.Start();
        //    //Debug.WriteLine("Updating images");

        //    //var imageViews = ViewModel.Images.Select(i => new ImageView(i));

        //    //ImagesPanel.Children= imageViews;

        //    //foreach (var image in ViewModel.Images)
        //    //{
        //    //    ImagesPanel
        //    //        .Children
        //    //        .Add(new ImageView(image));
        //    //}

        //    //stopwatch.Stop();
        //    //Debug.WriteLine($"Updating images in {stopwatch.Elapsed.Seconds} seconds");
        //}
    }
}