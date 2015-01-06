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

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epicycle.Commons.FileSystem
{
    public class CommonFileSystemTest
    {
        private FileSystemPath _testPath;
        private FileSystemPath _anotherTestPath;

        [SetUp]
        public void SetUp()
        {
            _testPath = new FileSystemPath("foo/bar");
            _anotherTestPath = new FileSystemPath("moo/booga");
        }

        #region CreateDirectoryRecursively

        [Test]
        public void CreateDirectoryRecursively_path_not_exists_should_work()
        {
            var fileSystemMock = new MockCommonFileSystemCreateDirectoryRecursively();
            fileSystemMock.CreateDirectoryRecursively(_testPath);
            
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathAlreadyExistsException))]
        public void CreateDirectoryRecursively_path_to_file_throws_FileSystemPathAlreadyExistsException()
        {
            var fileSystemMock = new MockCommonFileSystemCreateDirectoryRecursively();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            fileSystemMock.CreateDirectoryRecursively(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathAlreadyExistsException))]
        public void CreateDirectoryRecursively_path_to_directory_throws_FileSystemPathAlreadyExistsException()
        {
            var fileSystemMock = new MockCommonFileSystemCreateDirectoryRecursively();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);
            fileSystemMock.CreateDirectoryRecursively(_testPath);
        }

        private class MockCommonFileSystemCreateDirectoryRecursively : MockCommonFileSystem
        {
            public bool Called = false;
            public FileSystemPath Path = null;

            protected override void InnerCreateDirectoryRecursively(FileSystemPath path)
            {
                Called = true;
                Path = path;
            }
        }

        #endregion

        #region ListDirectory

        [Test]
        public void ListDirectory_path_to_directory_lists_it()
        {
            var fileSystemMock = new MockCommonFileSystemListDirectory();
            fileSystemMock.PathToReturn = _anotherTestPath;
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);

            var result = fileSystemMock.ListDirectory(_testPath);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(new FileSystemPath[] {_anotherTestPath}, result.ToArray());
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void ListDirectory_path_not_exixts_throws_FileSystemPathDoesNotExistException()
        {
            var fileSystemMock = new MockCommonFileSystemListDirectory();

            fileSystemMock.ListDirectory(_testPath);
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void ListDirectory_path_to_file_throws_DirectoryExpectedException()
        {
            var fileSystemMock = new MockCommonFileSystemListDirectory();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);

            fileSystemMock.ListDirectory(_testPath);
        }

        private class MockCommonFileSystemListDirectory : MockCommonFileSystem
        {
            public bool Called = false;
            public FileSystemPath Path = null;
            public FileSystemPath PathToReturn = null;

            protected override IEnumerable<FileSystemPath> InnerListDirectory(FileSystemPath path)
            {
                Called = true;
                Path = path;

                return new FileSystemPath[] {PathToReturn};
            }
        }

        #endregion

        #region Read files

        [Test]
        public void ReadBinaryFile_path_to_file_returns_contents()
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            var fileSystemMock = new MockCommonFileSystemReadFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            fileSystemMock.DataToReturn = data;

            var result = fileSystemMock.ReadBinaryFile(_testPath);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(data, result);
        }


        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void ReadBinaryFile_path_not_exixts_throws_FileSystemPathDoesNotExistException()
        {
            var fileSystemMock = new MockCommonFileSystemReadFile();

            fileSystemMock.ReadBinaryFile(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void ReadBinaryFile_path_to_directory_throws_FileExpectedException()
        {
            var fileSystemMock = new MockCommonFileSystemReadFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);

            fileSystemMock.ReadBinaryFile(_testPath);
        }

        [Test]
        public void ReadTextFile_path_to_file_returns_contents_using_default_encoding()
        {
            var data = "Booga, \u1234\u2345";

            var fileSystemMock = new MockCommonFileSystemReadFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            fileSystemMock.DataToReturn = Encoding.UTF8.GetBytes(data);

            var result = fileSystemMock.ReadTextFile(_testPath, null);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(data, result);
        }

        [Test]
        public void ReadTextFile_path_to_file_returns_contents_using_non_default_encoding()
        {
            var data = "Booga, \u1234\u2345";

            var fileSystemMock = new MockCommonFileSystemReadFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            fileSystemMock.DataToReturn = Encoding.UTF32.GetBytes(data);

            var result = fileSystemMock.ReadTextFile(_testPath, Encoding.UTF32);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(data, result);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void ReadTextFile_path_not_exixts_throws_FileSystemPathDoesNotExistException()
        {
            var fileSystemMock = new MockCommonFileSystemReadFile();

            fileSystemMock.ReadTextFile(_testPath, null);
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void ReadTextFile_path_to_directory_throws_FileExpectedException()
        {
            var fileSystemMock = new MockCommonFileSystemReadFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);

            fileSystemMock.ReadTextFile(_testPath, null);
        }

        private class MockCommonFileSystemReadFile : MockCommonFileSystem
        {
            public bool Called = false;
            public FileSystemPath Path = null;
            public byte[] DataToReturn = null;

            protected override byte[] InnerReadBinaryFile(FileSystemPath path)
            {
                Called = true;
                Path = path;

                return DataToReturn;
            }
        }

        #endregion

        #region Write files

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void WriteBinaryFile_path_to_directory_throws_FileExpectedException()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);

            fileSystemMock.WriteBinaryFile(_testPath, new byte[] {1, 2, 3});
        }

        [Test]
        public void WriteBinaryFile_write_new_file_works()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            TestWriteBinaryFile(fileSystemMock);
        }

        [Test]
        public void WriteBinaryFile_write_over_existing_file_works()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            TestWriteBinaryFile(fileSystemMock);
        }

        private void TestWriteBinaryFile(MockCommonFileSystemWriteFile fileSystemMock)
        {
            var data = new byte[] { 1, 2, 3, 4, 5, 6 };

            fileSystemMock.WriteBinaryFile(_testPath, data, false);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(data, fileSystemMock.Data);
        } 

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void WriteTextFile_path_to_directory_throws_FileExpectedException()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.Directory);

            fileSystemMock.WriteTextFile(_testPath, "Moo", null, false);
        }

        [Test]
        public void WriteTextFile_write_new_file_with_default_encoding_works()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            TestWriteTextFile(fileSystemMock, null);
        }

        [Test]
        public void WriteTextFile_write_new_file_with_non_default_encoding_works()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            TestWriteTextFile(fileSystemMock, Encoding.UTF32);
        }

        [Test]
        public void WriteTextFile_write_over_existing_file_works()
        {
            var fileSystemMock = new MockCommonFileSystemWriteFile();
            fileSystemMock.SetPathExistance(_testPath, MockCommonFileSystem.PathExistance.File);
            TestWriteTextFile(fileSystemMock, null);
        }

        // TODO: Test append
        private void TestWriteTextFile(MockCommonFileSystemWriteFile fileSystemMock, Encoding encoding)
        {
            var data = "Booga, \u1234\u2345";
            var binData = (encoding != null ? encoding : Encoding.UTF8).GetBytes(data);

            fileSystemMock.WriteTextFile(_testPath, data, encoding, false);
            Assert.That(fileSystemMock.Called);
            AssertPath(fileSystemMock.Path);
            Assert.AreEqual(binData, fileSystemMock.Data);
        } 

        private class MockCommonFileSystemWriteFile : MockCommonFileSystem
        {
            public bool Called = false;
            public FileSystemPath Path = null;
            public byte[] Data = null;

            protected override void InnerWriteBinaryFile(FileSystemPath path, byte[] data, bool append)
            {
                Called = true;
                Path = path;
                Data = data;
            }
        }

        #endregion

        private void AssertPath(FileSystemPath path)
        {
            Assert.AreEqual(_testPath.PathString, path.PathString);
        }

        private class MockCommonFileSystem : CommonFileSystem
        {
            public enum PathExistance
            {
                DoesntExist,
                File,
                Directory
            }

            private FileSystemPath _path;
            private PathExistance _pathExistance;

            public MockCommonFileSystem()
            {
                _path = null;
                _pathExistance = PathExistance.DoesntExist;
            }

            public void SetPathExistance(FileSystemPath path, PathExistance existance)
            {
                _path = path;
                _pathExistance = existance;
            }

            public override bool Exists(FileSystemPath path)
            {
                return (_pathExistance != PathExistance.DoesntExist) && (path.CompareTo(_path) == 0);
            }

            public override bool IsFile(FileSystemPath path)
            {
                return Exists(path) && (_pathExistance == PathExistance.File);
            }

            public override bool IsDirectory(FileSystemPath path)
            {
                return Exists(path) && (_pathExistance == PathExistance.Directory);
            }

            protected override void InnerCreateDirectoryRecursively(FileSystemPath path)
            {
                Assert.Fail();
            }

            protected override void InnerCopyFile(FileSystemPath from, FileSystemPath to)
            {
                Assert.Fail();
            }

            protected override void InnerDeleteFile(FileSystemPath path)
            {
                Assert.Fail();
            }

            protected override IEnumerable<FileSystemPath> InnerListDirectory(FileSystemPath path)
            {
                Assert.Fail();
                return null;
            }

            protected override byte[] InnerReadBinaryFile(FileSystemPath path)
            {
                Assert.Fail();
                return null;
            }

            protected override void InnerWriteBinaryFile(FileSystemPath path, byte[] data, bool append)
            {
                Assert.Fail();
            }
        }
    }
}
