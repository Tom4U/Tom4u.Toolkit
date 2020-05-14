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

using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using Tom4u.Toolkit.WpfControls.Common;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public class ImageViewModel : AbstractViewModel
    {
        private string title = "";
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        private string path = "";
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        private string tags = "";
        public string Tags
        {
            get => tags;
            set => this.RaiseAndSetIfChanged(ref tags, value);
        }

        private int imageSize = 150;
        public int ImageSize
        {
            get => imageSize;
            set => this.RaiseAndSetIfChanged(ref imageSize, value);
        }

        public ReactiveCommand<ImageViewModel, Unit> SelectImage { get; set; }
    }
}