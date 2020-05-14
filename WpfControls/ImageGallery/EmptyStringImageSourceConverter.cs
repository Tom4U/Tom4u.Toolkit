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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ReactiveUI;
using Splat;

namespace Tom4u.Toolkit.WpfControls.ImageGallery
{
    public class EmptyStringImageSourceConverter : IValueConverter, IBindingTypeConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return value;
            }

            return string.IsNullOrWhiteSpace(value.ToString()) ? DependencyProperty.UnsetValue : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            return fromType == typeof(string) ? 0 : 1;
        }

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                result = Convert(from, toType, conversionHint, CultureInfo.CurrentCulture);
            }
            catch (Exception exception)
            {
                this.Log().Warn(exception, $"Couldn't convert object to type:  {toType}");
                result = "";
                return false;
            }

            return true;
        }
    }
}