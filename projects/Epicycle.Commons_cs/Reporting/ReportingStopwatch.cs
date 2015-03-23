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

using System;
using System.Diagnostics;

namespace Epicycle.Commons.Reporting
{
    public sealed class ReportingStopwatch : IDisposable
    {
        private INumericReport _report;
        private string _name;
        private Stopwatch _stopwatch;

        public ReportingStopwatch(INumericReport report, string name)
        {
            _report = report;
            _name = name;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _report.Report(_name, _stopwatch);
        }
    }
}
