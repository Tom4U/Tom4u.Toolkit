using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tom4u.Toolkit.WpfControls.Common;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public class ImageViewModel : AbstractViewModel
    {
        public string Title
        {
            get => GetValue("");
            set => SetValue(value);
        }

        public string Path
        {
            get => GetValue(""); 
            set => SetValue(value);
        }

        public string Tags
        {
            get => GetValue(""); 
            set => SetValue(value);
        }

        public int ImageSize
        {
            get => GetValue(150);
            set => SetValue(value);
        }

        public ICommand SelectImage { get; set; }
    }
}
