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
using System.Diagnostics;
using Epicycle.Commons.FileSystem;

namespace Epicycle.Commons.Reporting
{
    public static class ReportingUtils
    {
        public static void Report(this INumericReport @this, string name, Stopwatch stopwatch)
        {
            var dt_sec = ((double)stopwatch.ElapsedTicks) / Stopwatch.Frequency;

            @this.Report(name, dt_sec);
        }

        public static IDisposable TimeAndReport(this INumericReport @this, string name)
        {
            return new ReportingStopwatch(@this, name);
        }

        public static void WriteReport(this IFileSystem @this, FileSystemPath path, SerializableReport report, bool append = false)
        {
            @this.WriteTextFile(path, report.Serialize(0), append);
        }
    }
}
