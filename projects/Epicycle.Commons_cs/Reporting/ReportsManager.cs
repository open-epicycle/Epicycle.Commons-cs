// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
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
using System.Text;

namespace Epicycle.Commons.Reporting
{
    public sealed class ReportsManager : DirectoryBasedObject
    {
        private readonly object _lock = new object();
        private readonly Dictionary<string, SimpleReport> _reports;

        public ReportsManager(IFileSystem fileSystem, FileSystemPath path)
            : base(fileSystem, path, true)
        {
            _reports = new Dictionary<string, SimpleReport>();
        }

        public IReport GetReport(string id)
        {
            return GetOrInitReport(id);
        }

        private SimpleReport GetOrInitReport(string id)
        {
            lock (_lock)
            {
                if (!_reports.ContainsKey(id))
                {
                    _reports[id] = new SimpleReport();
                }

                return _reports[id];
            }
        }

        public void Sync(string id)
        {
            lock (_lock)
            {
                var report = GetOrInitReport(id);
                var data = new StringBuilder();
                data.AppendFormat("ID: {0}\n", id);
                data.Append("DATA:\n");
                data.Append(report.Serialize(1));

                var reportFilePath = Path.Join(string.Format("{0}.report", FileSystemPathUtils.SanitizePathString(id)));

                FileSystem.WriteTextFile(reportFilePath, data.ToString());
            }
        }
    }
}
