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
using Epicycle.Commons.TestUtils.FileSystem;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class MultiFileTest : AssertionHelper
    {
        private Mock<IFileSystem> _mockFileSystem;
        private FileSystemPath _path;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = FileSystemTestUtils.CreateMock();
            _path = new FileSystemPath(@"foo\bar");
        }

        //[Test] // TODO: restore
        public void Prefix_based_ctor_loads_all_files_with_given_prefix()
        {
            SetUpDirectory();
            ValidateMultiFile(new MultiFile(_mockFileSystem.Object, _path, "foo"));
        }

        //[Test] // TODO: restore
        public void Main_file_based_ctor_loads_all_files_with_given_prefix()
        {
            SetUpDirectory();

            var mainFile = _path.Join("Foo.boo");

            _mockFileSystem.SetupExistance(mainFile, FileSystemTestUtils.PathExistance.File);
            ValidateMultiFile(new MultiFile(_mockFileSystem.Object, mainFile));
        }

        private void SetUpDirectory()
        {
            string[] files = { "foo.moo", "Foo.boo", "foo-xyz.goo", "bar.moo" };

            _mockFileSystem.SetupListDir(_path, files);
        }

        private void ValidateMultiFile(MultiFile multiFile)
        {
            var foundFiles = multiFile.Files.ToList();

            Expect(foundFiles.Count, Is.EqualTo(3));

            ValidateMultiFileFile(foundFiles[0], "Foo.boo", "", "boo");
            ValidateMultiFileFile(foundFiles[1], "foo.moo", "", "moo");
            ValidateMultiFileFile(foundFiles[2], "foo-xyz.goo", "-xyz", "goo");

        }

        private void ValidateMultiFileFile(MultiFile.MultiFileFile file, string expectedName, string expectedSuffix, string expectedExtension)
        {
            Expect(file.Path.PathString, Is.EqualTo(_path.Join(expectedName).PathString));
            Expect(file.Suffix, Is.EqualTo(expectedSuffix));
            Expect(file.Extension, Is.EqualTo(expectedExtension));
        }
    }
}
