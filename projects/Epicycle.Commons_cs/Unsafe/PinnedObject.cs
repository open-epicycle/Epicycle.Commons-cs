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
