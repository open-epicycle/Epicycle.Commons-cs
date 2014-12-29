// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Thrown when a file was expected, but a different file system object was found.
    /// </summary>
    public class FileExpectedException : FileSystemPathException
    {
        public FileExpectedException(FileSystemPath path) :
            this(path, "File was expected.") { }

        public FileExpectedException(FileSystemPath path, string message) :
            base(path, message) { }
    }
}
