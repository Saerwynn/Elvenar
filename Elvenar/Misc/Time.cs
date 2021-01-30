using System;

namespace Elvenar.Misc
{
    public static class DateTimeExtensions
    {
        public static long GetTime(this DateTime date)
        {
            return (long)new TimeSpan(date.ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks).TotalMilliseconds;
        }
    }

    public class Tid 
    { 
        public static Random _random;
        public static Random Random
        {
            get
            {
                if (_random == null)
                    _random = new Random();

                return _random;
            }
        }

        public static string CreateTid()
        {
            return DateTime.Now.GetTime().ToString() + '-' + Random.Next(10000, 99999).ToString();
        }
    }
}
