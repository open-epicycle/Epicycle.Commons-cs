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

using Epicycle.Commons.TestUtils.Reporting;
using Epicycle.Commons.Time;
using NUnit.Framework;
using System.Collections.Generic;

namespace Epicycle.Commons.Reporting
{
    [TestFixture]
    public class StatisticsReporterTest
    {
        private ManualClock _clock;
        private ReportMock _report;
        private StatisticsReporter _statisticsReporter;

        [SetUp]
        public void SetUp()
        {
            _clock = new ManualClock();
            _clock.Time = 1234567.0;

            _report = new ReportMock();

            _statisticsReporter = new StatisticsReporter(_clock);
        }

        [Test]
        public void sub_reporter_creates_sub_report_and_reports_to_it()
        {
            var subReporter = _statisticsReporter.CreateSubReporter("foo");
            _clock.Advance(2.5);
            subReporter.ReportEvent("bar");

            _statisticsReporter.DumpToReport(_report);

            var subReport = _report.GetSubReportAndRemove("foo");
            _report.ValidateNoMoreReports();

            subReport.ValidateReportedAndRemove("bar_COUNT", 1);
        }

        [Test]
        public void reported_events_are_counted()
        {
            var dt = 2.5;

            _clock.Advance(dt / 2.0);
            _statisticsReporter.ReportEvent("foo");
            _statisticsReporter.ReportEvent("bar", 3);

            _clock.Advance(dt / 2.0);
            _statisticsReporter.ReportEvent("foo", 2);
            _statisticsReporter.ReportEvent("bar");


            _statisticsReporter.DumpToReport(_report);

            _report.ValidateReportedAndRemove("foo_COUNT", 3);
            _report.ValidateReportedAndRemove("foo_FREQ", 3 / dt);
            _report.ValidateReportedAndRemove("bar_COUNT", 4);
            _report.ValidateReportedAndRemove("bar_FREQ", 4 / dt);

            _report.ValidateNoMoreReports();
        }

        [Test]
        public void once_reported_param_is_correctly_processed()
        {
            var dt = 2.5;
            var value = 123.45;

            _clock.Advance(dt);
            _statisticsReporter.Report("foo", value);

            _statisticsReporter.DumpToReport(_report);

            _report.ValidateReportedAndRemove("foo_COUNT", 1);
            _report.ValidateReportedAndRemove("foo_SUM", value);
            _report.ValidateReportedAndRemove("foo_MIN", value);
            _report.ValidateReportedAndRemove("foo_MAX", value);
            _report.ValidateReportedAndRemove("foo_AVG", value);

            _report.ValidateNoMoreReports();
        }

        [Test]
        public void twice_reported_param_is_correctly_processed()
        {
            var dt = 2.5;
            var value1 = 123.45;
            var value2 = 234.56;

            _clock.Advance(dt);
            _statisticsReporter.Report("foo", value2);
            _statisticsReporter.Report("foo", value1);

            _statisticsReporter.DumpToReport(_report);

            var sum = value1 + value2;
            _report.ValidateReportedAndRemove("foo_COUNT", 2);
            _report.ValidateReportedAndRemove("foo_SUM", sum);
            _report.ValidateReportedAndRemove("foo_MIN", value1);
            _report.ValidateReportedAndRemove("foo_MAX", value2);
            _report.ValidateReportedAndRemove("foo_AVG", sum / 2);
            _report.ValidateReportedAndRemove("foo_STD", 78.56663);

            _report.ValidateNoMoreReports();
        }

        [Test]
        public void multiple_reported_param_is_correctly_processed()
        {
            var dt = 2.5;
            var value1 = 123.45;
            var value2 = 234.56;
            var value3 = 345.67;
            var value4 = 456.78;

            _clock.Advance(dt);
            _statisticsReporter.Report("foo", value2);
            _statisticsReporter.Report("foo", value3);
            _statisticsReporter.Report("foo", value1);
            _statisticsReporter.Report("foo", value4);

            _statisticsReporter.DumpToReport(_report);

            var sum = value1 + value2 + value3 + value4;
            _report.ValidateReportedAndRemove("foo_COUNT", 4);
            _report.ValidateReportedAndRemove("foo_SUM", sum);
            _report.ValidateReportedAndRemove("foo_MIN", value1);
            _report.ValidateReportedAndRemove("foo_MAX", value4);
            _report.ValidateReportedAndRemove("foo_AVG", sum / 4);
            _report.ValidateReportedAndRemove("foo_STD", 124.22476);

            _report.ValidateNoMoreReports();
        }
    }
}
