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

using Epicycle.Commons.FileSystem;

namespace Epicycle.Commons.Csv
{
    /// <summary>
    /// This class contains various utilities for serializing and deserializing CSV data.
    /// </summary>
    public static class CsvUtils
    {
        /// <summary>
        /// An extension to IFileSystem types that reads and deserializes CSV data from a file. The default encoding (UTF-8) is used.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to the CSV file. The file must exist.</param>
        /// <returns>The deserialized object</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static CsvData ReadCsv(this IFileSystem fileSystem, FileSystemPath path)
        {
            return new CsvData(fileSystem.ReadTextFile(path));
        }

        /// <summary>
        /// An extension to IFileSystem types that serializes CSV data and writes it to a file. The default encoding (UTF-8) is used.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="data">The CSV data to serialize</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static void WriteCsv(this IFileSystem fileSystem, FileSystemPath path, CsvData data)
        {
            fileSystem.WriteTextFile(path, data.Serialize());
        }
    }
}
