namespace Epicycle.Commons.Reporting
{
    public interface IReport
    {
        IReport SubReport(string name);
        void Report(string name, int value);
        void Report(string name, long value);
        void Report(string name, float value);
        void Report(string name, double value);
        void Report(string name, string value);
        void Report(string name, object value);
    }
}
