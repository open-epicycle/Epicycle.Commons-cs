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

namespace Epicycle.Commons.Csv
{
    public sealed class CsvConfiguration
    {
        private readonly List<Column> _columns;
        private readonly Dictionary<string, Column> _columnsByName;

        public CsvConfiguration()
        {
            _columns = new List<Column>();
            _columnsByName = new Dictionary<string, Column>();

            IsHeaderRow = false;
        }

        public void AddColumn(string name, string label)
        {
            var column = new Column(_columns.Count, name, label);

            _columns.Add(column);
            _columnsByName[name] = column;
        }

        public IEnumerable<Column> Columns
        {
            get { return _columns; }
        }

        public Column GetColumn(int index)
        {
            return _columns[index];
        }

        public Column GetColumn(string name)
        {
            return _columnsByName[name];
        }

        public bool IsHeaderRow { get; set; }

        public sealed class Column
        {
            private readonly int _index;
            private readonly string _name;
            private readonly string _label;

            internal Column(int index, string name, string label)
            {
                _index = index;
                _name = name;
                _label = label;
            }

            public int Index
            {
                get { return _index; }
            }

            public string Name
            {
                get { return _name; }
            }

            public string Label
            {
                get { return _label; }
            }
        }
    }
}
