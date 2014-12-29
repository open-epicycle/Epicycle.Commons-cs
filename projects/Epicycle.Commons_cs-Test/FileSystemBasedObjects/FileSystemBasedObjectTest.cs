using Epicycle.Commons.FileSystem;
using NUnit.Framework;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class FileSystemBasedObjectTest
    {
        [Test]
        public void FileSystem_returns_the_filesystem_from_constructor()
        {
            var mockFileSystem = IFileSystemTestUtils.CreateMock();

            var testObject = new TestFileSystemBasedObject(mockFileSystem.Object);

            Assert.AreSame(mockFileSystem.Object, testObject.FileSystem);
        }

        private class TestFileSystemBasedObject : FileSystemBasedObject
        {
            public TestFileSystemBasedObject(IFileSystem fileSystem) : base(fileSystem) { }
        }
    }
}
