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

using Epicycle.Commons.Threading;
using System.Threading;

namespace Epicycle.Commons.Reporting
{
    // TODO: Test

    public sealed class PeriodicReportFileThread : BasePeriodicThread
    {
        private readonly PeriodicReportFile _periodicReportFile;

        public PeriodicReportFileThread(PeriodicReportFile periodicReportFile, double period_sec)
            : base(1.0 / period_sec, PeriodicThreadTightness.Low)
        {
            ArgAssert.NotNull(periodicReportFile, "periodicReportFile");

            Thread.Priority = ThreadPriority.Lowest;
            _periodicReportFile = periodicReportFile;
        }
        
        protected override void Iteration()
        {
            _periodicReportFile.ReportToFile();
        }
    }
}
