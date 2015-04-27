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

namespace Epicycle.Commons.Time
{
    [TestFixture]
    public class ManualClockTest
    {
        private ManualClock _clock;

        [SetUp]
        public void SetUp()
        {
            _clock = new ManualClock();
        }

        [Test]
        public void Time_is_zero_upon_creation()
        {
            Assert.That(_clock.Time, Is.EqualTo(0.0));
            
        }

        [Test]
        public void Setting_and_getting_time_is_correct()
        {
            _clock.Time = 10;

            Assert.That(_clock.Time, Is.EqualTo(10));
        }

        [Test]
        public void Advance_advances_time_by_the_given_amount()
        {
            _clock.Time = 10;
            _clock.Advance(15);

            Assert.That(_clock.Time, Is.EqualTo(25));
        }
    }
}
