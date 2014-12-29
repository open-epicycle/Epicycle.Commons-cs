using System.Collections.Generic;
using System.IO;
using System.Linq;

// Authors: untrots

// TODO: Test

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// A standatd file system implementation. Based on the System.IO APIs.
    /// This class is a singleton.
    /// </summary>
    public class StandardFileSystem: CommonFileSystem
    {
        #region Singleton

        /// <summary>
        /// The sole instance of the class.
        /// </summary>
        private static readonly StandardFileSystem _instance;

        /// <summary>
        /// Static c-tor
        /// </summary>
        static StandardFileSystem()
        {
            _instance = new StandardFileSystem();
        }

        /// <summary>
        /// Private c-tor to ensure a single instance.
        /// </summary>
        private StandardFileSystem() { }

        /// <summary>
        /// The sole instance of the class.
        /// </summary>
        public static StandardFileSystem Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Path target checks

        // See parent
        public override bool Exists(FileSystemPath path)
        {
            return IsFile(path) || IsDirectory(path);
        }

        // See parent
        public override bool IsFile(FileSystemPath path)
        {
            return File.Exists(path.PathString);
        }

        // See parent
        public override bool IsDirectory(FileSystemPath path)
        {
            return Directory.Exists(path.PathString);
        }

        #endregion

        #region File system tree manipulation

        // See parent
        protected override void InnerCreateDirectoryRecursively(FileSystemPath path)
        {
            Directory.CreateDirectory(path.PathString);
        }

        // See parent
        protected override void InnerCopyFile(FileSystemPath from, FileSystemPath to)
        {
            File.Copy(from.PathString, to.PathString, true);
        }

        // See parent
        protected override void InnerDeleteFile(FileSystemPath path)
        {
            File.Delete(path.PathString);
        }

        #endregion

        #region File system tree queries

        // See parent
        protected override IEnumerable<FileSystemPath> InnerListDirectory(FileSystemPath path)
        {
            var files = Directory.GetFiles(path.PathString);
            var directories = Directory.GetDirectories(path.PathString);

            return files.Concat(directories).Select(entry => new FileSystemPath(entry));
        }

        #endregion

        #region Read/Write files

        // See parent
        protected override byte[] InnerReadBinaryFile(FileSystemPath path)
        {
            return File.ReadAllBytes(path.PathString);
        }

        // See parent
        protected override void InnerWriteBinaryFile(FileSystemPath path, byte[] data, bool append)
        {
            if (append)
            {
                using (var fileStream = new FileStream(path.PathString, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var binaryWriter = new BinaryWriter(fileStream))
                    {
                        binaryWriter.Write(data);
                    }
                }
            }
            else
            {
                File.WriteAllBytes(path.PathString, data);
            }
        }

        #endregion
    }
}
