using System;

namespace Epicycle.Commons.Reporting
{
    public interface IStatisticsReporter
    {
        IStatisticsReporter CreateSubReporter(string name);

        void ReportEvent(string name);
        void ReportEvent(string name, int amount);

        void ReportParameter(string name, int value);
        void ReportParameter(string name, double value);

        IDisposable Time(string name);

        void Report(IReport report);
    }
}
