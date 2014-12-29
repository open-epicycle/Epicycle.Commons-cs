using System;

namespace Epicycle.Commons.Time
{
    public sealed class SystemClock : IClock
    {
        public double Time
        {
            get { return Environment.TickCount / 1000.0; }
        }
    }
}
