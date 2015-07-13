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
using Epicycle.Commons.FileSystemBasedObjects;
using System.Collections.Generic;

namespace Epicycle.Commons.Reporting
{
    // TODO: Test
    public sealed class ReportsManager : DirectoryBasedObject
    {
        private readonly object _lock = new object();
        private readonly Dictionary<string, SerializableReport> _reports;

        public ReportsManager(IFileSystem fileSystem, FileSystemPath path)
            : base(fileSystem, path, true)
        {
            _reports = new Dictionary<string, SerializableReport>();
        }

        public IReport GetReport(string id)
        {
            return GetOrInitReport(id);
        }

        private SerializableReport GetOrInitReport(string id)
        {
            lock (_lock)
            {
                if (!_reports.ContainsKey(id))
                {
                    var report = new SerializableReport();
                    report.Prefix = string.Format("######## ID: {0}\n", id);

                    _reports[id] = report;
                }

                return _reports[id];
            }
        }

        public void Sync(string id)
        {
            lock (_lock)
            {
                var reportFilePath = Path.Join(string.Format("{0}.report", FileSystemPathUtils.SanitizePathString(id)));

                FileSystem.WriteReport(reportFilePath, GetOrInitReport(id), append: false);
            }
        }
    }
}
