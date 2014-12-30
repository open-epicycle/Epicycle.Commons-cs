using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epicycle.Commons.Reporting
{
    internal sealed class SimpleReport : IReport
    {
        public static readonly string Indentation = "    ";

        private IList<KeyValuePair<string, object>> _entries;

        public SimpleReport()
        {
            _entries = null;
        }

        public IReport SubReport(string name)
        {
            var subReport = new SimpleReport();

            ReportInner(name, subReport);

            return subReport;
        }

        public void Report(string name, int value)
        {
            ReportInner(name, value);
        }

        public void Report(string name, long value)
        {
            ReportInner(name, value);
        }

        public void Report(string name, float value)
        {
            ReportInner(name, value);
        }

        public void Report(string name, double value)
        {
            ReportInner(name, value);
        }

        public void Report(string name, string value)
        {
            ReportInner(name, value);
        }

        public void Report(string name, object value)
        {
            ReportInner(name, value);
        }

        public IDisposable Time(string name)
        {
            return new Stopwatch(this, name);
        }

        private void ReportInner(string name, object value)
        {
            if(_entries == null)
            {
                _entries = new List<KeyValuePair<string, object>>();
            }

            _entries.Add(new KeyValuePair<string, object>(name, value));
        }

        public string Serialize(int level)
        {
            if(_entries == null || _entries.Count == 0)
            {
                return "";
            }

            var prefix = String.Concat(Enumerable.Repeat(Indentation, level));

            var result = new StringBuilder();

            foreach(var entry in _entries)
            {
                result.Append(prefix);

                var name = entry.Key;
                var value = entry.Value;

                if (value is SimpleReport)
                {
                    var subReporter = (SimpleReport)value;

                    result.Append(String.Format("{0}:\n", name));
                    result.Append(subReporter.Serialize(level + 1));
                }
                else
                {
                    WriteValue(result, name, value);
                }
            }

            return result.ToString();
        }

        private void WriteValue(StringBuilder result, string name, object value)
        {
            result.Append(String.Format("{0}: {1}\n", name, value.ToString()));
        }

        private sealed class Stopwatch : IDisposable
        {
            private IReport _report;
            private string _name;
            private System.Diagnostics.Stopwatch _stopwatch;

            public Stopwatch(IReport report, string name)
            {
                _report = report;
                _name = name;

                _stopwatch = new System.Diagnostics.Stopwatch();
                _stopwatch.Start();
            }

            public void Dispose()
            {
                _stopwatch.Stop();

                var dt_sec = ((double)_stopwatch.ElapsedTicks) / System.Diagnostics.Stopwatch.Frequency;

                _report.Report(_name, dt_sec);
            }
        }
    }
}
