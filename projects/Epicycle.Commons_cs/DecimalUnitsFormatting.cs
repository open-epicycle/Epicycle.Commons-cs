// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
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
using System.Collections.Generic;

namespace Epicycle.Commons
{
    public static class DecimalUnitsFormatting
    {
        public static readonly Unit[] MetricDistanceShort = GenerateUnits("m", isFull: false, includeCenti: true);
        public static readonly Unit[] MetricDistanceFull = GenerateUnits("m", isFull: true, includeCenti: true);
        public static readonly Unit[] MetricTimeFull = GenerateUnits("s", isFull: true, includeCenti: false);

        public static Unit[] GenerateUnits(string unitLabel, bool isFull, bool includeCenti)
        {
            var result = new List<Unit>();

            if(isFull)
            {
                result.Add(new Unit(-24, "y" + unitLabel));
                result.Add(new Unit(-21, "z" + unitLabel));
                result.Add(new Unit(-18, "a" + unitLabel));
                result.Add(new Unit(-15, "f" + unitLabel));
                result.Add(new Unit(-12, "p" + unitLabel));
            }
            result.Add(new Unit(-9, "n" + unitLabel));
            result.Add(new Unit(-6, "u" + unitLabel));
            result.Add(new Unit(-3, "m" + unitLabel));

            if(includeCenti)
            {
                result.Add(new Unit(-2, "c" + unitLabel));
            }

            result.Add(new Unit(0, unitLabel));
            result.Add(new Unit(3, "k" + unitLabel));

            if(isFull)
            {
                result.Add(new Unit(6, "M" + unitLabel));
                result.Add(new Unit(9, "G" + unitLabel));
                result.Add(new Unit(12, "T" + unitLabel));
                result.Add(new Unit(15, "P" + unitLabel));
                result.Add(new Unit(18, "E" + unitLabel));
                result.Add(new Unit(21, "Z" + unitLabel));
                result.Add(new Unit(24, "Y" + unitLabel));
            }

            return result.ToArray();
        }

        public static string Format(double value, Unit[] units, bool fractions)
        {
            var log = Math.Log10(value);

            int unitIndex = units.Length - 1;
            for(var i = 1; i < units.Length; i++)
            {
                if(units[i].Factor > log)
                {
                    unitIndex = i - 1;
                    break;
                }
            }

            return Format(value, units[unitIndex], fractions);
        }

        public static string Format(double value, Unit unit, bool fractions)
        {
            var unitValue = value / Math.Pow(10, unit.Factor);

            var format = fractions ? "{0:0.#}{1}" : "{0:0}{1}";

            return string.Format(format, unitValue, unit.Label);
        }

        public struct Unit
        {
            private readonly int _factor;
            private readonly string _label;

            public Unit(int factor, string label)
            {
                _factor = factor;
                _label = label;
            }

            public int Factor
            {
                get { return _factor; }
            }

            public string Label
            {
                get { return _label; }
            }
        }
    }
}
