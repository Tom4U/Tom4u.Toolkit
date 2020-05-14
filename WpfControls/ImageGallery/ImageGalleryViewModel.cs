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
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Tom4u.Toolkit.WpfControls.Common;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public class ImageGalleryViewModel : AbstractViewModel
    {
        private int currentThumbnailSize = 120;
        private int maxThumbnailSize = 500;

        public ImageGalleryViewModel()
        {
            CategoriesCache = new SourceCache<ImagesCategoryViewModel, string>(icvm => icvm.CategoryName);
            Categories = new ObservableCollectionExtended<ImagesCategoryViewModel>();

            CategoriesCache.Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(Categories)
                .Subscribe();

            CloseGallery = ReactiveCommand.Create(() => { });
        }

        public int MaxThumbnailSize
        {
            get => maxThumbnailSize;
            set => this.RaiseAndSetIfChanged(ref maxThumbnailSize, value);
        }

        public int CurrentThumbnailSize
        {
            get => currentThumbnailSize;
            set => this.RaiseAndSetIfChanged(ref currentThumbnailSize, value);
        }

        private SourceCache<ImagesCategoryViewModel, string> CategoriesCache { get; }
        public IObservableCollection<ImagesCategoryViewModel> Categories { get; }

        public ReactiveCommand<Unit, Unit> CloseGallery { get; }
    }
}