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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.Csv
{
    public sealed class CsvData : ICsvData
    {
        private readonly List<Row> _rows;
        private List<Row> _dataRows;
        private CsvConfiguration _configuration;

        public CsvData(string serialized)
        {
            _rows = CsvParser.Parse(serialized).Select(x => new Row(this, x)).ToList();

            Rebuild();
        }

        private void Rebuild()
        {
            if(_configuration == null || !_configuration.IsHeaderRow)
            {
                _dataRows = _rows;
            }
            else
            {
                _dataRows = _rows.GetRange(1, _rows.Count - 1);
            }
        }

        public void ConfigureFromFirstRow()
        {
            var configuraion = new CsvConfiguration();

            configuraion.IsHeaderRow = true;
            
            foreach(var cell in GetRow(0))
            {
                configuraion.AddColumn(cell, cell);
            }

            Configuration = configuraion;
        }

        public CsvConfiguration Configuration
        {
            get { return _configuration; }
            set { _configuration = value; Rebuild(); }
        }

        public IEnumerable<ICsvRow> Rows
        {
            get { return _rows.Select(x => (ICsvRow)x); }
        }

        public ICsvRow GetRow(int index)
        {
            return _rows[index];
        }

        public IEnumerable<ICsvRow> DataRows
        {
            get { return _dataRows.Select(x => (ICsvRow)x); }
        }

        public ICsvRow GetDataRow(int index)
        {
            return _dataRows[index];
        }

        public string Serialize()
        {
            var linesData = _rows.Select(x => x.Serialize());
            return string.Join("\r\n", linesData.ToArray());
        }

        public sealed class Row : ICsvRow
        {
            private readonly CsvData _parent;
            private readonly List<string> _cells;

            internal Row(CsvData parent, IEnumerable<string> cells)
            {
                _parent = parent;
                _cells = cells.ToList();
            }

            public string Serialize()
            {
                return string.Join(", ", _cells.ToArray());
            }

            public IEnumerator<string> GetEnumerator()
            {
                return _cells.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public string this[int index]
            {
                get { return _cells[index]; }
                set { _cells[index] = value; }
            }

            public string this[string columnName]
            {
                get { return _cells[GetIndex(columnName)]; }
                set { _cells[GetIndex(columnName)] = value; }
            }

            private int GetIndex(string columnName)
            {
                return _parent.Configuration.GetColumn(columnName).Index;
            }
        }
    }
}
