using Epicycle.Commons.FileSystem;
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
