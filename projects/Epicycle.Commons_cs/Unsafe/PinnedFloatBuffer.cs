using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Epicycle.Commons.Unsafe
{
    public unsafe sealed class PinnedFloatBuffer : PinnedObject
    {
        public PinnedFloatBuffer(float[] array, int offset = 0)
            : base(array)
        {
            _ptr = (float*)_handle.AddrOfPinnedObject().ToPointer() + offset;
        }

        public PinnedFloatBuffer(float[,] array, int offset1 = 0, int offset2 = 0) 
            : base(array)
        {
            _ptr = (float*)_handle.AddrOfPinnedObject().ToPointer() + offset1 * array.GetLength(1) + offset2;
        }

        public PinnedFloatBuffer(float[, ,] array, int offset1 = 0, int offset2 = 0, int offset3 = 0)
            : base(array)
        {
            var offset = (offset1 * array.GetLength(1) + offset2) * array.GetLength(2) + offset3;
            _ptr = (float*)_handle.AddrOfPinnedObject().ToPointer() + offset;
        }

        private float* _ptr;

        public new float* Ptr
        {
            get { return _ptr; }
        }

        public void MoveOffset(int offset)
        {
            _ptr += offset;
        }
    }
}
