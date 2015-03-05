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
using System.IO;

namespace Epicycle.Commons.Binary
{
    [TestFixture]
    public class StreamUtilsTest
    {
        private Stream _stream;

        [SetUp]
        public void SetUp()
        {
            var data = new byte[] { 1, 2, 3, 4, 5 };

            _stream = new MemoryStream(data);
        }

        private void MoveTo(int newPosition)
        {
            _stream.Seek(newPosition, SeekOrigin.Begin);
        }

        [Test]
        public void HasEnded_zero_position_return_false()
        {
            Assert.That(_stream.HasEnded(), Is.False);
        }

        [Test]
        public void HasEnded_last_position_return_false()
        {
            MoveTo(4);
            Assert.That(_stream.HasEnded(), Is.False);
        }

        [Test]
        public void HasEnded_just_after_end_position_return_false()
        {
            MoveTo(5);
            Assert.That(_stream.HasEnded(), Is.True);
        }

        [Test]
        public void HasEnded_way_over_end_position_return_false()
        {
            MoveTo(15);
            Assert.That(_stream.HasEnded(), Is.True);
        }

        [Test]
        public void BytesLeft_different_positions_return_correct_answer()
        {
            for(var i = 0; i < 5; i++)
            {
                MoveTo(i);
                Assert.That(_stream.BytesLeft(), Is.EqualTo(5 - i));
            }
        }

        [Test]
        public void BytesLeft_after_the_end_position_returns_zero()
        {
            MoveTo(25);
            Assert.That(_stream.BytesLeft(), Is.EqualTo(0));
        }

        [Test]
        public void AssertNotEnded_if_not_ended_nothing_happens()
        {
            MoveTo(3);
            _stream.AssertNotEnded();
        }

        [Test]
        [ExpectedException(typeof(EndOfStreamException))]
        public void AssertNotEnded_if_ended_exception_is_thrown()
        {
            MoveTo(15);
            _stream.AssertNotEnded();
        }

        [Test]
        public void AssertBytesLeft_exactly_bytes_left_does_nothing()
        {
            MoveTo(3);
            _stream.AssertBytesLeft(2);
        }

        [Test]
        public void AssertBytesLeft_more_than_needed_bytes_left_does_nothing()
        {
            MoveTo(1);
            _stream.AssertBytesLeft(2);
        }

        [Test]
        [ExpectedException(typeof(EndOfStreamException))]
        public void AssertNotEnded_not_enough_bytes_exception_is_thrown()
        {
            MoveTo(3);
            _stream.AssertBytesLeft(3);
        }

        [Test]
        public void Skip_zero_delta_does_nothing()
        {
            MoveTo(1);
            _stream.Skip(0);
            Assert.That(_stream.Position, Is.EqualTo(1));
        }

        [Test]
        public void Skip_positive_delta_moves_forward()
        {
            MoveTo(1);
            _stream.Skip(2);
            Assert.That(_stream.Position, Is.EqualTo(3));
        }

        [Test]
        public void Skip_negative_delta_moves_backward()
        {
            MoveTo(3);
            _stream.Skip(-2);
            Assert.That(_stream.Position, Is.EqualTo(1));
        }
    }
}
