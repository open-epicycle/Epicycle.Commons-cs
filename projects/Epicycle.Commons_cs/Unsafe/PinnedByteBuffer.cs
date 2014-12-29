using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.Unsafe
{
    public unsafe sealed class PinnedByteBuffer : PinnedObject
    {
        public PinnedByteBuffer(byte[] array, int offset)
            : base(array)
        {
            _ptr = (byte*)_handle.AddrOfPinnedObject().ToPointer() + offset;
        }

        public PinnedByteBuffer(byte[,] array, int offset1, int offset2)
            : base(array)
        {
            _ptr = (byte*)_handle.AddrOfPinnedObject().ToPointer() + offset1 * array.GetLength(1) + offset2;
        }

        public PinnedByteBuffer(byte[,,] array, int offset1 = 0, int offset2 = 0, int offset3 = 0)
            : base(array)
        {
            var offset = (offset1 * array.GetLength(1) + offset2) * array.GetLength(2) + offset3;
            _ptr = (byte*)_handle.AddrOfPinnedObject().ToPointer() + offset;
        }

        private byte* _ptr;

        public new byte* Ptr
        {
            get { return _ptr; }
        }

        public void MoveOffset(int offset)
        {
            _ptr += offset;
        }
    }
}
