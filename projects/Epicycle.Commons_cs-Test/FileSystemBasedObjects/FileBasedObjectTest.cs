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

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class FileBasedObjectTest
    {
        private Mock<IFileSystem> _mockFileSystem;
        private FileSystemPath _path;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = IFileSystemTestUtils.CreateMock();
            _path = new FileSystemPath(@"foo\bar");
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void Ctor_not_existing_path_throws_FileSystemPathDoesNotExistException()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.DoesntExist);
            CreateTestObject();
        }

        [Test]
        public void Ctor_path_to_file_does_nothing()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.File);
            CreateTestObject();
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void Ctor_path_to_directory_throws_FileExpectedException()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.Directory);
            CreateTestObject();
        }

        private void CreateTestObject()
        {
            new TestFileBasedObject(_mockFileSystem.Object, _path);
        }

        private class TestFileBasedObject : FileBasedObject
        {
            public TestFileBasedObject(IFileSystem fileSystem, FileSystemPath path)
                : base(fileSystem, path) { }
        }
    }
}
