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
using Moq;
using System.Linq;

namespace Epicycle.Commons.TestUtils.FileSystem
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

        public static void SetupExistance(this Mock<IFileSystem> @this, FileSystemPath path, PathExistance existance)
        {
            @this.Setup(m => m.Exists(path)).Returns(existance != PathExistance.DoesntExist);
            @this.Setup(m => m.IsFile(path)).Returns(existance == PathExistance.File);
            @this.Setup(m => m.IsDirectory(path)).Returns(existance == PathExistance.Directory);
        }

        public static void SetupListDir(this Mock<IFileSystem> @this, FileSystemPath path, params string[] listResult)
        {
            SetupExistance(@this, path, IFileSystemTestUtils.PathExistance.Directory);
            @this.Setup(m => m.ListDirectory(path)).Returns(listResult.Select(subPath => new FileSystemPath(subPath)));
        }

        public static void SetupTextFile(this Mock<IFileSystem> @this, FileSystemPath path, string data)
        {
            SetupExistance(@this, path, PathExistance.File);
            @this.Setup(m => m.ReadTextFile(path, null)).Returns(data);
        }

        public static void SetupWritableFile(this Mock<IFileSystem> @this, FileSystemPath path, string expected, bool exists = false)
        {
            SetupExistance(@this, path, exists ? PathExistance.File : PathExistance.DoesntExist);
            @this.Setup(m => m.WriteTextFile(path, It.IsAny<string>(), null, It.IsAny<bool>())).Callback(() => SetupTextFile(@this, path, expected)).Verifiable();
        }

        public static void AssertFileWritten(this Mock<IFileSystem> @this, FileSystemPath path, string expectedData, bool expectedAppend = false)
        {
            @this.Verify(m => m.WriteTextFile(path, expectedData, null, expectedAppend));
        }
    }
}
