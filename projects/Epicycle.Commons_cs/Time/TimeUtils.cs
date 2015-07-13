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

namespace Epicycle.Commons.Time
{
    public static class TimeUtils
    {
        public static readonly DateTime UnixEpochStartUtc = new DateTime(1970, 1, 1).ReinterpretAsUtc();

        public static long MillisecondsSinceUnixEpochUtc(this DateTime @this)
        {
            return (long)@this.ToUniversalTime().Subtract(UnixEpochStartUtc).TotalMilliseconds;
        }

        public static long SecondsSinceUnixEpochUtc(this DateTime @this)
        {
            return (long)@this.ToUniversalTime().Subtract(UnixEpochStartUtc).TotalSeconds;
        }

        public static DateTimeUtcAndLocal NowUtcAndLocal()
        {
            // TODO: Test
            return DateTime.UtcNow.ToUtcAndLocal();
        }

        public static DateTime ReinterpretAsUnspecified(this DateTime @this)
        {
            return @this.Kind == DateTimeKind.Unspecified ? @this : DateTime.SpecifyKind(@this, DateTimeKind.Unspecified);
        }

        public static DateTime ReinterpretAsUtc(this DateTime @this)
        {
            return @this.Kind == DateTimeKind.Utc ? @this : DateTime.SpecifyKind(@this, DateTimeKind.Utc);
        }

        public static DateTime ReinterpretAsLocal(this DateTime @this)
        {
            return @this.Kind == DateTimeKind.Local ? @this : DateTime.SpecifyKind(@this, DateTimeKind.Local);
        }

        public static DateTimeUtcAndLocal ToUtcAndLocal(this DateTime @this)
        {
            switch(@this.Kind)
            {
                case DateTimeKind.Utc:
                    return new DateTimeUtcAndLocal(@this, @this.ToLocalTime());
                case DateTimeKind.Local:
                    return new DateTimeUtcAndLocal(@this.ToUniversalTime(), @this);
                case DateTimeKind.Unspecified:
                    throw new ArgumentException("Dates of an unspecified kind are not supported!");
                default:
                    throw new ArgumentException("Unsupported DateTimeKind");
            }
        }
    }
}
