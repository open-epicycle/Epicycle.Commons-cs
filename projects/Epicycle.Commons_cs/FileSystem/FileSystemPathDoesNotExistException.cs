// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Thrown when a path was expected to point to a file system object.
    /// </summary>
    public class FileSystemPathDoesNotExistException : FileSystemPathException
    {
        public FileSystemPathDoesNotExistException(FileSystemPath path) :
            this(path, "Path does not exists.") { }

        public FileSystemPathDoesNotExistException(FileSystemPath path, string message) :
            base(path, message) { }
    }
}
