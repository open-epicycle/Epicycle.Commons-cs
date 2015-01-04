namespace Epicycle.Commons.Reporting
{
    public interface IReport : INumericReport
    {
        IReport SubReport(string name);
        void Report(string name, string value);
        void Report(string name, object value);
    }
}
