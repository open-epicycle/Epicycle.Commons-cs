using Epicycle.Commons.FileSystem;

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This class is a base for objects that are based on a specific path on a file system. The path is
    /// mutable and might change during the lifetime of the object.
    /// </summary>
    public abstract class FileSystemPathBasedObject : FileSystemBasedObject
    {
        /// <summary>
        /// The path the object is based on.
        /// </summary>
        private FileSystemPath _path;

        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="fileSystem">The file system to use. Must not be null.</param>
        /// <param name="path">The path the object is based on. Must not be null.</param>
        public FileSystemPathBasedObject(IFileSystem fileSystem, FileSystemPath path) : base(fileSystem)
        {
            ArgAssert.NotNull(path, "path");

            _path = path;
        }

        /// <summary>
        /// The path the object is based on. Never null.
        /// </summary>
        public FileSystemPath Path
        {
            get { return _path; }
            protected set 
            {
                ArgAssert.NotNull(value, "value");
                _path = value;
            }
        }
    }
}
