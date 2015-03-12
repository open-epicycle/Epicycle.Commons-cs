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

using System.IO;

namespace Epicycle.Commons.Binary
{
    public static class BinaryReaderUtils
    {
        public static byte ReadByteAsByte(this BinaryReader @this)
        {
            @this.BaseStream.AssertNotEnded();

            return (byte)@this.BaseStream.ReadByte();
        }

        public static byte[] ReadBytes(this BinaryReader @this, int length)
        {
            ArgAssert.AtLeast(length, "length", 0);

            if (length == 0)
            {
                return new byte[0];
            }

            @this.BaseStream.AssertBytesLeft(length);

            var result = new byte[length];
            @this.BaseStream.Read(result, offset: 0, count: length);

            return result;
        }

        #region ReadUint

        public static uint ReadUint8(this BinaryReader @this)
        {
            return BinaryUtils.DeserializeUint8(@this.ReadByteAsByte());
        }

        public static uint ReadUint16(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeUint16(b0, b1, endianity);
        }

        public static uint ReadUint24(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();
            var b2 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeUint24(b0, b1, b2, endianity);
        }

        public static uint ReadUint32(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();
            var b2 = @this.ReadByteAsByte();
            var b3 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeUint32(b0, b1, b2, b3, endianity);
        }

        #endregion

        #region ReadInt

        public static int ReadInt8(this BinaryReader @this)
        {
            return BinaryUtils.DeserializeInt8(@this.ReadByteAsByte());
        }

        public static int ReadInt16(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeInt16(b0, b1, endianity);
        }

        public static int ReadInt24(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();
            var b2 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeInt24(b0, b1, b2, endianity);
        }

        public static int ReadInt32(this BinaryReader @this, Endianity endianity)
        {
            var b0 = @this.ReadByteAsByte();
            var b1 = @this.ReadByteAsByte();
            var b2 = @this.ReadByteAsByte();
            var b3 = @this.ReadByteAsByte();

            return BinaryUtils.DeserializeInt32(b0, b1, b2, b3, endianity);
        }

        #endregion

        public static float ReadFloat32(this BinaryReader @this, Endianity endianity)
        {
            if(endianity == Endianity.Big)
            {
                byte[] temp = new byte[4];
                temp[3] = @this.ReadByteAsByte();
                temp[2] = @this.ReadByteAsByte();
                temp[1] = @this.ReadByteAsByte();
                temp[0] = @this.ReadByteAsByte();
                BinaryReader tempReader = new BinaryReader(new MemoryStream(temp));
                return tempReader.ReadSingle();
            }
            else
            {
                return @this.ReadSingle();
            }
        }
    }
}
