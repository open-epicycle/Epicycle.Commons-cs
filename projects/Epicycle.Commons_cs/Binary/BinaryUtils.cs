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

using System;

namespace Epicycle.Commons.Binary
{
    public static class BinaryUtils
    {
        #region SerializeUint

        public static byte SerializeUint8(this uint @this)
        {
            return (byte)(@this & 0xFF);
        }

        public static void SerializeUint16(this uint @this, out byte b0, out byte b1, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 8) & 0xFF);
                b1 = (byte)(@this & 0xFF);
            }
            else
            {
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeUint24(this uint @this, out byte b0, out byte b1, out byte b2, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b2 = (byte)(@this & 0xFF);
            }
            else
            {
                b2 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeUint32(this uint @this, out byte b0, out byte b1, out byte b2, out byte b3, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 24) & 0xFF);
                b1 = (byte)((@this >> 16) & 0xFF);
                b2 = (byte)((@this >> 8) & 0xFF);
                b3 = (byte)(@this & 0xFF);
            }
            else
            {
                b3 = (byte)((@this >> 24) & 0xFF);
                b2 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeUint(this uint @this, byte[] data, int offset, int length, Endianity endianity)
        {
            switch (length)
            {
                case 1:
                    data[offset] = SerializeUint8(@this);
                    break;
                case 2:
                    SerializeUint16(@this, out data[offset], out data[offset + 1], endianity);
                    break;
                case 3:
                    SerializeUint24(@this, out data[offset], out data[offset + 1], out data[offset + 2], endianity);
                    break;
                case 4:
                    SerializeUint32(@this, out data[offset], out data[offset + 1], out data[offset + 2], out data[offset + 3], endianity);
                    break;
                default:
                    throw new ArgumentException("length must be between 1 and 4");
            }
        }

        #endregion

        #region SerializeInt

        public static byte SerializeInt8(this int @this)
        {
            return (byte)(@this & 0xFF);
        }

        public static void SerializeInt16(this int @this, out byte b0, out byte b1, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 8) & 0xFF);
                b1 = (byte)(@this & 0xFF);
            }
            else
            {
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeInt24(this int @this, out byte b0, out byte b1, out byte b2, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b2 = (byte)(@this & 0xFF);
            }
            else
            {
                b2 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeInt32(this int @this, out byte b0, out byte b1, out byte b2, out byte b3, Endianity endianity)
        {
            if (endianity == Endianity.Big)
            {
                b0 = (byte)((@this >> 24) & 0xFF);
                b1 = (byte)((@this >> 16) & 0xFF);
                b2 = (byte)((@this >> 8) & 0xFF);
                b3 = (byte)(@this & 0xFF);
            }
            else
            {
                b3 = (byte)((@this >> 24) & 0xFF);
                b2 = (byte)((@this >> 16) & 0xFF);
                b1 = (byte)((@this >> 8) & 0xFF);
                b0 = (byte)(@this & 0xFF);
            }
        }

        public static void SerializeInt(this int @this, byte[] data, int offset, int length, Endianity endianity)
        {
            switch (length)
            {
                case 1:
                    data[offset] = SerializeInt8(@this);
                    break;
                case 2:
                    SerializeInt16(@this, out data[offset], out data[offset + 1], endianity);
                    break;
                case 3:
                    SerializeInt24(@this, out data[offset], out data[offset + 1], out data[offset + 2], endianity);
                    break;
                case 4:
                    SerializeInt32(@this, out data[offset], out data[offset + 1], out data[offset + 2], out data[offset + 3], endianity);
                    break;
                default:
                    throw new ArgumentException("length must be between 1 and 4");
            }
        }

        #endregion

        #region DeserializeUint

        public static uint DeserializeUint8(byte b)
        {
            return b;
        }

        public static uint DeserializeUint16(byte b0, byte b1, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                ((uint)b0 << 8) | (uint)b1 :
                ((uint)b1 << 8) | (uint)b0;
        }

        public static uint DeserializeUint24(byte b0, byte b1, byte b2, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                ((uint)b0 << 16) | ((uint)b1 << 8) | (uint)b2 :
                ((uint)b2 << 16) | ((uint)b1 << 8) | (uint)b0;
        }

        public static uint DeserializeUint32(byte b0, byte b1, byte b2, byte b3, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                ((uint)b0 << 24) | ((uint)b1 << 16) | ((uint)b2 << 8) | (uint)b3 :
                ((uint)b3 << 24) | ((uint)b2 << 16) | ((uint)b1 << 8) | (uint)b0;
        }

        public static uint DeserializeUint(byte[] data, int offset, int length, Endianity endianity)
        {
            switch(length)
            {
                case 1:
                    return DeserializeUint8(data[offset]);
                case 2:
                    return DeserializeUint16(data[offset], data[offset + 1], endianity);
                case 3:
                    return DeserializeUint24(data[offset], data[offset + 1], data[offset + 2], endianity);
                case 4:
                    return DeserializeUint32(data[offset], data[offset + 1], data[offset + 2], data[offset + 3], endianity);
                default:
                    throw new ArgumentException("length must be between 1 and 4");
            }
        }

        #endregion

        #region DeserializeInt

        public static int DeserializeInt8(byte b)
        {
            return ((int)(b << 24)) >> 24;
        }

        public static int DeserializeInt16(byte b0, byte b1, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                (((int)(b0 << 24)) >> 16) | b1 :
                (((int)(b1 << 24)) >> 16) | b0;
        }
        
        public static int DeserializeInt24(byte b0, byte b1, byte b2, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                (((int)(b0 << 24)) >> 8) | (b1 << 8) | b2 :
                (((int)(b2 << 24)) >> 8) | (b1 << 8) | b0;
        }
        
        public static int DeserializeInt32(byte b0, byte b1, byte b2, byte b3, Endianity endianity)
        {
            return (endianity == Endianity.Big) ?
                ((int)(b0 << 24)) | (b1 << 16) | (b2 << 8) | b3 :
                ((int)(b3 << 24)) | (b2 << 16) | (b1 << 8) | b0;
        }
        
        public static int DeserializeInt(byte[] data, int offset, int length, Endianity endianity)
        {
            switch (length)
            {
                case 1:
                    return DeserializeInt8(data[offset]);
                case 2:
                    return DeserializeInt16(data[offset], data[offset + 1], endianity);
                case 3:
                    return DeserializeInt24(data[offset], data[offset + 1], data[offset + 2], endianity);
                case 4:
                    return DeserializeInt32(data[offset], data[offset + 1], data[offset + 2], data[offset + 3], endianity);
                default:
                    throw new ArgumentException("length must be between 1 and 4");
            }
        }

        #endregion
    }
}
