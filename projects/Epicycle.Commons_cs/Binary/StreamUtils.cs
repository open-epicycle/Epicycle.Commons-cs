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
using System.IO;

namespace Epicycle.Commons.Binary
{
    public static class StreamUtils
    {
        public static bool HasEnded(this Stream @this)
        {
            return @this.Position >= @this.Length;
        }

        public static long BytesLeft(this Stream @this)
        {
            return Math.Max(@this.Length - @this.Position, 0);
        }

        /// <summary>
        /// Validates that the stream has not ended.
        /// </summary>
        /// <param name="this">The stream.</param>
        /// <exception cref="System.IO.EndOfStreamException"> Throws if the validation fails.</exception>
        public static void AssertNotEnded(this Stream @this)
        {
            if (@this.HasEnded())
            {
                throw new EndOfStreamException("The stream has ended!");
            }
        }

        public static void AssertBytesLeft(this Stream @this, long minBytes)
        {
            if (@this.BytesLeft() < minBytes)
            {
                throw new EndOfStreamException("Not enough bytes in stream!");
            }
        }

        /// <summary>
        /// Skips the given <paramref name="amount"/> of bytes from the current position. If the new position is beyond
        /// beyond the end of the stream, no exception will be thrown.
        /// </summary>
        /// <param name="this">The stream.</param>
        /// <param name="amount">The number of bytes to skip. Negative amount will move the cursor backwards.</param>
        public static void Skip(this Stream @this, long amount)
        {
            @this.Seek(amount, SeekOrigin.Current);
        }
    }
}
