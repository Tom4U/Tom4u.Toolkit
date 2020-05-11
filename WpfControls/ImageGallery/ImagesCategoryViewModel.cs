using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tom4u.Toolkit.WpfControls.Common;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public class ImagesCategoryViewModel : AbstractViewModel
    {
        public string CategoryName
        {
            get => GetValue("");
            set => SetValue(value);
        }

        public ObservableCollection<ImageViewModel> Images
        {
            get => GetValue(new ObservableCollection<ImageViewModel>()); 
            set => SetValue(value);
        }
    }
}
