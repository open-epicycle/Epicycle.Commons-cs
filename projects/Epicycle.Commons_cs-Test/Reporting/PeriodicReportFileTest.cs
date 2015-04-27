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
using Epicycle.Commons.TestUtils.FileSystem;
using Epicycle.Commons.Time;
using Moq;
using NUnit.Framework;
using System;

namespace Epicycle.Commons.Reporting
{
    [TestFixture]
    public class PeriodicReportFileTest
    {
        private ManualDateTimeProvider _dateTimeProvider;
        private Mock<IFileSystem> _fileSystemMock;
        private FileSystemPath _reportFilePath;

        private PeriodicReportFile _periodicReportFile;

        [SetUp]
        public void SetUp()
        {
            _dateTimeProvider = new ManualDateTimeProvider();
            _dateTimeProvider.CurrentDateTime = new DateTime(2012, 12, 12, 13, 45, 32, 123, DateTimeKind.Utc).ToUtcAndLocal();

            _fileSystemMock = FileSystemTestUtils.CreateMock();
            _reportFilePath = new FileSystemPath("/foo/bar");

            _periodicReportFile = new PeriodicReportFile(_dateTimeProvider, _fileSystemMock.Object, _reportFilePath);

            _fileSystemMock.SetupWritableFile(_reportFilePath, expected: null, exists: false);

        }

        [Test]
        public void ReportFilePath_returns_the_path()
        {
            Assert.AreEqual(_reportFilePath, _periodicReportFile.ReportFilePath);
        }

        [Test]
        public void Report_no_reporters_only_writes_header()
        {
            _periodicReportFile.Report();
            ValidateReporting("");
        }

        [Test]
        public void Report_one_reporter_reports_data()
        {
            _periodicReportFile.RegisterReporter(TestReporter1);

            _periodicReportFile.Report();
            ValidateReporting("foo: bar\n");
        }

        [Test]
        public void Report_two_reporters_report_data_in_correct_order()
        {
            _periodicReportFile.RegisterReporter(TestReporter1);
            _periodicReportFile.RegisterReporter(TestReporter2);

            _periodicReportFile.Report();
            ValidateReporting("foo: bar\nbaz: 123\n");
        }

        private void ValidateReporting(string expecterReport)
        {
            var timestamp = _dateTimeProvider.CurrentDateTime.ToStringISO8601(DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal);
            var expectedData = string.Format("######## {0}\n{1}", timestamp, expecterReport);

            _fileSystemMock.AssertFileWritten(_reportFilePath, expectedData, expectedAppend: true);
        }

        private void TestReporter1(IReport report)
        {
            report.Report("foo", "bar");
        }

        private void TestReporter2(IReport report)
        {
            report.Report("baz", 123);
        }
    }
}
