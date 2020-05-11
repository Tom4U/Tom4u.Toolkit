using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tom4u.Toolkit.WpfControls.Common;
using Tom4u.Toolkit.WpfControls.Properties;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public class ImageGalleryViewModel : AbstractViewModel
    {
        public int MaxThumbnailSize
        {
            get => GetValue(500);
            set => SetValue(value);
        }

        public int CurrentThumbnailSize
        {
            get => GetValue(200);
            set => SetValue(value);
        }

        public ObservableCollection<ImagesCategoryViewModel> Categories
        {
            get => GetValue(new ObservableCollection<ImagesCategoryViewModel>());
            set => SetValue(value);
        }
    }
}
