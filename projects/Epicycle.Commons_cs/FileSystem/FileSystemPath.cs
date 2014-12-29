using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Represents a path in a file system. Currently only Windows paths are supported.
    /// The path will be canonized using the following rules:
    /// <list type="bullet">
    ///     <item>
    ///         <term>Path delimiter canonization</term>
    ///         <description>All slashes will be converted to back-slashes</description>
    ///     </item>
    ///     <item>
    ///         <term>Multiple path delimiter removal</term>
    ///         <description>Any number of path delimiteres in a row will be converted into a single path delimiter</description>
    ///     </item>
    ///     <item>
    ///         <term>Trailing path delimiter removal</term>
    ///         <description>If the path ends with a path delimiter it will be removed</description>
    ///     </item>
    /// </list>
    /// This object is immutable
    /// Note: The path string will NOT be checked for invalid characters.
    /// </summary>
    public class FileSystemPath : IEnumerable<string>, IComparable<FileSystemPath>
    {
        /// <summary>
        /// The delimiter that is used in all the paths.
        /// </summary>
        public static char PathDelimiter = Path.DirectorySeparatorChar;

        /// <summary>
        /// An empty path (represented by an empty string)
        /// </summary>
        public static readonly FileSystemPath Empty = new FileSystemPath("");

        /// <summary>
        /// The root path ("/")
        /// </summary>
        public static readonly FileSystemPath Root = new FileSystemPath(PathDelimiter.ToString());

        /// <summary>
        /// The path represented as a string
        /// </summary>
        private readonly string _pathString;

        /// <summary>
        /// Creates a new path. The provided string is always canonized. For canonization rulles see the class description.
        /// </summary>
        /// <param name="pathString">The string to build the path from. Must not be null.</param>
        public FileSystemPath(string pathString)
        {
            ArgAssert.NotNull(pathString, "pathString");

            _pathString = CanonizePath(pathString);
        }

        #region String represintation

        /// <summary>
        /// A string represintation of the path. Note that it may not be the same as the string passed to the constructor
        /// due to canonization.
        /// </summary>
        public string PathString
        {
            get { return _pathString; }
        }

        /// <summary>
        /// Returns a string representation of the path.
        /// </summary>
        /// <returns>The string representation of the path.</returns>
        public override string ToString()
        {
            return PathString;
        }

        #endregion


        #region Path properties

        /// <summary>
        /// Checks if the path is relative to root (starts with a delimiter)
        /// </summary>
        private bool IsRelativeToRoot
        {
            get { return _pathString.StartsWith(PathDelimiter.ToString()); }
        }

        #endregion

        #region Path subsets

        /// <summary>
        /// Returns a sub-path of the current path. It includes everything that follows the first part.
        /// </summary>
        /// <see cref="FirstPart"/>
        public FileSystemPath SubPath
        {
            get
            {
                var firstDelimiterPos = FindFirstDelimiter(_pathString);

                if (firstDelimiterPos < 0)
                {
                    return Empty;
                }

                return new FileSystemPath(_pathString.Substring(firstDelimiterPos + 1));
            }
        }

        /// <summary>
        /// The parent path of this path. A parent path contain all path parts except the last. If the path contains
        /// only one part, an empty part will be return in case of relative path and a root path in case of an relative to root
        /// path. The parent of an empty path is empty path. The parent of the root path is the root path.
        /// </summary>
        /// <see cref="LastPart"/>
        public FileSystemPath Parent
        {
            get
            {
                var lastDelimiterPos = FindLastDelimiter(_pathString);

                if (lastDelimiterPos < 0)
                {
                    return Empty;
                }

                if (IsRelativeToRoot && lastDelimiterPos == 0)
                {
                    return Root;
                }

                return new FileSystemPath(_pathString.Substring(0, lastDelimiterPos));
            }
        }

        /// <summary>
        /// The first part of the path. Empty in case of an empty path or an relative to root path.
        /// </summary>
        /// <see cref="SubPath"/>
        public string FirstPart
        {
            get
            {
                var firstDelimiterPos = FindFirstDelimiter(_pathString);

                if (firstDelimiterPos < 0)
                {
                    return _pathString;
                }

                return _pathString.Substring(0, firstDelimiterPos);
            }
        }

        /// <summary>
        /// The last part of the path. In case of an empty path or the root path an empty string is returned.
        /// </summary>
        /// <see cref="Parent"/>
        public string LastPart
        {
            get
            {
                var lastDelimiterPos = FindLastDelimiter(_pathString);

                if (lastDelimiterPos < 0)
                {
                    return _pathString;
                }

                return _pathString.Substring(lastDelimiterPos + 1);
            }
        }

        /// <summary>
        /// Enumerates the path parts.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var part in _pathString.Split(PathDelimiter))
            {
                yield return part;
            }
        }

        /// <summary>
        /// Enumerates the path parts.
        /// </summary>
        /// <returns>The enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Path composition

        /// <summary>
        /// Adds the provided path parts to the path creating a new path. The original path is unaffected.
        /// The parts may contian path delimiters or be empty (but can't be null). The resulting path will be canonized.
        /// </summary>
        /// <param name="parts">The path parts to add to this path. Must not be null or contain nulls.</param>
        /// <returns>The resulting path</returns>
        public FileSystemPath Join(params string[] parts) {
            ArgAssert.NoNullIn(parts, "parts");

            if (parts.Length == 0)
            {
                return this;
            }

            return new FileSystemPath(JoinPathParts(PathString, JoinPathParts(parts)));
        }
        
        /// <summary>
        /// Adds the provided path parts to the path creating a new path. The original path is unaffected.
        /// The parts may contian path delimiters or be empty (but can't be null). The resulting path will be canonized.
        /// </summary>
        /// <param name="parts">The path parts to add to this path. Must not be null or contain nulls.</param>
        /// <returns>The resulting path</returns>
        public FileSystemPath Join(params FileSystemPath[] parts)
        {
            ArgAssert.NoNullIn(parts, "parts");
            return Join(parts.Select(path => path.PathString).ToArray());
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares two paths lexicographically using their string representation. The comparison ignores the case.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(FileSystemPath other)
        {
            return StringComparer.InvariantCultureIgnoreCase.Compare(PathString, other.PathString);
        }

        #endregion

        #region Private utilties

        /// <summary>
        /// Joins the path parts using the path delimiter
        /// </summary>
        private static string JoinPathParts(params string[] parts)
        {
            return string.Join(PathDelimiter.ToString(), parts);
        }

        /// <summary>
        /// Finds the first occurance of the path delimiter in the give path string.
        /// </summary>
        private int FindFirstDelimiter(string pathString)
        {
            return pathString.IndexOf(PathDelimiter);
        }

        /// <summary>
        /// Finds the last occurance of the path delimiter in the give path string.
        /// </summary>
        private int FindLastDelimiter(string pathString)
        {
            return pathString.LastIndexOf(PathDelimiter);
        }

        /// <summary>
        /// Canonizes a path
        /// </summary>
        private static string CanonizePath(string pathString)
        {
            var canonizedDelimeters = pathString.Replace('/', PathDelimiter).Replace('\\', PathDelimiter);

            var pathParts = canonizedDelimeters.Split(PathDelimiter).ToList();


            var isRelativeToRootPath = (pathParts[0] == "") && (pathParts.Count > 1);

            var nonEmptyParts = pathParts.Where(part => part != "");

            var result = JoinPathParts(nonEmptyParts.ToArray());

            if (isRelativeToRootPath)
            {
                result = PathDelimiter + result;
            }

            return result;
        }

        #endregion
    }
}
