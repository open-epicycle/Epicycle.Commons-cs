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

using NUnit.Framework;
using System.Diagnostics;
using System.Threading;

namespace Epicycle.Commons.Reporting
{
    [TestFixture]
    public class ReportingUtilsTest
    {
        [Test]
        public void Report_Stopwatch_reports_correct_time()
        {
            var reportMock = new ReportMock();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            Thread.Sleep(100);
            stopwatch.Stop();

            reportMock.Report("moo", stopwatch);

            Assert.That(reportMock.ReportedName, Is.EqualTo("moo"));
            Assert.That(reportMock.ReportedValue, Is.AtLeast(0.09).And.AtMost(1.0));
        }

        [Test]
        public void TimeAndReport_correctly_measures_time()
        {
            var reportMock = new ReportMock();

            using(reportMock.TimeAndReport("moo"))
            {
                Thread.Sleep(100);
            }

            Assert.That(reportMock.ReportedName, Is.EqualTo("moo"));
            Assert.That(reportMock.ReportedValue, Is.AtLeast(0.09).And.AtMost(1.0));
        }

        private sealed class ReportMock : INumericReport
        {
            public ReportMock()
            {
                ReportedName = null;
                ReportedValue = 0;
            }

            public string ReportedName { get; private set; }
            public double ReportedValue { get; private set; }

            public void Report(string name, int value)
            {
                Assert.Fail();
            }

            public void Report(string name, long value)
            {
                Assert.Fail();
            }

            public void Report(string name, float value)
            {
                Assert.Fail();
            }

            public void Report(string name, double value)
            {
                ReportedName = name;
                ReportedValue = value;
            }
        }
    }
}
