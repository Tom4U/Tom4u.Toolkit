using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tom4u.Toolkit.NetStandardLibrary.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts date string into DateTime
        /// </summary>
        /// <param name="dateString">Date string in format YYYY-MM-DD</param>
        /// <returns>DateTime | NULL</returns>
        public static DateTime? InternationalDateToDateTime(this string dateString)
        {
            if (dateString == null) return null;

            var parts = dateString.Split('-');

            if (parts?.Length != 3)
                throw new FormatException(Strings.DateStringFormat_YYYY_MM_DD_Expected);

            var year = int.Parse(parts[0], NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat);
            var month = int.Parse(parts[1], CultureInfo.CurrentCulture.NumberFormat);
            var day = int.Parse(parts[2], CultureInfo.CurrentCulture.NumberFormat);

            return new DateTime(year, month, day);
        }

        public static string ToFirstLetterUppercase(this string value)
        {
            if (value == null) return string.Empty;

            var parts = value.ToCharArray();
            var firstLetter = parts[0].ToString().ToUpper(CultureInfo.CurrentCulture);
            var otherParts = value.TrimStart(parts[0]);

            return $"{firstLetter}{otherParts}";
        }
    }
}
