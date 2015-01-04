using System;

namespace Epicycle.Commons.Reporting
{
    public interface INumericReport
    {
        void Report(string name, int value);
        void Report(string name, long value);
        void Report(string name, float value);
        void Report(string name, double value);
        IDisposable Time(string name);
    }
}
