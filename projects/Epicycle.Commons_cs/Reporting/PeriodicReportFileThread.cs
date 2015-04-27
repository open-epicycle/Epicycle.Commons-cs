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

using System.Threading;

namespace Epicycle.Commons.Reporting
{
    // TODO: Test

    public sealed class PeriodicReportFileThread : BasePeriodicThread
    {
        private readonly object _lock = new object();

        PeriodicReportFile _periodicReportFile;

        public delegate void ReporterDelegate(IReport report);

        public PeriodicReportFileThread(PeriodicReportFile periodicReportFile, double period_sec)
            : base(BasicMath.Round(period_sec * 1000), 100)
        {
            Thread.Priority = ThreadPriority.Lowest;
            _periodicReportFile = periodicReportFile;
        }

        public new void Start()
        {
            base.Start();
        }
        
        protected override void Iteration()
        {
            _periodicReportFile.Report();
        }
    }
}
