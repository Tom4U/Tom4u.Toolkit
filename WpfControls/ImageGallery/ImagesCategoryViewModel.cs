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
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Tom4u.Toolkit.WpfControls.Common;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public class ImagesCategoryViewModel : AbstractViewModel
    {
        private string categoryName = "";

        private double tabItemHeight = 300;

        public ImagesCategoryViewModel()
        {
            ImagesCache = new SourceCache<ImageViewModel, string>(ivm => ivm.Path);
            Images = new ObservableCollectionExtended<ImageViewModel>();

            ImagesCache.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(Images)
                .Subscribe();
        }

        public string CategoryName
        {
            get => categoryName;
            set => this.RaiseAndSetIfChanged(ref categoryName, value);
        }

        public double TabItemHeight
        {
            get => tabItemHeight;
            set => this.RaiseAndSetIfChanged(ref tabItemHeight, value);
        }

        private SourceCache<ImageViewModel, string> ImagesCache { get; }
        public IObservableCollection<ImageViewModel> Images { get; }
    }
}