// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

using System;

namespace Epicycle.Commons.Time
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
