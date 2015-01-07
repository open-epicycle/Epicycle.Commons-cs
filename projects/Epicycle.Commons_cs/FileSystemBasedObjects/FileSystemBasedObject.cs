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

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This is the base class for all objects that are based on a file system.
    /// </summary>
    public abstract class FileSystemBasedObject
    {
        /// <summary>
        /// The file system the object is based on
        /// </summary>
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="fileSystem">The file system the object is based on. Must not be null</param>
        public FileSystemBasedObject(IFileSystem fileSystem)
        {
            ArgAssert.NotNull(fileSystem, "fileSystem");

            _fileSystem = fileSystem;
        }

        /// <summary>
        /// The file system the object is based on
        /// </summary>
        public IFileSystem FileSystem
        {
            get { return _fileSystem; }
        }
    }
}
