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
using System.Linq;

namespace Epicycle.Commons.Binary
{
    [TestFixture]
    public class BinaryWriterUtilsTest
    {
        private MemoryStream _stream;
        private BinaryWriter _writer;

        [SetUp]
        public void SetUp()
        {
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);
        }

        private void AssertPosition(int expectedPosition)
        {
            Assert.That(_stream.Position, Is.EqualTo(expectedPosition));
        }

        public void AssertData(params byte[] expectedData)
        {
            AssertPosition(expectedData.Length);
            Assert.That(_stream.GetBuffer().ToList().GetRange(0, expectedData.Length).ToArray(), Is.EqualTo(expectedData));
        }

        [Test]
        public void WriteByte_writes_a_byte()
        {
            _writer.WriteByte(0xAB);
            AssertData(0xAB);
        }

        [Test]
        public void WriteUint8_writes_data_correctly()
        {
            _writer.WriteUint8(0x89);
            AssertData(0x89);
        }

        [Test]
        public void WriteUint16_writes_data_correctly()
        {
            _writer.WriteUint16(0x89AB, Endianity.Big);
            AssertData(0x89, 0xAB);
        }

        [Test]
        public void WriteUint24_writes_data_correctly()
        {
            _writer.WriteUint24(0x89ABCD, Endianity.Big);
            AssertData(0x89, 0xAB, 0xCD);
        }

        [Test]
        public void WriteUint32_writes_data_correctly()
        {
            _writer.WriteUint32(0x89ABCDEFU, Endianity.Big);
            AssertData(0x89, 0xAB, 0xCD, 0xEF);
        }

        [Test]
        public void WriteInt8_writes_data_correctly()
        {
            _writer.WriteInt8(-2);
            AssertData(0xFE);
        }

        [Test]
        public void WriteInt16_writes_data_correctly()
        {
            _writer.WriteInt16(-0x124, Endianity.Big);
            AssertData(0xFE, 0xDC);
        }

        [Test]
        public void WriteInt24_writes_data_correctly()
        {
            _writer.WriteInt24(-0x12346, Endianity.Big);
            AssertData(0xFE, 0xDC, 0xBA);
        }

        [Test]
        public void WriteInt32_writes_data_correctly()
        {
            _writer.WriteInt32(-0x1234568, Endianity.Big);
            AssertData(0xFE, 0xDC, 0xBA, 0x98);
        }
    }
}
