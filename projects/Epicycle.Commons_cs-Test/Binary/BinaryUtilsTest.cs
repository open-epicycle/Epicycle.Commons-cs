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

namespace Epicycle.Commons.Binary
{
    [TestFixture]
    public class BinaryUtilsTest
    {
        #region SerializeUint

        [Test]
        public void SerializeUint8_works_properly()
        {
            Assert.That(BinaryUtils.SerializeUint8(0x99), Is.EqualTo(0x99));
            Assert.That(BinaryUtils.SerializeUint8(0x34), Is.EqualTo(0x34));
        }

        [Test]
        public void SerializeUint16_big_endian_works_properly()
        {
            byte b0, b1;

            BinaryUtils.SerializeUint16(0x1234, out b0, out b1, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));

            BinaryUtils.SerializeUint16(0xFEDC, out b0, out b1, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
        }

        [Test]
        public void SerializeUint16_little_endian_works_properly()
        {
            byte b0, b1;

            BinaryUtils.SerializeUint16(0x1234, out b0, out b1, Endianity.Little);
            Assert.That(b1, Is.EqualTo(0x12));
            Assert.That(b0, Is.EqualTo(0x34));

            BinaryUtils.SerializeUint16(0xFEDC, out b0, out b1, Endianity.Little);
            Assert.That(b1, Is.EqualTo(0xFE));
            Assert.That(b0, Is.EqualTo(0xDC));
        }

        [Test]
        public void SerializeUint24_big_endian_works_properly()
        {
            byte b0, b1, b2;

            BinaryUtils.SerializeUint24(0x123456, out b0, out b1, out b2, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b2, Is.EqualTo(0x56));

            BinaryUtils.SerializeUint24(0xFEDCBA, out b0, out b1, out b2, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b2, Is.EqualTo(0xBA));
        }

        [Test]
        public void SerializeUint24_little_endian_works_properly()
        {
            byte b0, b1, b2;

            BinaryUtils.SerializeUint24(0x123456, out b0, out b1, out b2, Endianity.Little);
            Assert.That(b2, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b0, Is.EqualTo(0x56));

            BinaryUtils.SerializeUint24(0xFEDCBA, out b0, out b1, out b2, Endianity.Little);
            Assert.That(b2, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b0, Is.EqualTo(0xBA));
        }

        [Test]
        public void SerializeUint32_big_endian_works_properly()
        {
            byte b0, b1, b2, b3;

            BinaryUtils.SerializeUint32(0x12345678U, out b0, out b1, out b2, out b3, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b2, Is.EqualTo(0x56));
            Assert.That(b3, Is.EqualTo(0x78));

            BinaryUtils.SerializeUint32(0xFEDCBA98U, out b0, out b1, out b2, out b3, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b2, Is.EqualTo(0xBA));
            Assert.That(b3, Is.EqualTo(0x98));
        }

        [Test]
        public void SerializeUint32_little_endian_works_properly()
        {
            byte b0, b1, b2, b3;

            BinaryUtils.SerializeUint32(0x12345678U, out b0, out b1, out b2, out b3, Endianity.Little);
            Assert.That(b3, Is.EqualTo(0x12));
            Assert.That(b2, Is.EqualTo(0x34));
            Assert.That(b1, Is.EqualTo(0x56));
            Assert.That(b0, Is.EqualTo(0x78));

            BinaryUtils.SerializeUint32(0xFEDCBA98U, out b0, out b1, out b2, out b3, Endianity.Little);
            Assert.That(b3, Is.EqualTo(0xFE));
            Assert.That(b2, Is.EqualTo(0xDC));
            Assert.That(b1, Is.EqualTo(0xBA));
            Assert.That(b0, Is.EqualTo(0x98));
        }

