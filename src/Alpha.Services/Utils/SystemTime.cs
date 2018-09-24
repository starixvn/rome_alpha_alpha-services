using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Apha.Services.Tests")]

namespace Alpha.Services.Utils
{
    public static class SystemTime
    {
        [ThreadStatic]
        private static DateTime? today;

        public static DateTime Today
        {
            get { return today ?? DateTime.Today; }
            internal set { today = value; }
        }
    }
}