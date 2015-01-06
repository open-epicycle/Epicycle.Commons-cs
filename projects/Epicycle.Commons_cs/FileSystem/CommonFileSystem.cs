// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
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
using System.Text;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// A helper class for easier implementation of a file system object.
    /// </summary>
    public abstract class CommonFileSystem : IFileSystem
    {
        /// <summary>
        /// The default encoding for reading/writing files.
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;

        #region Path target checks

        // See parent doc
        public abstract bool Exists(FileSystemPath path);

        // See parent doc
        public abstract bool IsFile(FileSystemPath path);

        // See parent doc
        public abstract bool IsDirectory(FileSystemPath path);

        #endregion

        #region File system tree manipulation

        // See parent doc
        public void CreateDirectoryRecursively(FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            this.AssertNotExists(path);
            InnerCreateDirectoryRecursively(path);
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract void InnerCreateDirectoryRecursively(FileSystemPath path);

        // See parent doc
        public void CopyFile(FileSystemPath from, FileSystemPath to)
        {
            ArgAssert.NotNull(from, "from");
            ArgAssert.NotNull(to, "to");

            this.AssertFile(from);
            this.AssertFileOrNotExsits(to);

            InnerCopyFile(from, to);
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract void InnerCopyFile(FileSystemPath from, FileSystemPath to);

        // See parent doc
        public void DeleteFile(FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            this.AssertFile(path);
            InnerDeleteFile(path);
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract void InnerDeleteFile(FileSystemPath path);

        #endregion

        #region File system tree queries

        // See parent doc
        public IEnumerable<FileSystemPath> ListDirectory(FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");
            this.AssertDirectory(path);

            return InnerListDirectory(path);
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract IEnumerable<FileSystemPath> InnerListDirectory(FileSystemPath path);

        #endregion

        #region Read/Write files

        // See parent doc
        public byte[] ReadBinaryFile(FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            this.AssertFile(path);
            return InnerReadBinaryFile(path);
        }

        // See parent doc
        public string ReadTextFile(FileSystemPath path, Encoding encoding)
        {
            ArgAssert.NotNull(path, "path");

            byte[] binData = ReadBinaryFile(path);

            return ResolveEncoding(encoding).GetString(binData);
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract byte[] InnerReadBinaryFile(FileSystemPath path);

        // See parent doc
        public void WriteBinaryFile(FileSystemPath path, byte[] data, bool append)
        {
            ArgAssert.NotNull(path, "path");
            ArgAssert.NotNull(data, "data");

            this.AssertFileOrNotExsits(path);
            InnerWriteBinaryFile(path, data, append);
        }

        // See parent doc
        public void WriteTextFile(FileSystemPath path, string data, Encoding encoding, bool append)
        {
            ArgAssert.NotNull(path, "path");
            ArgAssert.NotNull(data, "data");

            this.AssertFileOrNotExsits(path);
            var binData = ResolveEncoding(encoding).GetBytes(data);
            WriteBinaryFile(path, binData, append);
        }

        // See parent doc
        private static Encoding ResolveEncoding(Encoding encoding) 
        {
            return (encoding != null) ? encoding : DefaultEncoding;
        }

        /// <summary>
        /// The implementation itself. Called after all the relevant validations were made.
        /// </summary>
        protected abstract void InnerWriteBinaryFile(FileSystemPath path, byte[] data, bool append);

        #endregion
    }
}
