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
    public static class BinaryWriterUtils
    {
        public static void WriteByte(this BinaryWriter @this, byte data)
        {
            @this.Write(data);
        }

        public static void WriteUint8(this BinaryWriter @this, uint data)
        {
            @this.WriteByte(data.SerializeUint8());
        }

        public static void WriteUint16(this BinaryWriter @this, uint data, Endianity endianity)
        {
            byte b0;
            byte b1;

            data.SerializeUint16(out b0, out b1, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
        }

        public static void WriteUint24(this BinaryWriter @this, uint data, Endianity endianity)
        {
            byte b0;
            byte b1;
            byte b2;

            data.SerializeUint24(out b0, out b1, out b2, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
            @this.WriteByte(b2);
        }

        public static void WriteUint32(this BinaryWriter @this, uint data, Endianity endianity)
        {
            byte b0;
            byte b1;
            byte b2;
            byte b3;

            data.SerializeUint32(out b0, out b1, out b2, out b3, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
            @this.WriteByte(b2);
            @this.WriteByte(b3);
        }

        public static void WriteInt8(this BinaryWriter @this, int data)
        {
            @this.WriteByte(data.SerializeInt8());
        }

        public static void WriteInt16(this BinaryWriter @this, int data, Endianity endianity)
        {
            byte b0;
            byte b1;

            data.SerializeInt16(out b0, out b1, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
        }

        public static void WriteInt24(this BinaryWriter @this, int data, Endianity endianity)
        {
            byte b0;
            byte b1;
            byte b2;

            data.SerializeInt24(out b0, out b1, out b2, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
            @this.WriteByte(b2);
        }

        public static void WriteInt32(this BinaryWriter @this, int data, Endianity endianity)
        {
            byte b0;
            byte b1;
            byte b2;
            byte b3;

            data.SerializeInt32(out b0, out b1, out b2, out b3, endianity);

            @this.WriteByte(b0);
            @this.WriteByte(b1);
            @this.WriteByte(b2);
            @this.WriteByte(b3);
        }
    }
}
