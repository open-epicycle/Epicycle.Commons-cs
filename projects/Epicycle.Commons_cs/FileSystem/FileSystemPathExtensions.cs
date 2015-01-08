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

using System;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons.FileSystem
{
    /// <summary>
    /// Various utilities for FileSystemPath objects.
    /// </summary>
    public static class FileSystemPathExtensions
    {
        /// <summary>
        /// Returns the extension of the last part of the path. The extension is a string that follows the last
        /// instance of the '.' character in the part (unless it is the first character of the part).
        /// </summary>
        /// <param name="path">The extended object</param>
        /// <returns>The extension. If there is no extension an empty string is returned.</returns>
        public static string GetExtension(this FileSystemPath path)
        {
            var lastPart = path.LastPart;

            var separatorPos = FindExtensionSeparatorPosition(lastPart);

            if (separatorPos < 0)
            {
                return "";
            }

            return lastPart.Substring(separatorPos + 1);
        }

        /// <summary>
        /// Checks if the path has one of the provided extensions. The test ignores the cases of the path and the extensions.
        /// </summary>
        /// <param name="path">The extended object</param>
        /// <param name="extensions">The extension list to test agains. Must not be null or contain nulls.</param>
        /// <returns>True if the path's extension is one of the provided extensions.</returns>
        public static bool IsExtension(this FileSystemPath path, params string[] extensions)
        {
            ArgAssert.NoNullIn(extensions, "extensions");

            return extensions.Contains(path.GetExtension(), StringComparer.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns the last part of the path without the extension. The extension is a string that follows the last
        /// instance of the '.' character in the part (unless it is the first character of the part).
        /// </summary>
        /// <param name="path">The extended object</param>
        /// <returns>The last part without the extension.</returns>
        public static string GetLastPartWithoutExtension(this FileSystemPath path)
        {
            var lastPart = path.LastPart;

            var separatorPos = FindExtensionSeparatorPosition(lastPart);

            if (separatorPos < 0)
            {
                return lastPart;
            }

            return lastPart.Substring(0, separatorPos);
        }

        /// <summary>
        /// Finds the extension separator
        /// </summary>
        private static int FindExtensionSeparatorPosition(string pathPart)
        {
            var pos = pathPart.LastIndexOf('.');

            return (pos > 0) ? pos : -1;
        }
    }
}
