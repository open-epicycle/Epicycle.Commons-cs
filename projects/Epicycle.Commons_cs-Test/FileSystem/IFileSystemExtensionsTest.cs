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

using Epicycle.Commons.TestUtils.FileSystem;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons.FileSystem
{
    [TestFixture]
    public class IFileSystemExtensionsTest
    {
        private FileSystemPath _testPath;

        [SetUp]
        public void SetUp()
        {
            _testPath = new FileSystemPath(@"foo\bar");
        }

        #region Path assertions

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void AssertExists_path_does_not_exist_throws_FileSystemPathDoesNotExistException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertExists(_testPath);
        }

        [Test]
        public void AssertExists_path_points_to_file_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertExists(_testPath);
        }

        [Test]
        public void AssertExists_path_points_to_directory_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertExists(_testPath);
        }

        [Test]
        public void AssertNotExists_path_does_not_exist_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertNotExists(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathAlreadyExistsException))]
        public void AssertNotExists_path_points_to_file_throws_FileSystemPathAlreadyExistsException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertNotExists(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathAlreadyExistsException))]
        public void AssertNotExists_path_points_to_directory_throws_FileSystemPathAlreadyExistsException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertNotExists(_testPath);
        }

        [Test]
        public void AssertFileOrNotExsits_path_does_not_exist_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertFileOrNotExsits(_testPath);
        }

        [Test]
        public void AssertFileOrNotExsits_path_points_to_file_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertFileOrNotExsits(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void AssertFileOrNotExsits_path_points_to_directory_throws_FileExpectedException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertFileOrNotExsits(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void AssertFile_path_does_not_exist_throws_FileSystemPathDoesNotExistException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertFile(_testPath);
        }

        [Test]
        public void AssertFile_path_points_to_file_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertFile(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void AssertFile_path_points_to_directory_throws_FileExpectedException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertFile(_testPath);
        }

        [Test]
        public void AssertDirectoryOrNotExsits_path_does_not_exist_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertDirectoryOrNotExsits(_testPath);
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void AssertDirectoryOrNotExsits_path_points_to_file_throws_DirectoryExpectedException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertDirectoryOrNotExsits(_testPath);
        }

        [Test]
        public void AssertDirectoryOrNotExsits_path_points_to_directory_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertDirectoryOrNotExsits(_testPath);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void AssertDirectory_path_does_not_exist_throws_FileSystemPathDoesNotExistException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist).Object.AssertDirectory(_testPath);
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void AssertDirectory_path_points_to_file_throws_DirectoryExpectedException()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.File).Object.AssertDirectory(_testPath);
        }

        [Test]
        public void AssertDirectory_path_points_to_directory_doesnt_throw()
        {
            PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory).Object.AssertDirectory(_testPath);
        }

        #endregion

        #region EnsureDirectory

        [Test]
        public void EnsureDirectory_path_does_not_exist_creates_new_dir()
        {
            var fileSystemMock = PathExistanceMock(IFileSystemTestUtils.PathExistance.DoesntExist);
            fileSystemMock.Setup(m => m.CreateDirectoryRecursively(_testPath)).Verifiable();

            fileSystemMock.Object.EnsureDirectory(_testPath);

            fileSystemMock.Verify(m => m.CreateDirectoryRecursively(_testPath));
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void EnsureDirectory_path_to_file_throws_DirectoryExpectedException()
        {
            var fileSystemMock = PathExistanceMock(IFileSystemTestUtils.PathExistance.File);

            fileSystemMock.Object.EnsureDirectory(_testPath);
        }

        [Test]
        public void EnsureDirectory_path_to_directory_does_nothing()
        {
            var fileSystemMock = PathExistanceMock(IFileSystemTestUtils.PathExistance.Directory);

            fileSystemMock.Object.EnsureDirectory(_testPath);
        }

        #endregion

        #region Directory listing

        [Test]
        public void ListDirectorySorted_sorts_the_paths()
        {
            var fileSystemMock = IFileSystemTestUtils.CreateMock();
            IFileSystemTestUtils.SetupListDir(fileSystemMock, _testPath, @"abc\yyz", @"bbc\xyz", @"abc\xyz");

            var result = fileSystemMock.Object.ListDirectorySorted(_testPath);

            AssertPathList(result, @"abc\xyz", @"abc\yyz", @"bbc\xyz");
        }

        [Test]
        public void ListSubdirectoriesSorted_sorts_the_paths_and_filters_out_non_directories()
        {
            var fileSystemMock = IFileSystemTestUtils.CreateMock();
            IFileSystemTestUtils.SetupListDir(fileSystemMock, _testPath, @"abc\yyz", @"bbc\xyz", @"abc\xyz");

            IFileSystemTestUtils.SetupExistance(fileSystemMock, new FileSystemPath(@"abc\yyz"), IFileSystemTestUtils.PathExistance.File);
            IFileSystemTestUtils.SetupExistance(fileSystemMock, new FileSystemPath(@"bbc\xyz"), IFileSystemTestUtils.PathExistance.Directory);
            IFileSystemTestUtils.SetupExistance(fileSystemMock, new FileSystemPath(@"abc\xyz"), IFileSystemTestUtils.PathExistance.Directory);

            var result = fileSystemMock.Object.ListSubdirectoriesSorted(_testPath);

            AssertPathList(result, @"abc\xyz", @"bbc\xyz");
        }

        [Test]
        public void ListDirectoryFilterByExtensionSorted_sorts_the_paths_and_filters_out_other_extensions()
        {
            var fileSystemMock = IFileSystemTestUtils.CreateMock();
            IFileSystemTestUtils.SetupListDir(fileSystemMock, _testPath, @"moo.foO", @"moo.bAr", @"moo.baz");

            var result = fileSystemMock.Object.ListDirectoryFilterByExtensionSorted(_testPath, "foo", "BAR");

            AssertPathList(result, @"moo.bAr", @"moo.foO");
        }

        private void AssertPathList(IEnumerable<FileSystemPath> paths, params string[] expected)
        {
            Assert.AreEqual(expected, paths.Select(path => path.PathString).ToArray());
        }

        #endregion

        #region Read/Write files

        [Test]
        public void ReadTextFile_passes_default_encoding()
        {
            var fileSystemMock = PathExistanceMock(IFileSystemTestUtils.PathExistance.File);

            fileSystemMock.Setup(m => m.ReadTextFile(_testPath, null)).Returns("moo").Verifiable();

            fileSystemMock.Object.ReadTextFile(_testPath);

            fileSystemMock.Verify(m => m.ReadTextFile(_testPath, null));
        }

        [Test]
        public void WriteTextFile_passes_default_encoding()
        {
            var fileSystemMock = PathExistanceMock(IFileSystemTestUtils.PathExistance.File);

            fileSystemMock.Setup(m => m.WriteTextFile(_testPath, "moo", null, false)).Verifiable();

            fileSystemMock.Object.WriteTextFile(_testPath, "moo");

            fileSystemMock.Verify(m => m.WriteTextFile(_testPath, "moo", null, false));
        }

        #endregion

        private Mock<IFileSystem> PathExistanceMock(IFileSystemTestUtils.PathExistance existance)
        {
            var fileSystemMock = new Mock<IFileSystem>(MockBehavior.Strict);

            IFileSystemTestUtils.SetupExistance(fileSystemMock, _testPath, existance);

            return fileSystemMock;
        }
    }
}
