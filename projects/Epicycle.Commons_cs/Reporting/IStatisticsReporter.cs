namespace Epicycle.Commons.Reporting
{
    public interface IStatisticsReporter : INumericReport
    {
        IStatisticsReporter CreateSubReporter(string name);

        void ReportEvent(string name);
        void ReportEvent(string name, int amount);

        void Report(IReport report);
    }
}
