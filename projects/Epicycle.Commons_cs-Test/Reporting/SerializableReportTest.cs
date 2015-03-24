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

namespace Epicycle.Commons.Reporting
{
    [TestFixture]
    public class SerializableReportTest
    {
        private SerializableReport _report;

        [SetUp]
        public void SetUp()
        {
            _report = new SerializableReport();
        }

        [Test]
        public void empty_report_serializes_to_empty_string()
        {
            Assert.That(_report.Serialize(), Is.EqualTo(""));
        }

        [Test]
        public void reporting_int_produces_correct_report()
        {
            _report.Report("moo", 123);
            Assert.That(_report.Serialize(), Is.EqualTo("moo: 123\n"));
        }

        [Test]
        public void reporting_long_produces_correct_report()
        {
            _report.Report("moo", 123123123123123L);
            Assert.That(_report.Serialize(), Is.EqualTo("moo: 123123123123123\n"));
        }

        [Test]
        public void reporting_float_produces_correct_report()
        {
            _report.Report("moo", 1.5f);
            Assert.That(_report.Serialize(), Is.EqualTo("moo: 1.5\n"));
        }

        [Test]
        public void reporting_double_produces_correct_report()
        {
            _report.Report("moo", 1.5);
            Assert.That(_report.Serialize(), Is.EqualTo("moo: 1.5\n"));
        }

        [Test]
        public void reporting_string_produces_correct_report()
        {
            _report.Report("foo", "bar");
            Assert.That(_report.Serialize(), Is.EqualTo("foo: bar\n"));
        }

        [Test]
        public void reporting_object_produces_correct_report()
        {
            _report.Report("foo", (object) "bar");
            Assert.That(_report.Serialize(), Is.EqualTo("foo: bar\n"));
        }

        [Test]
        public void sub_report_produces_idented_report()
        {
            var subReport = _report.SubReport("moo");

            subReport.Report("foo", "bar");
            Assert.That(_report.Serialize(), Is.EqualTo("moo:\n    foo: bar\n"));
        }

        [Test]
        public void multiple_entries_are_reported_in_correct_order_and_duplicated_are_preseved()
        {
            _report.Report("foo", "bar");
            _report.Report("moo", 123);
            _report.Report("foo", "baz");
            Assert.That(_report.Serialize(), Is.EqualTo("foo: bar\nmoo: 123\nfoo: baz\n"));
        }

        [Test]
        public void prefix_is_serialized_before_data()
        {
            _report.Report("moo", 123);
            _report.Prefix = "BOOGA";

            Assert.That(_report.Serialize(), Is.EqualTo("BOOGA\nmoo: 123\n"));
        }
    }
}
