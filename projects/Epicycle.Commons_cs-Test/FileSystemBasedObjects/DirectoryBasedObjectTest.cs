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
using Moq;
using NUnit.Framework;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class DirectoryBasedObjectTest
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
        public void Ctor_with_autocreate_not_existing_path_creates_empty_directory()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.DoesntExist);
            _mockFileSystem.Setup(m => m.CreateDirectoryRecursively(_path)).Verifiable();

            CreateTestObject(true);

            _mockFileSystem.Verify(m => m.CreateDirectoryRecursively(_path));
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void Ctor_with_autocreate_path_to_file_throws_DirectoryExpectedException()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.File);
            CreateTestObject(true);
        }

        [Test]
        public void Ctor_with_autocreate_path_to_directory_does_nothing()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.Directory);
            CreateTestObject(true);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void Ctor_with_no_autocreate_not_existing_path_throws_FileSystemPathDoesNotExistException()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.DoesntExist);
            CreateTestObject(false);
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void Ctor_with_no_autocreate_path_to_file_throws_DirectoryExpectedException()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.File);
            CreateTestObject(false);
        }

        [Test]
        public void Ctor_with_no_autocreate_path_to_directory_does_nothing()
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, IFileSystemTestUtils.PathExistance.Directory);
            CreateTestObject(false);
        }

        private void CreateTestObject(bool autoCreateEmptyDirectory)
        {
            new TestDirectoryBasedObject(_mockFileSystem.Object, _path, autoCreateEmptyDirectory);
        }

        private class TestDirectoryBasedObject : DirectoryBasedObject
        {
            public TestDirectoryBasedObject(IFileSystem fileSystem, FileSystemPath path, bool autoCreateEmptyDirectory)
                : base(fileSystem, path, autoCreateEmptyDirectory) { }
        }
    }
}
