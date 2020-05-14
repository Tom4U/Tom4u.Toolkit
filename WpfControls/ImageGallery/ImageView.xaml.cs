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

using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public partial class ImageView
    {
        //private static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        //    nameof(ViewModel),
        //    typeof(ImageViewModel),
        //    typeof(ImageView));

        public ImageView()
        {
            InitializeComponent();
            ViewModel = new ImageViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                        vm => vm.Path,
                        view => view.ImageControl.Source)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.ImageSize,
                        view => view.ImageControl.Width)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.ImageSize,
                        view => view.ImageControl.Height)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.Tags,
                        view => view.Tag)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        vm => vm.Title,
                        view => view.ImageTitleControl.Text)
                    .DisposeWith(disposables);
            });
        }

        //public ImageViewModel ViewModel
        //{
        //    get => (ImageViewModel)GetValue(ViewModelProperty);
        //    set => SetValue(ViewModelProperty, value);
        //}
    }
}