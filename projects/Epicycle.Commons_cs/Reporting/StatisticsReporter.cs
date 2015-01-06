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

using Epicycle.Commons.Time;
using System.Collections.Generic;

namespace Epicycle.Commons.Reporting
{
    using System;

    public sealed class StatisticsReporter : IStatisticsReporter
    {
        private object _lock = new object();

        private readonly IClock _clock;
        private double _lastReportTime;
        private IList<KeyValuePair<string, object>> _entries;

        public StatisticsReporter(IClock clock)
        {
            _clock = clock;

            _entries = null;

            _lastReportTime = _clock.Time;
        }

        public IStatisticsReporter CreateSubReporter(string name)
        {
            var subReporter = new StatisticsReporter(_clock);

            AddInner(name, subReporter);

            return subReporter;
        }

        public void ReportEvent(string name)
        {
            ReportEvent(name, 1);
        }

        public void ReportEvent(string name, int amount)
        {
            lock(_lock)
            {
                var obj = GetValue(name);
                var eventReporter = (obj != null) ? (EventReporter)obj : (EventReporter)null;

                if (eventReporter == null)
                {
                    eventReporter = new EventReporter(name);
                    AddInner(name, eventReporter);
                }

                eventReporter.Record(amount);
            }
        }

        public void Report(string name, int value)
        {
            Report(name, (double)value);
        }

        public void Report(string name, long value)
        {
            Report(name, (double)value);
        }

        public void Report(string name, float value)
        {
            Report(name, (double)value);
        }

        public void Report(string name, double value)
        {
            lock (_lock)
            {
                var obj = GetValue(name);
                var eventReporter = (obj != null) ? (ParameterReporter)obj : (ParameterReporter)null;

                if (eventReporter == null)
                {
                    eventReporter = new ParameterReporter(name);
                    AddInner(name, eventReporter);
                }

                eventReporter.Record(value);
            }
        }

        public IDisposable Time(string name)
        {
            return new ReportingStopwatch(this, name);
        }

        private object GetValue(string name)
        {
            if (_entries != null)
            {
                foreach (var entry in _entries)
                {
                    if (entry.Key == name)
                    {
                        return entry.Value;
                    }
                }
            }

            return null;
        }

        private void AddInner(string name, object value)
        {
            if (_entries == null)
            {
                _entries = new List<KeyValuePair<string, object>>();
            }

            _entries.Add(new KeyValuePair<string, object>(name, value));
        }

        public void Report(IReport report)
        {
            lock (_lock)
            {
                var time = _clock.Time;
                var dt = time - _lastReportTime;
                _lastReportTime = time;

                if (_entries == null)
                {
                    return;
                }

                foreach (var entry in _entries)
                {
                    var name = entry.Key;
                    var obj = entry.Value;

                    if (obj is IReporter)
                    {
                        var reporter = (IReporter) obj;

                        reporter.Report(report, dt);
                    }
                    else if (obj is StatisticsReporter)
                    {
                        var subReporter = (StatisticsReporter)obj;

                        subReporter.Report(report.SubReport(name));
                    }
                }
            }
        }

        // TODO: Wrap the sub report with an inner object and use polymorphism

        private interface IReporter
        {
            void Report(IReport report, double dt_sec);
        }

        private sealed class EventReporter : IReporter
        {
            private readonly string _name;

            public EventReporter(string name)
            {
                _name = name;

                Count = 0;
            }

            public int Count { get; private set; }

            public void Record(int amount)
            {
                Count += amount;
            }

            public void Report(IReport report, double dt_sec)
            {
                var count = Count;
                Count = 0;

                report.Report(_name + "_COUNT", count);

                if(dt_sec > 0)
                {
                    report.Report(_name + "_FREQ", (count / dt_sec));
                }
            }
        }

        private sealed class ParameterReporter : IReporter
        {
            private readonly string _name;

            private int _count;
            private double _sum;
            private double _sumSquares;
            private double _min;
            private double _max;

            public ParameterReporter(string name)
            {
                _name = name;

                Reset();
            }

            private void Reset()
            {
                _count = 0;
                _sum = 0;
                _sumSquares = 0;
                _min = double.NaN;
                _max = double.NaN;
            }

            public void Record(double value)
            {
                _count++;
                _sum += value;
                _sumSquares += value * value;

                if (double.IsNaN(_min) || (value < _min))
                {
                    _min = value;
                }

                if (double.IsNaN(_max) || (value > _max))
                {
                    _max = value;
                }
            }

            public void Report(IReport report, double dt_sec)
            {
                var count = _count;
                var sum = _sum;
                var sumSquares = _sumSquares;
                var min = _min;
                var max = _max;
                Reset();

                report.Report(_name + "_COUNT", count);
                report.Report(_name + "_SUM", sum);

                if(!double.IsNaN(min))
                {
                    report.Report(_name + "_MIN", min);
                }

                if (count > 0)
                {
                    var avg = sum / count;

                    report.Report(_name + "_AVG", avg);

                    if (count > 1)
                    {
                        var std = Math.Sqrt((sumSquares / count - avg * avg) * (count / (count - 1)));
                        report.Report(_name + "_STD", std);
                    }
                }

                if (!double.IsNaN(max))
                {
                    report.Report(_name + "_MAX", max);
                }
            }
        }
    }
}
