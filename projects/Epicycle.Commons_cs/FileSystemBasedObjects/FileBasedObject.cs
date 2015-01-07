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
    /// This class is a base for objects that are based on a specific file on a file system.
    /// </summary>
    public abstract class FileBasedObject : FileSystemPathBasedObject
    {
        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="fileSystem">The file system to use. Must not be null.</param>
        /// <param name="path">The path the object is based on. Must not be null and point to a file.</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public FileBasedObject(IFileSystem fileSystem, FileSystemPath path)
            : base(fileSystem, path)
        {
            fileSystem.AssertFile(path);
        }
    }
}
