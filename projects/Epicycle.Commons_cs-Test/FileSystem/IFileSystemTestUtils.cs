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

using Moq;
using System.Linq;

namespace Epicycle.Commons.FileSystem
{
    public static class IFileSystemTestUtils
    {
        public enum PathExistance
        {
            DoesntExist,
            File,
            Directory
        }

        public static Mock<IFileSystem> CreateMock()
        {
            return new Mock<IFileSystem>(MockBehavior.Strict);
        }

        public static void SetupExistance(Mock<IFileSystem> fileSystemMock, FileSystemPath path, PathExistance existance)
        {
            fileSystemMock.Setup(m => m.Exists(path)).Returns(existance != PathExistance.DoesntExist);
            fileSystemMock.Setup(m => m.IsFile(path)).Returns(existance == PathExistance.File);
            fileSystemMock.Setup(m => m.IsDirectory(path)).Returns(existance == PathExistance.Directory);
        }

        public static void SetupListDir(Mock<IFileSystem> fileSystemMock, FileSystemPath path, params string[] listResult)
        {
            SetupExistance(fileSystemMock, path, IFileSystemTestUtils.PathExistance.Directory);
            fileSystemMock.Setup(m => m.ListDirectory(path)).Returns(listResult.Select(subPath => new FileSystemPath(subPath)));
        }

        public static void SetupTextFile(Mock<IFileSystem> fileSystemMock, FileSystemPath path, string data)
        {
            SetupExistance(fileSystemMock, path, PathExistance.File);
            fileSystemMock.Setup(m => m.ReadTextFile(path, null)).Returns(data);
        }

        public static void SetupWritableFile(Mock<IFileSystem> fileSystemMock, FileSystemPath path, string expected, bool exists=false)
        {
            SetupExistance(fileSystemMock, path, exists ? PathExistance.File : PathExistance.DoesntExist);
            fileSystemMock.Setup(m => m.WriteTextFile(path, It.IsAny<string>(), null, false)).Callback(() => SetupTextFile(fileSystemMock, path, expected)).Verifiable();
        }

        public static void AssertFileWritten(Mock<IFileSystem> fileSystemMock, FileSystemPath path, string expectedData)
        {
            fileSystemMock.Verify(m => m.WriteTextFile(path, expectedData, null, false));
        }
    }
}
