using System.IO;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// A file system path based exceotion
    /// </summary>
    public abstract class FileSystemPathException : IOException
    {
        /// <summary>
        /// The offending path.
        /// </summary>
        private readonly FileSystemPath _path;

        /// <summary>
        /// C-tor.
        /// </summary>
        /// <param name="path">The offending path.</param>
        /// <param name="message">A message</param>
        public FileSystemPathException(FileSystemPath path, string message) :
            base(string.Format("{0} ({1})", message, path))
        {
            _path = path;
        }

        /// <summary>
        /// The offending path.
        /// </summary>
        public FileSystemPath Path
        {
            get { return _path; }
        }
    }
}
