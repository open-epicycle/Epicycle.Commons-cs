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
using System;

namespace Epicycle.Commons.Time
{
    [TestFixture]
    public class TimeUtilsTest
    {
        private DateTime _dateTimeUnspecified;
        private DateTime _dateTimeUtc;
        private DateTime _dateTimeLocal;

        [SetUp]
        public void SetUp()
        {
            var dateTime = new DateTime(2012, 12, 21, 13, 23, 54);

            _dateTimeUnspecified = dateTime.ReinterpretAsUnspecified();
            _dateTimeUtc = dateTime.ReinterpretAsUtc();
            _dateTimeLocal = dateTime.ReinterpretAsLocal();
        }

        private void AssertSameTimeAndKind(DateTime expected, DateTime dateTime)
        {
            Assert.That(dateTime.Kind, Is.EqualTo(expected.Kind));
            Assert.That(dateTime.Ticks, Is.EqualTo(expected.Ticks));
        }

        [Test]
        public void GetMillisecondsSinceUnixEpochUtc_start_of_unix_epoch_yields_zero()
        {
            Assert.That(TimeUtils.UnixEpochStartUtc.MillisecondsSinceUnixEpochUtc(), Is.EqualTo(0));
        }

        [Test]
        public void GetMillisecondsSinceUnixEpochUtc_calculated_correctly()
        {
            Assert.That(_dateTimeUtc.MillisecondsSinceUnixEpochUtc(), Is.EqualTo(1356096234000));
        }

        [Test]
        public void GetSecondsSinceUnixEpochUtc_start_of_unix_epoch_yields_zero()
        {
            Assert.That(TimeUtils.UnixEpochStartUtc.SecondsSinceUnixEpochUtc(), Is.EqualTo(0));
        }

        [Test]
        public void GetSecondsSinceUnixEpochUtc_calculated_correctly()
        {
            Assert.That(_dateTimeUtc.SecondsSinceUnixEpochUtc(), Is.EqualTo(1356096234));
        }

        [Test]
        public void ReinterpretAsUnspecified_time_remains_the_same_kind_changes()
        {
            AssertSameTimeAndKind(_dateTimeUnspecified, _dateTimeUnspecified.ReinterpretAsUnspecified());
            AssertSameTimeAndKind(_dateTimeUnspecified, _dateTimeUtc.ReinterpretAsUnspecified());
            AssertSameTimeAndKind(_dateTimeUnspecified, _dateTimeLocal.ReinterpretAsUnspecified());
        }

        [Test]
        public void ReinterpretAsUtc_time_remains_the_same_kind_changes()
        {
            AssertSameTimeAndKind(_dateTimeUtc, _dateTimeUnspecified.ReinterpretAsUtc());
            AssertSameTimeAndKind(_dateTimeUtc, _dateTimeUtc.ReinterpretAsUtc());
            AssertSameTimeAndKind(_dateTimeUtc, _dateTimeLocal.ReinterpretAsUtc());
        }

        [Test]
        public void ReinterpretAsLocal_time_remains_the_same_kind_changes()
        {
            AssertSameTimeAndKind(_dateTimeLocal, _dateTimeUnspecified.ReinterpretAsLocal());
            AssertSameTimeAndKind(_dateTimeLocal, _dateTimeUtc.ReinterpretAsLocal());
            AssertSameTimeAndKind(_dateTimeLocal, _dateTimeLocal.ReinterpretAsLocal());
        }

        [Test]
        public void ToUtcAndLocal_utc_time_is_properly_converted_to_local()
        {
            var utcAndLocal = _dateTimeUtc.ToUtcAndLocal();

            AssertSameTimeAndKind(_dateTimeUtc, utcAndLocal.Utc);
            AssertSameTimeAndKind(_dateTimeUtc.ToLocalTime(), utcAndLocal.Local);
        }

        [Test]
        public void ToUtcAndLocal_local_time_is_properly_converted_to_utc()
        {
            var utcAndLocal = _dateTimeLocal.ToUtcAndLocal();

            AssertSameTimeAndKind(_dateTimeLocal.ToUniversalTime(), utcAndLocal.Utc);
            AssertSameTimeAndKind(_dateTimeLocal, utcAndLocal.Local);
        }
    }
}
