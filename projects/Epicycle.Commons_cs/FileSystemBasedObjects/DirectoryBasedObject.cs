using Epicycle.Commons.FileSystem;

// Authors: untrots

namespace Epicycle.Commons.FileSystemBasedObjects
{
    /// <summary>
    /// This class is a base for objects that are based on a specific directory on a file system.
    /// </summary>
    public abstract class DirectoryBasedObject : FileSystemPathBasedObject
    {
        /// <summary>
        /// C-tor. If autoCreateEmptyDirectory is true and the path does not exist then a new empty directory will be (recursevly) created.
        /// </summary>
        /// <param name="fileSystem">The file system to use. Must not be null.</param>
        /// <param name="path">The path the object is based on. Must not be null.</param>
        /// <param name="shouldAutoCreateEmptyDirectory">If true and the path does not exist then a new empty directory will be (recursevly) created</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path points to a non-existing directory and autoCreateEmptyDirectory is false.</exception>
        /// <exception cref="DirectoryExpectedException">In case the path points to a non-directory file system entity.</exception>
        public DirectoryBasedObject(IFileSystem fileSystem, FileSystemPath path, bool autoCreateEmptyDirectory)
            : base(fileSystem, path)
        {
            if (autoCreateEmptyDirectory)
            {
                fileSystem.EnsureDirectory(path);
            }
            else
            {
                fileSystem.AssertDirectory(path);
            }
        }
    }
}
