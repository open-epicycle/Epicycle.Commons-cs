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

using System.IO;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// A file system path based exceotion
    /// </summary>
    public abstract class FileSystemPathException : IOException
    {
        /// <summary>
        /// The offending path.
        /// </summary>
        private readonly FileSystemPath _path;

        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="path">The offending path.</param>
        /// <param name="message">A message</param>
        public FileSystemPathException(FileSystemPath path, string message) :
            base(string.Format("{0} ({1})", message, path))
        {
            _path = path;
        }

        /// <summary>
        /// The offending path.
        /// </summary>
        public FileSystemPath Path
        {
            get { return _path; }
        }
    }
}
