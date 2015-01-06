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
