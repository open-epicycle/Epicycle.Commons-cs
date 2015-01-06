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

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Thrown when a path was expected not to point to an existing file system object.
    /// </summary>
    public class FileSystemPathAlreadyExistsException : FileSystemPathException
    {
        public FileSystemPathAlreadyExistsException(FileSystemPath path) :
            this(path, "Path already exists.") { }

        public FileSystemPathAlreadyExistsException(FileSystemPath path, string message) :
            base(path, message) { }
    }
}
