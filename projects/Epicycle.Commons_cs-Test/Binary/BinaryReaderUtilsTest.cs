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
using System.IO;

namespace Epicycle.Commons.Binary
{
    [TestFixture]
    public class BinaryReaderUtilsTest
    {
        private BinaryReader _reader;

        [SetUp]
        public void SetUp()
        {
            InitReader();
        }

        private void InitReader(params byte[] data)
        {
            _reader = new BinaryReader(new MemoryStream(data));
        }

        private void AssertPosition(int expectedPosition)
        {
            Assert.That(_reader.BaseStream.Position, Is.EqualTo(expectedPosition));
        }

        [Test]
        public void ReadByteAsByte_reads_byte_correctly()
        {
            InitReader(0xAB, 1);
            Assert.That(_reader.ReadByteAsByte(), Is.EqualTo((byte)0xAB));
            AssertPosition(1);
        }

        [Test]
        public void ReadBytes_reads_bytes_correctly()
        {
            InitReader(0x12, 0x34, 0xAB, 1);
            Assert.That(_reader.ReadBytes(3), Is.EqualTo(new Byte[] { 0X12, 0x34, 0xAB }));
            AssertPosition(3);
        }

        [Test]
        public void ReadUint8_reads_data_correctly()
        {
            InitReader(0xAB, 1);
            Assert.That(_reader.ReadUint8(), Is.EqualTo(0xAB));
            AssertPosition(1);
        }

        [Test]
        public void ReadUint16_reads_data_correctly()
        {
            InitReader(0x89, 0xAB, 1);
            Assert.That(_reader.ReadUint16(Endianity.Big), Is.EqualTo(0x89AB));
            AssertPosition(2);
        }

        [Test]
        public void ReadUint24_reads_data_correctly()
        {
            InitReader(0x89, 0xAB, 0xCD, 1);
            Assert.That(_reader.ReadUint24(Endianity.Big), Is.EqualTo(0x89ABCD));
            AssertPosition(3);
        }

        [Test]
        public void ReadUint32_reads_data_correctly()
        {
            InitReader(0x89, 0xAB, 0xCD, 0xEF, 1);
            Assert.That(_reader.ReadUint32(Endianity.Big), Is.EqualTo(0x89ABCDEFU));
            AssertPosition(4);
        }

        [Test]
        public void ReadInt8_reads_data_correctly()
        {
            InitReader(0xFE, 1);
            Assert.That(_reader.ReadInt8(), Is.EqualTo(-2));
            AssertPosition(1);
        }

        [Test]
        public void ReadInt16_reads_data_correctly()
        {
            InitReader(0xFE, 0xDC, 1);
            Assert.That(_reader.ReadInt16(Endianity.Big), Is.EqualTo(-0x124));
            AssertPosition(2);
        }

        [Test]
        public void ReadInt24_reads_data_correctly()
        {
            InitReader(0xFE, 0xDC, 0xBA, 1);
            Assert.That(_reader.ReadInt24(Endianity.Big), Is.EqualTo(-0x12346));
            AssertPosition(3);
        }

        [Test]
        public void ReadInt32_reads_data_correctly()
        {
            InitReader(0xFE, 0xDC, 0xBA, 0x98, 1);
            Assert.That(_reader.ReadInt32(Endianity.Big), Is.EqualTo(-0x1234568));
            AssertPosition(4);
        }

        [Test]
        public void ReadFloat32_reads_data_correctly()
        {
            InitReader(0x3F, 0xC0, 0xA5, 0x7A, 1);
            Assert.That(_reader.ReadFloat32(Endianity.Big), Is.EqualTo(1.50505).Within(0.001));
            AssertPosition(4);
        }
    }
}
