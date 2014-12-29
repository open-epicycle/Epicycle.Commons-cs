using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Epicycle.Commons.Unsafe
{
    public class PinnedObject : IDisposable
    {
        public PinnedObject(object @object)
        {
            _handle = GCHandle.Alloc(@object, GCHandleType.Pinned);
        }

        protected readonly GCHandle _handle;

        public IntPtr Ptr
        {
            get { return _handle.AddrOfPinnedObject(); }
        }

        public void Dispose()
        {
            _handle.Free();
        }        
    }
}
