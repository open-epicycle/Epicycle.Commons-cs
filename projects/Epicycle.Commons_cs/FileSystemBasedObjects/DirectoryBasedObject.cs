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

using Epicycle.Commons.FileSystem;

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This class is a base for objects that are based on a specific directory on a file system.
    /// </summary>
    public abstract class DirectoryBasedObject : FileSystemPathBasedObject
    {
        /// <summary>
        /// C-tor. If autoCreateEmptyDirectory is true and the path does not exist then a new empty directory will be (recursevly) created.
        /// </summary>
        /// <param name="fileSystem">The file system to use. Must not be null.</param>
        /// <param name="path">The path the object is based on. Must not be null.</param>
        /// <param name="shouldAutoCreateEmptyDirectory">If true and the path does not exist then a new empty directory will be (recursevly) created</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path points to a non-existing directory and autoCreateEmptyDirectory is false.</exception>
        /// <exception cref="DirectoryExpectedException">In case the path points to a non-directory file system entity.</exception>
        public DirectoryBasedObject(IFileSystem fileSystem, FileSystemPath path, bool autoCreateEmptyDirectory)
            : base(fileSystem, path)
        {
            if (autoCreateEmptyDirectory)
            {
                fileSystem.EnsureDirectory(path);
            }
            else
            {
                fileSystem.AssertDirectory(path);
            }
        }
    }
}
