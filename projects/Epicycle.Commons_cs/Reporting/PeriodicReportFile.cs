// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
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

using Epicycle.Commons.FileSystem;
using Epicycle.Commons.Time;
using System.Collections.Generic;

namespace Epicycle.Commons.Reporting
{
    public sealed class PeriodicReportFile
    {
        private readonly object _lock = new object();

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFileSystem _fileSystem;
        private readonly FileSystemPath _reportFilePath;
        private readonly List<ReporterDelegate> _reporters;

        public delegate void ReporterDelegate(IReport report);

        public PeriodicReportFile(IDateTimeProvider dateTimeProvider, IFileSystem fileSystem, FileSystemPath reportFilePath)
        {
            ArgAssert.NotNull(dateTimeProvider, "dateTimeProvider");
            ArgAssert.NotNull(fileSystem, "fileSystem");
            ArgAssert.NotNull(reportFilePath, "reportFilePath");

            _dateTimeProvider = dateTimeProvider;
            _fileSystem = fileSystem;
            _reportFilePath = reportFilePath;
            _reporters = new List<ReporterDelegate>();
        }

        public FileSystemPath ReportFilePath
        {
            get { return _reportFilePath; }
        }

        public void RegisterReporter(ReporterDelegate reporter)
        {
            lock (_lock)
            {
                _reporters.Add(reporter);
            }
        }

        public void Report()
        {
            lock (_lock)
            {
                var report = new SerializableReport();

                var timestamp = _dateTimeProvider.CurrentDateTime.ToStringISO8601(DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal);
                report.Prefix = string.Format("######## {0}", timestamp);

                _reporters.ForEach(reporter => reporter(report));

                _fileSystem.WriteReport(_reportFilePath, report, append: true);
            }
        }
    }
}
