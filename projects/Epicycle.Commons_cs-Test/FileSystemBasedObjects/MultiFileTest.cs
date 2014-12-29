using Epicycle.Commons.FileSystem;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class MultiFileTest : AssertionHelper
    {
        private Mock<IFileSystem> _mockFileSystem;
        private FileSystemPath _path;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = IFileSystemTestUtils.CreateMock();
            _path = new FileSystemPath(@"foo\bar");
        }

        //[Test] // TODO: restore
        public void Prefix_based_ctor_loads_all_files_with_given_prefix()
        {
            SetUpDirectory();
            ValidateMultiFile(new MultiFile(_mockFileSystem.Object, _path, "foo"));
        }

        //[Test] // TODO: restore
        public void Main_file_based_ctor_loads_all_files_with_given_prefix()
        {
            SetUpDirectory();

            var mainFile = _path.Join("Foo.boo");

            IFileSystemTestUtils.SetupExistance(_mockFileSystem, mainFile, IFileSystemTestUtils.PathExistance.File);
            ValidateMultiFile(new MultiFile(_mockFileSystem.Object, mainFile));
        }

        private void SetUpDirectory()
        {
            string[] files = { "foo.moo", "Foo.boo", "foo-xyz.goo", "bar.moo" };

            IFileSystemTestUtils.SetupListDir(_mockFileSystem, _path, files);
        }

        private void ValidateMultiFile(MultiFile multiFile)
        {
            var foundFiles = multiFile.Files.ToList();

            Expect(foundFiles.Count, Is.EqualTo(3));

            ValidateMultiFileFile(foundFiles[0], "Foo.boo", "", "boo");
            ValidateMultiFileFile(foundFiles[1], "foo.moo", "", "moo");
            ValidateMultiFileFile(foundFiles[2], "foo-xyz.goo", "-xyz", "goo");

        }

        private void ValidateMultiFileFile(MultiFile.MultiFileFile file, string expectedName, string expectedSuffix, string expectedExtension)
        {
            Expect(file.Path.PathString, Is.EqualTo(_path.Join(expectedName).PathString));
            Expect(file.Suffix, Is.EqualTo(expectedSuffix));
            Expect(file.Extension, Is.EqualTo(expectedExtension));
        }
    }
}
