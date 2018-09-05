namespace Alpha.Services.Extensions
{
    using System;

    public static class LongExtensions
    {
        public static DateTime ToLocalTime(this long value)
            => DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime.ToLocalTime();
    }
}