// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Thrown when a directory was expected, but a different file system object was found.
    /// </summary>
    public class DirectoryExpectedException : FileSystemPathException
    {
        public DirectoryExpectedException(FileSystemPath path) :
            this(path, "Directory was expected.") { }

        public DirectoryExpectedException(FileSystemPath path, string message) :
            base(path, message) { }
    }
}
