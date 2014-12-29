using Epicycle.Commons.FileSystem;

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This is the base class for all objects that are based on a file system.
    /// </summary>
    public abstract class FileSystemBasedObject
    {
        /// <summary>
        /// The file system the object is based on
        /// </summary>
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// C-tor
        /// </summary>
        /// <param name="fileSystem">The file system the object is based on. Must not be null</param>
        public FileSystemBasedObject(IFileSystem fileSystem)
        {
            ArgAssert.NotNull(fileSystem, "fileSystem");

            _fileSystem = fileSystem;
        }

        /// <summary>
        /// The file system the object is based on
        /// </summary>
        public IFileSystem FileSystem
        {
            get { return _fileSystem; }
        }
    }
}
