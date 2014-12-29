using Epicycle.Commons.FileSystem;

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This class is a base for objects that are based on a specific file on a file system.
    /// </summary>
    public abstract class FileBasedObject : FileSystemPathBasedObject
    {
        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="fileSystem">The file system to use. Must not be null.</param>
        /// <param name="path">The path the object is based on. Must not be null and point to a file.</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public FileBasedObject(IFileSystem fileSystem, FileSystemPath path)
            : base(fileSystem, path)
        {
            fileSystem.AssertFile(path);
        }
    }
}
