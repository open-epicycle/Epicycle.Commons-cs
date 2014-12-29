using Epicycle.Commons.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epicycle.Commons.Reporting
{
    public sealed class PeriodicReport : BasePeriodicThread
    {
        private readonly object _lock = new object();

        public readonly IFileSystem _fileSystem;
        public readonly FileSystemPath _reportFilePath;
        public readonly IList<ReporterDelegate> _reporters;

        public delegate void ReporterDelegate(IReport report);

        public PeriodicReport(IFileSystem fileSystem, FileSystemPath reportFilePath, double period_sec)
            : base(BasicMath.Round(period_sec * 1000), 0)
        {
            Thread.Priority = System.Threading.ThreadPriority.Lowest;

            _fileSystem = fileSystem;
            _reportFilePath = reportFilePath;
            _reporters = new List<ReporterDelegate>();
        }

        public new void Start()
        {
            base.Start();
        }

        public void RegisterReporter(ReporterDelegate reporter)
        {
            lock (_lock)
            {
                _reporters.Add(reporter);
            }
        }

        protected override void Iteration()
        {
            Report();
        }

        public void Report()
        {
            lock (_lock)
            {
                var report = new SimpleReport();

                foreach(var reporter in _reporters)
                {
                    reporter(report);
                }

                _fileSystem.WriteTextFile(_reportFilePath, GenerateReportText(report), append: true);
            }
        }
        
        private string GenerateReportText(SimpleReport report)
        {
            var result = new StringBuilder();

            result.AppendFormat("######## {0}\n", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            result.Append(report.Serialize(1));

            return result.ToString();
        }
    }
}
