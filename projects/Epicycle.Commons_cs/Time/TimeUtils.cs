using System;

namespace Epicycle.Commons.Time
{
    public static class TimeUtils
    {
        public static double GetMillisecondsSinceUnixEpoch(DateTime time)
        {
            return time.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
