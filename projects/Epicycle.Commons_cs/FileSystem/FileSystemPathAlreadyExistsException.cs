// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Thrown when a path was expected not to point to an existing file system object.
    /// </summary>
    public class FileSystemPathAlreadyExistsException : FileSystemPathException
    {
        public FileSystemPathAlreadyExistsException(FileSystemPath path) :
            this(path, "Path already exists.") { }

        public FileSystemPathAlreadyExistsException(FileSystemPath path, string message) :
            base(path, message) { }
    }
}
