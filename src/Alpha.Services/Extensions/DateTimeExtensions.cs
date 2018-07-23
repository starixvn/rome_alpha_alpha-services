namespace Alpha.Services.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
            => new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime GetGreaterDate(this DateTime firstDate, DateTime secondDate)
            => firstDate < secondDate ? secondDate : firstDate;
    }
}