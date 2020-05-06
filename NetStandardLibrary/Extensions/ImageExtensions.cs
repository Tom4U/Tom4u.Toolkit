using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tom4u.Toolkit.NetStandardLibrary.Extensions
{
    public static class ImageExtensions
    {
        internal static Image ResizeKeepAspect(this Image image, int maxWidth, int maxHeight, bool enlarge = false)
        {
            maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, image.Width);
            maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, image.Height);

            var rnd = Math.Min(maxWidth / (decimal)image.Width, maxHeight / (decimal)image.Height);
            var newSize = new Size((int)Math.Round(image.Width * rnd), (int)Math.Round(image.Height * rnd));
            var newImage = (Image) new Bitmap(image, newSize);

            return newImage;
        }

        internal static Image ResizeKeepAspect(this Image image, int maxSize, bool enlarge = false)
        {
            return ResizeKeepAspect(image, maxSize, maxSize, enlarge);
        }
    }
}
