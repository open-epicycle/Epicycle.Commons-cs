using System;

namespace Epicycle.Commons.Reporting
{
    public sealed class ReportingStopwatch : IDisposable
    {
        private INumericReport _report;
        private string _name;
        private System.Diagnostics.Stopwatch _stopwatch;

        public ReportingStopwatch(INumericReport report, string name)
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
