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
    public class DateTimeFormattingTest
    {
        private DateTime _dateTimeUtc;
        private DateTime _dateTimeLocal;
        private DateTimeUtcAndLocal _dateTimeUtcAndLocal;

        [SetUp]
        public void SetUp()
        {
            var dateTime = new DateTime(2012, 12, 21, 13, 23, 54);

            _dateTimeUtc = dateTime.ReinterpretAsUtc();
            _dateTimeLocal = dateTime.ReinterpretAsLocal();

            _dateTimeUtcAndLocal = new DateTimeUtcAndLocal(_dateTimeUtc, _dateTimeLocal);
        }

        #region ToStringISO8601(DateTime)

        [Test]
        public void ToStringISO8601_DateTime_DateOnly_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21", DateTimeFormatting.ISO8601Template.DateOnly, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTime_TimeOnly_produces_correct_result()
        {
            TestToStringISO8601("13:23:54", DateTimeFormatting.ISO8601Template.TimeOnly, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTime_DateTime_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54", DateTimeFormatting.ISO8601Template.DateTime, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTime_Pretty_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21 13:23:54", DateTimeFormatting.ISO8601Template.DateTime, DateTimeFormatting.ISO8601Format.Pretty);
        }

        [Test]
        public void ToStringISO8601_DateTime_Formal_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54", DateTimeFormatting.ISO8601Template.DateTime, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTime_FileSystemPathFriendly_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13_23_54", DateTimeFormatting.ISO8601Template.DateTime, DateTimeFormatting.ISO8601Format.FileSystemPathFriendly);
        }

        private void TestToStringISO8601(string expected, DateTimeFormatting.ISO8601Template template, DateTimeFormatting.ISO8601Format format)
        {
            Assert.That(_dateTimeUtc.ToStringISO8601(template, format), Is.EqualTo(expected + "Z"));
            Assert.That(_dateTimeLocal.ToStringISO8601(template, format), Is.EqualTo(expected));
        }

        #endregion

        #region ToStringISO8601(DateTimeUtcAndLocal)

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_Utc_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54Z", DateTimeFormatting.UtcAndLocalTemplate.Utc, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_LocaL_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54", DateTimeFormatting.UtcAndLocalTemplate.Local, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_UtcAndLocal_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54Z 2012-12-21T13:23:54", DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_LocalAndUtc_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54 2012-12-21T13:23:54Z", DateTimeFormatting.UtcAndLocalTemplate.LocalAndUtc, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_Pretty_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21 13:23:54Z / 2012-12-21 13:23:54", DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal, DateTimeFormatting.ISO8601Format.Pretty);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_Formal_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13:23:54Z 2012-12-21T13:23:54", DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal, DateTimeFormatting.ISO8601Format.Formal);
        }

        [Test]
        public void ToStringISO8601_DateTimeUtcAndLocal_FileSystemPathFriendly_produces_correct_result()
        {
            TestToStringISO8601("2012-12-21T13_23_54Z--2012-12-21T13_23_54", DateTimeFormatting.UtcAndLocalTemplate.UtcAndLocal, DateTimeFormatting.ISO8601Format.FileSystemPathFriendly);
        }

        private void TestToStringISO8601(string expected, DateTimeFormatting.UtcAndLocalTemplate template, DateTimeFormatting.ISO8601Format format)
        {
            Assert.That(_dateTimeUtcAndLocal.ToStringISO8601(template, DateTimeFormatting.ISO8601Template.DateTime, format), Is.EqualTo(expected));
        }

        #endregion
    }
}
