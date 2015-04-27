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

using Epicycle.Commons.Reporting;
using NUnit.Framework;
using System.Collections.Generic;

namespace Epicycle.Commons.TestUtils.Reporting
{
    public sealed class ReportMock : IReport
    {
        private Dictionary<string, object> _reports;

        public ReportMock()
        {
            _reports = new Dictionary<string, object>();   
        }

        public IReport SubReport(string name)
        {
            var value = new ReportMock();

            _reports[name] = value;

            return value;
        }

        public void Report(string name, int value)
        {
            _reports[name] = value;
        }

        public void Report(string name, long value)
        {
            _reports[name] = value;
        }

        public void Report(string name, float value)
        {
            _reports[name] = value;
        }

        public void Report(string name, double value)
        {
            _reports[name] = value;
        }

        public void Report(string name, string value)
        {
            _reports[name] = value;
        }

        public void Report(string name, object value)
        {
            _reports[name] = value;
        }

        public void ValidateNoMoreReports()
        {
            Assert.That(_reports.Count, Is.EqualTo(0));
        }

        public ReportMock GetSubReportAndRemove(string name)
        {
            Assert.That(_reports.ContainsKey(name), Is.True);
            var subReport = (ReportMock)_reports[name];
            _reports.Remove(name);

            return subReport;
        }

        public void ValidateReportedAndRemove(string name, int expected)
        {
            ValidateReportedAndRemove(name, (object)expected);
        }

        public void ValidateReportedAndRemove(string name, long expected)
        {
            ValidateReportedAndRemove(name, (object)expected);
        }

        public void ValidateReportedAndRemove(string name, float expected)
        {
            Assert.That(_reports.ContainsKey(name), Is.True);
            Assert.That(_reports[name], Is.EqualTo(expected).Within(0.001));
            _reports.Remove(name);
        }

        public void ValidateReportedAndRemove(string name, double expected)
        {
            Assert.That(_reports.ContainsKey(name), Is.True);
            Assert.That(_reports[name], Is.EqualTo(expected).Within(0.001));
            _reports.Remove(name);
        }

        public void ValidateReportedAndRemove(string name, string expected)
        {
            ValidateReportedAndRemove(name, (object)expected);
        }

        public void ValidateReportedAndRemove(string name, object expected)
        {
            Assert.That(_reports.ContainsKey(name), Is.True);
            Assert.That(_reports[name], Is.EqualTo(expected));
            _reports.Remove(name);
        }
    }
}
