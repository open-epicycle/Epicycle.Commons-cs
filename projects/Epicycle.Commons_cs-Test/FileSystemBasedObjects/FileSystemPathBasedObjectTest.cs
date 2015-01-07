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
    public class FileSystemPathBasedObjectTest
    {
        private Mock<IFileSystem> _mockFileSystem;
        private FileSystemPath _path;
        private TestFileSystemPathBasedObjectTest _testObject;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = IFileSystemTestUtils.CreateMock();
            _path = new FileSystemPath(@"foo\bar");
            _testObject = new TestFileSystemPathBasedObjectTest(_mockFileSystem.Object, _path);
        }

        [Test]
        public void Path_returns_the_path_from_constructor()
        {
            Assert.AreEqual(_path.PathString, _testObject.Path.PathString);
        }

        [Test]
        public void Path_set_path_changes_it()
        {
            var newPath = new FileSystemPath(@"moo/booga");

            _testObject.SetPath(newPath);

            Assert.AreEqual(newPath.PathString, _testObject.Path.PathString);
        }

        private class TestFileSystemPathBasedObjectTest : FileSystemPathBasedObject
        {
            public TestFileSystemPathBasedObjectTest(IFileSystem fileSystem, FileSystemPath path) : base(fileSystem, path) { }

            public void SetPath(FileSystemPath path)
            {
                Path = path;
            }
        }
    }
}