        [Test]
        public void SerializeUint_big_endian_works_properly()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            BinaryUtils.SerializeUint(0x99U, data, 1, 1, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x99, 3, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0x1234U, data, 1, 2, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0x123456U, data, 1, 3, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 0x56, 5, 6 }));
            BinaryUtils.SerializeUint(0x12345678U, data, 1, 4, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 0x56, 0x78, 6 }));

            data = new byte[] { 1, 2, 3, 4, 5, 6 };
            BinaryUtils.SerializeUint(0x34U, data, 1, 1, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 3, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCU, data, 1, 2, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCBAU, data, 1, 3, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 0xBA, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCBA98U, data, 1, 4, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 0xBA, 0x98, 6 }));
        }

        [Test]
        public void SerializeUint_little_endian_works_properly()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            BinaryUtils.SerializeUint(0x99U, data, 1, 1, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x99, 3, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0x1234U, data, 1, 2, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 0x12, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0x123456U, data, 1, 3, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x56, 0x34, 0x12, 5, 6 }));
            BinaryUtils.SerializeUint(0x12345678U, data, 1, 4, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x78, 0x56, 0x34, 0x12, 6 }));

            data = new byte[] { 1, 2, 3, 4, 5, 6 };
            BinaryUtils.SerializeUint(0x34U, data, 1, 1, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 3, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCU, data, 1, 2, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xDC, 0xFE, 4, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCBAU, data, 1, 3, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xBA, 0xDC, 0xFE, 5, 6 }));
            BinaryUtils.SerializeUint(0xFEDCBA98U, data, 1, 4, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x98, 0xBA, 0xDC, 0xFE, 6 }));
        }

        #endregion

        #region SerializeInt

        [Test]
        public void SerializeInt8_works_properly()
        {
            Assert.That(BinaryUtils.SerializeInt8(-2), Is.EqualTo(0xFE));
            Assert.That(BinaryUtils.SerializeInt8(0x34), Is.EqualTo(0x34));
        }

        [Test]
        public void SerializeInt16_big_endian_works_properly()
        {
            byte b0, b1;

            BinaryUtils.SerializeInt16(0x1234, out b0, out b1, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));

            BinaryUtils.SerializeInt16(-0x124, out b0, out b1, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
        }

        [Test]
        public void SerializeInt16_little_endian_works_properly()
        {
            byte b0, b1;

            BinaryUtils.SerializeInt16(0x1234, out b0, out b1, Endianity.Little);
            Assert.That(b1, Is.EqualTo(0x12));
            Assert.That(b0, Is.EqualTo(0x34));

            BinaryUtils.SerializeInt16(-0x124, out b0, out b1, Endianity.Little);
            Assert.That(b1, Is.EqualTo(0xFE));
            Assert.That(b0, Is.EqualTo(0xDC));
        }

        [Test]
        public void SerializeInt24_big_endian_works_properly()
        {
            byte b0, b1, b2;

            BinaryUtils.SerializeInt24(0x123456, out b0, out b1, out b2, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b2, Is.EqualTo(0x56));

            BinaryUtils.SerializeInt24(-0x12346, out b0, out b1, out b2, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b2, Is.EqualTo(0xBA));
        }

        [Test]
        public void SerializeInt24_little_endian_works_properly()
        {
            byte b0, b1, b2;

            BinaryUtils.SerializeInt24(0x123456, out b0, out b1, out b2, Endianity.Little);
            Assert.That(b2, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b0, Is.EqualTo(0x56));

            BinaryUtils.SerializeInt24(-0x12346, out b0, out b1, out b2, Endianity.Little);
            Assert.That(b2, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b0, Is.EqualTo(0xBA));
        }
        
        [Test]
        public void SerializeInt32_big_endian_works_properly()
        {
            byte b0, b1, b2, b3;

            BinaryUtils.SerializeInt32(0x12345678, out b0, out b1, out b2, out b3, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0x12));
            Assert.That(b1, Is.EqualTo(0x34));
            Assert.That(b2, Is.EqualTo(0x56));
            Assert.That(b3, Is.EqualTo(0x78));

            BinaryUtils.SerializeInt32(-0x1234568, out b0, out b1, out b2, out b3, Endianity.Big);
            Assert.That(b0, Is.EqualTo(0xFE));
            Assert.That(b1, Is.EqualTo(0xDC));
            Assert.That(b2, Is.EqualTo(0xBA));
            Assert.That(b3, Is.EqualTo(0x98));
        }

        [Test]
        public void SerializeInt32_little_endian_works_properly()
        {
            byte b0, b1, b2, b3;

            BinaryUtils.SerializeInt32(0x12345678, out b0, out b1, out b2, out b3, Endianity.Little);
            Assert.That(b3, Is.EqualTo(0x12));
            Assert.That(b2, Is.EqualTo(0x34));
            Assert.That(b1, Is.EqualTo(0x56));
            Assert.That(b0, Is.EqualTo(0x78));

            BinaryUtils.SerializeInt32(-0x1234568, out b0, out b1, out b2, out b3, Endianity.Little);
            Assert.That(b3, Is.EqualTo(0xFE));
            Assert.That(b2, Is.EqualTo(0xDC));
            Assert.That(b1, Is.EqualTo(0xBA));
            Assert.That(b0, Is.EqualTo(0x98));
        }


        [Test]
        public void SerializeInt_big_endian_works_properly()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            BinaryUtils.SerializeInt(0x34, data, 1, 1, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 3, 4, 5, 6 }));
            BinaryUtils.SerializeInt(0x1234, data, 1, 2, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 4, 5, 6 }));
            BinaryUtils.SerializeInt(0x123456, data, 1, 3, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 0x56, 5, 6 }));
            BinaryUtils.SerializeInt(0x12345678, data, 1, 4, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x12, 0x34, 0x56, 0x78, 6 }));

            data = new byte[] { 1, 2, 3, 4, 5, 6 };
            BinaryUtils.SerializeInt(-2, data, 1, 1, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 3, 4, 5, 6 }));
            BinaryUtils.SerializeInt(-0x124, data, 1, 2, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 4, 5, 6 }));
            BinaryUtils.SerializeInt(-0x12346, data, 1, 3, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 0xBA, 5, 6 }));
            BinaryUtils.SerializeInt(-0x1234568, data, 1, 4, Endianity.Big);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 0xDC, 0xBA, 0x98, 6 }));
        }

        [Test]
        public void SerializeInt_little_endian_works_properly()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            BinaryUtils.SerializeInt(0x34, data, 1, 1, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 3, 4, 5, 6 }));
            BinaryUtils.SerializeInt(0x1234, data, 1, 2, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x34, 0x12, 4, 5, 6 }));
            BinaryUtils.SerializeInt(0x123456, data, 1, 3, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x56, 0x34, 0x12, 5, 6 }));
            BinaryUtils.SerializeInt(0x12345678, data, 1, 4, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x78, 0x56, 0x34, 0x12, 6 }));

            data = new byte[] { 1, 2, 3, 4, 5, 6 };
            BinaryUtils.SerializeInt(-2, data, 1, 1, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xFE, 3, 4, 5, 6 }));
            BinaryUtils.SerializeInt(-0x124, data, 1, 2, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xDC, 0xFE, 4, 5, 6 }));
            BinaryUtils.SerializeInt(-0x12346, data, 1, 3, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0xBA, 0xDC, 0xFE, 5, 6 }));
            BinaryUtils.SerializeInt(-0x1234568, data, 1, 4, Endianity.Little);
            Assert.That(data, Is.EqualTo(new byte[] { 1, 0x98, 0xBA, 0xDC, 0xFE, 6 }));
        }
        
        #endregion

        #region DeserializeUint

        [Test]
        public void DeserializeUint8_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint8(0xFE), Is.EqualTo(0xFE));
            Assert.That(BinaryUtils.DeserializeUint8(0x12), Is.EqualTo(0x12));
        }

        [Test]
        public void DeserializeUint16_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint16(0xFE, 0xDC, Endianity.Big), Is.EqualTo(0xFEDC));
            Assert.That(BinaryUtils.DeserializeUint16(0x12, 0x34, Endianity.Big), Is.EqualTo(0x1234));
        }

        [Test]
        public void DeserializeUint16_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint16(0xDC, 0xFE, Endianity.Little), Is.EqualTo(0xFEDC));
            Assert.That(BinaryUtils.DeserializeUint16(0x34, 0x12, Endianity.Little), Is.EqualTo(0x1234));
        }

        [Test]
        public void DeserializeUint24_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint24(0xFE, 0xDC, 0xBA, Endianity.Big), Is.EqualTo(0xFEDCBA));
            Assert.That(BinaryUtils.DeserializeUint24(0x12, 0x34, 0x56, Endianity.Big), Is.EqualTo(0x123456));
        }

        [Test]
        public void DeserializeUint24_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint24(0xBA, 0xDC, 0xFE, Endianity.Little), Is.EqualTo(0xFEDCBA));
            Assert.That(BinaryUtils.DeserializeUint24(0x56, 0x34, 0x12, Endianity.Little), Is.EqualTo(0x123456));
        }

        [Test]
        public void DeserializeUint32_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint32(0xFE, 0xDC, 0xBA, 0x98, Endianity.Big), Is.EqualTo(0xFEDCBA98U));
            Assert.That(BinaryUtils.DeserializeUint32(0x12, 0x34, 0x56, 0x78, Endianity.Big), Is.EqualTo(0x12345678U));
        }

        [Test]
        public void DeserializeUint32_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeUint32(0x98, 0xBA, 0xDC, 0xFE, Endianity.Little), Is.EqualTo(0xFEDCBA98U));
            Assert.That(BinaryUtils.DeserializeUint32(0x78, 0x56, 0x34, 0x12, Endianity.Little), Is.EqualTo(0x12345678U));
        }

        [Test]
        public void DeserializeUint_buffer_big_endian_works_properly()
        {
            var buffer1a = new byte[] { 0xFF, 0xFE };
            var buffer1b = new byte[] { 0xFF, 0x12 }; 
            var buffer2a = new byte[] { 0xFF, 0xFE, 0xDC };
            var buffer2b = new byte[] { 0xFF, 0x12, 0x34 };
            var buffer3a = new byte[] { 0xFF, 0xFE, 0xDC, 0xBA };
            var buffer3b = new byte[] { 0xFF, 0x12, 0x34, 0x56 };
            var buffer4a = new byte[] { 0xFF, 0xFE, 0xDC, 0xBA, 0x98 };
            var buffer4b = new byte[] { 0xFF, 0x12, 0x34, 0x56, 0x78 };

            Assert.That(BinaryUtils.DeserializeUint(buffer1a, 1, 1, Endianity.Big), Is.EqualTo(0xFE));
            Assert.That(BinaryUtils.DeserializeUint(buffer1b, 1, 1, Endianity.Big), Is.EqualTo(0x12)); 
            Assert.That(BinaryUtils.DeserializeUint(buffer2a, 1, 2, Endianity.Big), Is.EqualTo(0xFEDC));
            Assert.That(BinaryUtils.DeserializeUint(buffer2b, 1, 2, Endianity.Big), Is.EqualTo(0x1234));
            Assert.That(BinaryUtils.DeserializeUint(buffer3a, 1, 3, Endianity.Big), Is.EqualTo(0xFEDCBA));
            Assert.That(BinaryUtils.DeserializeUint(buffer3b, 1, 3, Endianity.Big), Is.EqualTo(0x123456));
            Assert.That(BinaryUtils.DeserializeUint(buffer4a, 1, 4, Endianity.Big), Is.EqualTo(0xFEDCBA98U));
            Assert.That(BinaryUtils.DeserializeUint(buffer4b, 1, 4, Endianity.Big), Is.EqualTo(0x12345678U));
        }

        [Test]
        public void DeserializeUint_buffer_little_endian_works_properly()
        {
            var buffer1a = new byte[] { 0xFF, 0xFE };
            var buffer1b = new byte[] { 0xFF, 0x12 }; 
            var buffer2a = new byte[] { 0xFF, 0xDC, 0xFE };
            var buffer2b = new byte[] { 0xFF, 0x34, 0x12 };
            var buffer3a = new byte[] { 0xFF, 0xBA, 0xDC, 0xFE };
            var buffer3b = new byte[] { 0xFF, 0x56, 0x34, 0x12 };
            var buffer4a = new byte[] { 0xFF, 0x98, 0xBA, 0xDC, 0xFE };
            var buffer4b = new byte[] { 0xFF, 0x78, 0x56, 0x34, 0x12 };

            Assert.That(BinaryUtils.DeserializeUint(buffer1a, 1, 1, Endianity.Little), Is.EqualTo(0xFE));
            Assert.That(BinaryUtils.DeserializeUint(buffer1b, 1, 1, Endianity.Little), Is.EqualTo(0x12));
            Assert.That(BinaryUtils.DeserializeUint(buffer2a, 1, 2, Endianity.Little), Is.EqualTo(0xFEDC));
            Assert.That(BinaryUtils.DeserializeUint(buffer2b, 1, 2, Endianity.Little), Is.EqualTo(0x1234));
            Assert.That(BinaryUtils.DeserializeUint(buffer3a, 1, 3, Endianity.Little), Is.EqualTo(0xFEDCBA));
            Assert.That(BinaryUtils.DeserializeUint(buffer3b, 1, 3, Endianity.Little), Is.EqualTo(0x123456));
            Assert.That(BinaryUtils.DeserializeUint(buffer4a, 1, 4, Endianity.Little), Is.EqualTo(0xFEDCBA98U));
            Assert.That(BinaryUtils.DeserializeUint(buffer4b, 1, 4, Endianity.Little), Is.EqualTo(0x12345678U));
        }

        #endregion

        #region DeserializeInt

        [Test]
        public void DeserializeInt8_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt8(0xFE), Is.EqualTo(-2));
            Assert.That(BinaryUtils.DeserializeInt8(0x12), Is.EqualTo(0x12));
        }

        [Test]
        public void DeserializeInt16_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt16(0xFE, 0xDC, Endianity.Big), Is.EqualTo(-0x124));
            Assert.That(BinaryUtils.DeserializeInt16(0x12, 0x34, Endianity.Big), Is.EqualTo(0x1234));
        }

        [Test]
        public void DeserializeInt16_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt16(0xDC, 0xFE, Endianity.Little), Is.EqualTo(-0x124));
            Assert.That(BinaryUtils.DeserializeInt16(0x34, 0x12, Endianity.Little), Is.EqualTo(0x1234));
        }

        [Test]
        public void DeserializeInt24_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt24(0xFE, 0xDC, 0xBA, Endianity.Big), Is.EqualTo(-0x12346));
            Assert.That(BinaryUtils.DeserializeInt24(0x12, 0x34, 0x56, Endianity.Big), Is.EqualTo(0x123456));
        }

        [Test]
        public void DeserializeInt24_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt24(0xBA, 0xDC, 0xFE, Endianity.Little), Is.EqualTo(-0x12346));
            Assert.That(BinaryUtils.DeserializeInt24(0x56, 0x34, 0x12, Endianity.Little), Is.EqualTo(0x123456));
        }

        [Test]
        public void DeserializeInt32_big_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt32(0xFE, 0xDC, 0xBA, 0x98, Endianity.Big), Is.EqualTo(-0x1234568));
            Assert.That(BinaryUtils.DeserializeInt32(0x12, 0x34, 0x56, 0x78, Endianity.Big), Is.EqualTo(0x12345678));
        }

        [Test]
        public void DeserializeInt32_little_endian_works_properly()
        {
            Assert.That(BinaryUtils.DeserializeInt32(0x98, 0xBA, 0xDC, 0xFE, Endianity.Little), Is.EqualTo(-0x1234568));
            Assert.That(BinaryUtils.DeserializeInt32(0x78, 0x56, 0x34, 0x12, Endianity.Little), Is.EqualTo(0x12345678));
        }

        [Test]
        public void DeserializeInt_buffer_big_endian_works_properly()
        {
            var buffer1a = new byte[] { 0xFF, 0xFE };
            var buffer1b = new byte[] { 0xFF, 0x12 }; 
            var buffer2a = new byte[] { 0xFF, 0xFE, 0xDC };
            var buffer2b = new byte[] { 0xFF, 0x12, 0x34 };
            var buffer3a = new byte[] { 0xFF, 0xFE, 0xDC, 0xBA };
            var buffer3b = new byte[] { 0xFF, 0x12, 0x34, 0x56 };
            var buffer4a = new byte[] { 0xFF, 0xFE, 0xDC, 0xBA, 0x98 };
            var buffer4b = new byte[] { 0xFF, 0x12, 0x34, 0x56, 0x78 };

            Assert.That(BinaryUtils.DeserializeInt(buffer1a, 1, 1, Endianity.Big), Is.EqualTo(-2));
            Assert.That(BinaryUtils.DeserializeInt(buffer1b, 1, 1, Endianity.Big), Is.EqualTo(0x12)); 
            Assert.That(BinaryUtils.DeserializeInt(buffer2a, 1, 2, Endianity.Big), Is.EqualTo(-0x124));
            Assert.That(BinaryUtils.DeserializeInt(buffer2b, 1, 2, Endianity.Big), Is.EqualTo(0x1234));
            Assert.That(BinaryUtils.DeserializeInt(buffer3a, 1, 3, Endianity.Big), Is.EqualTo(-0x12346));
            Assert.That(BinaryUtils.DeserializeInt(buffer3b, 1, 3, Endianity.Big), Is.EqualTo(0x123456));
            Assert.That(BinaryUtils.DeserializeInt(buffer4a, 1, 4, Endianity.Big), Is.EqualTo(-0x1234568));
            Assert.That(BinaryUtils.DeserializeInt(buffer4b, 1, 4, Endianity.Big), Is.EqualTo(0x12345678U));
        }

        [Test]
        public void DeserializeInt_buffer_little_endian_works_properly()
        {
            var buffer1a = new byte[] { 0xFF, 0xFE };
            var buffer1b = new byte[] { 0xFF, 0x12 };
            var buffer2a = new byte[] { 0xFF, 0xDC, 0xFE };
            var buffer2b = new byte[] { 0xFF, 0x34, 0x12 };
            var buffer3a = new byte[] { 0xFF, 0xBA, 0xDC, 0xFE };
            var buffer3b = new byte[] { 0xFF, 0x56, 0x34, 0x12 };
            var buffer4a = new byte[] { 0xFF, 0x98, 0xBA, 0xDC, 0xFE };
            var buffer4b = new byte[] { 0xFF, 0x78, 0x56, 0x34, 0x12 };

            Assert.That(BinaryUtils.DeserializeInt(buffer1a, 1, 1, Endianity.Little), Is.EqualTo(-2));
            Assert.That(BinaryUtils.DeserializeInt(buffer1b, 1, 1, Endianity.Little), Is.EqualTo(0x12));
            Assert.That(BinaryUtils.DeserializeInt(buffer2a, 1, 2, Endianity.Little), Is.EqualTo(-0x124));
            Assert.That(BinaryUtils.DeserializeInt(buffer2b, 1, 2, Endianity.Little), Is.EqualTo(0x1234));
            Assert.That(BinaryUtils.DeserializeInt(buffer3a, 1, 3, Endianity.Little), Is.EqualTo(-0x12346));
            Assert.That(BinaryUtils.DeserializeInt(buffer3b, 1, 3, Endianity.Little), Is.EqualTo(0x123456));
            Assert.That(BinaryUtils.DeserializeInt(buffer4a, 1, 4, Endianity.Little), Is.EqualTo(-0x1234568));
            Assert.That(BinaryUtils.DeserializeInt(buffer4b, 1, 4, Endianity.Little), Is.EqualTo(0x12345678U));
        }

        #endregion
    }
}
