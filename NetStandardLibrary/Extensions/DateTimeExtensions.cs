using System;
using System.Collections.Generic;
using System.Text;

namespace Tom4u.Toolkit.NetStandardLibrary.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsYesterdayOrOlderThan(this DateTime date, DateTime comparingDate)
        {
            if (date.Year < comparingDate.Year)
                return true;

            if (date.Month < comparingDate.Month)
                return true;

            return date.Day < comparingDate.Day;
        }

        public static string ToInternationalDateString(this DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day}";
        }
    }
}
