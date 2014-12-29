using System.Collections.Generic;
using System.Text;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// This interface represents a file system abstraction.
    /// </summary>
    public interface IFileSystem
    {
        #region Path target checks

        /// <summary>
        /// Checks the existance of the path
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>True if the path exists</returns>
        bool Exists(FileSystemPath path);
        
        /// <summary>
        /// Checks if the path points to a file.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>True if the path points to a file</returns>
        bool IsFile(FileSystemPath path);

        /// <summary>
        /// Checks if the path points to a directory.
        /// </summary>
        /// <param name="path">The path to check</param>
        /// <returns>True if the path points to a directory</returns>
        bool IsDirectory(FileSystemPath path);

        #endregion

        #region File system tree manipulation

        /// <summary>
        /// Creates a directory recursively.
        /// </summary>
        /// <param name="path">The path to the directory to create. Must not exist</param>
        /// <exception cref="FileSystemPathAlreadyExistsException">Thrown if the path already exists.</exception>
        void CreateDirectoryRecursively(FileSystemPath path);

        #endregion

        #region File system tree queries

        /// <summary>
        /// Non-recursive list of the given directory (both files and directories)
        /// </summary>
        /// <param name="path">The path of the directory to list. Must point to a directory.</param>
        /// <returns>The content of the directory. In no particular order</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="DirectoryExpectedException">Thrown if the path does not point to a directory.</exception>
        IEnumerable<FileSystemPath> ListDirectory(FileSystemPath path);

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="from">The file to copy. Must exist and point to a file.</param>
        /// <param name="to">The destination of the copy (must include the file name). If there is already a file at
        /// this path it will be overwritten. Must point to a file or at nothing.</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the source path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the source path or the destination path does not point to a file.</exception>
        void CopyFile(FileSystemPath from, FileSystemPath to);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="path">The path to the file to delete. Must exist and point to a file.</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        void DeleteFile(FileSystemPath path);

        #endregion

        #region Read/Write files

        /// <summary>
        /// Reads an entire binary file.
        /// </summary>
        /// <param name="path">The path of the file to read. Must exist and point to a file.</param>
        /// <returns>An array that contains the file data.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        byte[] ReadBinaryFile(FileSystemPath path);

        /// <summary>
        /// Reads an entire textual file.
        /// Note: The line endings are not converted.
        /// </summary>
        /// <param name="path">The path of the file to read. Must exist and point to a file.</param>
        /// <param name="encoding">The encoding to use. If null or omitted then UTF-8 is used.</param>
        /// <returns>A string that contains the file content.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        string ReadTextFile(FileSystemPath path, Encoding encoding);

        /// <summary>
        /// Writes binarty data into a file.
        /// </summary>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="append">If false the file will be created or overwritten. If false the data will be appended to the end.</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        void WriteBinaryFile(FileSystemPath path, byte[] data, bool append);

        /// <summary>
        /// Writes textual data into a file.
        /// </summary>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="encoding">The encoding to use. If null or omitted then UTF-8 is used.</param>
        /// <param name="append">If false the file will be created or overwritten. If false the data will be appended to the end.</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        void WriteTextFile(FileSystemPath path, string data, Encoding encoding, bool append);

        #endregion
    }
}
