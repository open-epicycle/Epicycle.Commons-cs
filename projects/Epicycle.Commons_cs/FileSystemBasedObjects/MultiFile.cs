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
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    public sealed class MultiFile : FileSystemBasedObject
    {
        private readonly FileSystemPath _directoryPath;
        private readonly string _prefix;
        private readonly IList<MultiFileFile> _files;

        public MultiFile(IFileSystem fileSystem, FileSystemPath directoryPath, string prefix)
            : base(fileSystem)
        {
            ArgAssert.NotNull(directoryPath, "directoryPath");
            ArgAssert.NotNull(prefix, "prefix");
            ArgAssert.That(prefix != "", "prefix", "not be empty");

            FileSystem.AssertDirectory(directoryPath);

            _directoryPath = directoryPath;
            _prefix = prefix;
            _files = new List<MultiFileFile>();

            Populate();
        }

        public MultiFile(IFileSystem fileSystem, FileSystemPath mainFilePath)
            : base(fileSystem)
        {
            ArgAssert.NotNull(mainFilePath, "mainFilePath");

            FileSystem.AssertFile(mainFilePath);

            _directoryPath = mainFilePath.Parent;
            _prefix = mainFilePath.GetLastPartWithoutExtension();
            _files = new List<MultiFileFile>();

            Populate();
        }

        private void Populate()
        {
            var prefixLength = _prefix.Length;

            foreach (var path in FileSystem.ListDirectorySorted(_directoryPath).Where(path => path.LastPart.StartsWith(_prefix, true, null)))
            {
                var afterPrefix = path.LastPart.Substring(prefixLength);
                var dotPos = afterPrefix.LastIndexOf('.');

                string suffix;
                string extension;

                if(dotPos < 0)
                {
                    suffix = afterPrefix;
                    extension = "";
                }
                else
                {
                    suffix = afterPrefix.Substring(0, dotPos);
                    extension = afterPrefix.Substring(dotPos + 1);
                }

                _files.Add(new MultiFileFile(path, suffix, extension));
            }
        }

        public FileSystemPath DirectoryPath
        {
            get { return _directoryPath; }
        }

        public string Prefix
        {
            get { return _prefix; }
        }

        public IEnumerable<MultiFileFile> Files
        {
            get { return _files; }
        }

        public FileSystemPath GetFile(string suffix, string extension)
        {
            var record = _files.FirstOrDefault(file => (file.Suffix == suffix) && (file.Extension == extension));

            return (record != null) ? record.Path : null;
        }

        public sealed class MultiFileFile
        {
            private readonly FileSystemPath _path;
            private readonly string _suffix;
            private readonly string _extension;

            internal MultiFileFile(FileSystemPath path, string suffix, string extension)
            {
                _path = path;
                _suffix = suffix;
                _extension = extension;
            }

            public FileSystemPath Path
            {
                get { return _path; }
            }

            public string Suffix
            {
                get { return _suffix; }
            }

            public string Extension
            {
                get { return _extension; }
            }
        }
    }
}
