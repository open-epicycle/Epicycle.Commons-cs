using Epicycle.Commons.FileSystem;
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
