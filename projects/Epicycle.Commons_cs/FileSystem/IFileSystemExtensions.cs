// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

using System.Collections.Generic;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Various utilities for IFileSystem objects.
    /// </summary>
    public static class IFileSystemExtensions
    {
        #region Path assertions

        /// <summary>
        /// Asserts that the path exists.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        public static void AssertExists(this IFileSystem fileSystem, FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            if (!fileSystem.Exists(path))
            {
                throw new FileSystemPathDoesNotExistException(path);
            }
        }

        /// <summary>
        /// Asserts that the path does not exists.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        /// <exception cref="FileSystemPathAlreadyExistsException">Thrown if the path already exists.</exception>
        public static void AssertNotExists(this IFileSystem fileSystem, FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            if (fileSystem.Exists(path))
            {
                throw new FileSystemPathAlreadyExistsException(path);
            }
        }

        /// <summary>
        /// Asserts that the path points to a file if it exists.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        public static void AssertFileOrNotExsits(this IFileSystem fileSystem, FileSystemPath path)
        {
            if (fileSystem.Exists(path) && !fileSystem.IsFile(path))
            {
                throw new FileExpectedException(path);
            }
        }

        /// <summary>
        /// Asserts that the path points to a file.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static void AssertFile(this IFileSystem fileSystem, FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            if (!fileSystem.Exists(path))
            {
                throw new FileSystemPathDoesNotExistException(path, "File does not exist");
            }

            if (!fileSystem.IsFile(path))
            {
                throw new FileExpectedException(path);
            }
        }

        /// <summary>
        /// Asserts that the path points to a directory if it exists.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        public static void AssertDirectoryOrNotExsits(this IFileSystem fileSystem, FileSystemPath path)
        {
            if (fileSystem.Exists(path) && !fileSystem.IsDirectory(path))
            {
                throw new DirectoryExpectedException(path);
            }
        }
        /// <summary>
        /// Asserts that the path points to a directory.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to assert</param>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="DirectoryExpectedException">Thrown if the path does not point to a directory.</exception>
        public static void AssertDirectory(this IFileSystem fileSystem, FileSystemPath path)
        {
            ArgAssert.NotNull(path, "path");

            if (!fileSystem.Exists(path))
            {
                throw new FileSystemPathDoesNotExistException(path, "Directory does not exist");
            }

            if (!fileSystem.IsDirectory(path))
            {
                throw new DirectoryExpectedException(path);
            }
        }

        #endregion

        /// <summary>
        /// Ensures that the path to the directory exists. If it doesn't the directories will be created recursevly. If
        /// the path points to a directory, nothing will happen. If it points to a different object, an exception is
        /// thrown.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to the directory to create.</param>
        /// <returns>True if there was a need to create the directories</returns>
        /// <exception cref="DirectoryExpectedException">Thrown if the path points to a directory.</exception>
        public static bool EnsureDirectory(this IFileSystem fileSystem, FileSystemPath path)
        {
            if (fileSystem.Exists(path))
            {
                fileSystem.AssertDirectory(path);
                return false;
            }
            else
            {
                fileSystem.CreateDirectoryRecursively(path);
                return true;
            }
        }

        /// <summary>
        /// Non-recursive list of the given directory (both files and directories). The resulting paths will be lexicographically sorted.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="directoryPath">The path of the directory to list. Must point to a directory.</param>
        /// <returns>Sorted content of the directory.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="DirectoryExpectedException">Thrown if the path does not point to a directory.</exception>
        public static IEnumerable<FileSystemPath> ListDirectorySorted(this IFileSystem fileSystem, FileSystemPath directoryPath)
        {
            var directoryContent = fileSystem.ListDirectory(directoryPath).ToList();
            directoryContent.Sort();

            return directoryContent;
        }
        
        /// <summary>
        /// Non-recursive list of the sub-directories in the given directory. The resulting paths will be lexicographically sorted.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="directoryPath">The path of the directory to list. Must point to a directory.</param>
        /// <returns>Sorted sub-directories in the directory.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="DirectoryExpectedException">Thrown if the path does not point to a directory.</exception>
        public static IEnumerable<FileSystemPath> ListSubdirectoriesSorted(this IFileSystem fileSystem, FileSystemPath directoryPath)
        {
            return fileSystem.ListDirectorySorted(directoryPath).Where(path => fileSystem.IsDirectory(path));
        }

        /// <summary>
        /// Non-recursive list of the given directory (both files and directories) with a specific extension. The resulting paths will be lexicographically sorted.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="directoryPath">The path of the directory to list. Must point to a directory.</param>
        /// <param name="extensions">The allowed extensions</param>
        /// <returns>Sorted and filtered content of the directory.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="DirectoryExpectedException">Thrown if the path does not point to a directory.</exception>
        public static IEnumerable<FileSystemPath> ListDirectoryFilterByExtensionSorted(this IFileSystem fileSystem, FileSystemPath directoryPath, params string[] extensions)
        {
            return fileSystem.ListDirectorySorted(directoryPath).Where(path => path.IsExtension(extensions));
        }

        /// <summary>
        /// Reads an entire textual file using the default encoding (UTF-8).
        /// Note: The line endings are not converted.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path of the file to read. Must exist and point to a file.</param>
        /// <returns>A string that contains the file content.</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static string ReadTextFile(this IFileSystem fileSystem, FileSystemPath path)
        {
            return fileSystem.ReadTextFile(path, null);
        }

        /// <summary>
        /// Writes an entire binary file. If the file already exists it will be overwritten.
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="data">The data to write.</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static void WriteBinaryFile(this IFileSystem fileSystem, FileSystemPath path, byte[] data)
        {
            fileSystem.WriteBinaryFile(path, data, false);
        }

        /// <summary>
        /// Writes textual data into a file using the defult encoding (UTF-8).
        /// </summary>
        /// <param name="fileSystem">The extended object</param>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="data">The data to write.</param>
        /// <param name="append">If false the file will be created or overwritten. If false the data will be appended to the end.</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static void WriteTextFile(this IFileSystem fileSystem, FileSystemPath path, string data, bool append = false)
        {
            fileSystem.WriteTextFile(path, data, null, append);
        }
    }
}
