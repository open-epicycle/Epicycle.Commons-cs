using System;

namespace Epicycle.Commons
{
    public static class TimeFormatting
    {
        public static string FormatTimeDurationInRoundMinutes(double dt)
        {
            var minutes = BasicMath.Round(dt / 60);

            return String.Format("{0} min", minutes);
        }

        public static string FormatTimeDurationFlexible(double dt)
        {
            if(dt < 60)
            {
                return DecimalUnitsFormatting.Format(dt, DecimalUnitsFormatting.MetricTimeFull, true);
            }

            var secs = BasicMath.Round(dt);

            var secPart = secs % 60;
            var mins = secs / 60;

            if(mins < 60)
            {
                return string.Format("{0:00}:{1:00}", mins, secPart);
            }

            var minPart = mins % 60;
            var hours = mins / 60;

            if(hours < 24)
            {
                return string.Format("{0:00}:{1:00}:{2:00}", hours, minPart, secPart);
            }

            var days = ((double)hours) / 24.0;

            return string.Format("{0:0.#}d", days);
        }
    }
}
