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

using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.Csv
{
    internal static class CsvParser
    {
        public static IEnumerable<IEnumerable<string>> Parse(string data)
        {
            var linesData = data.Replace("\r", "").Split('\n').Where(x => x.Trim() != "");
            return linesData.Select(x => ParseLine(x));
        }

        private static IEnumerable<string> ParseLine(string data)
        {
            var parts = data.Split(',');
            return parts.Select(x => x.Trim());
        }
    }
}
