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


using NUnit.Framework;

namespace Epicycle.Commons.FileSystem
{
    public class FileSystemPathExtensionsTest
    {
        #region GetExtension

        [Test]
        public void GetExtension_no_extension_returns_empty_string()
        {
            TestGetExtension("", "foo/bar");
        }

        [Test]
        public void GetExtension_first_character_dot_doesnt_count_as_extension_separator()
        {
            TestGetExtension("", "foo/.bar");
        }

        [Test]
        public void GetExtension_last_character_dot_returns_empty_exception()
        {
            TestGetExtension("", "foo/bar.");
        }

        [Test]
        public void GetExtension_parent_extensions_are_ignore()
        {
            TestGetExtension("", "foo.ext/bar");
        }

        [Test]
        public void GetExtension_simple_extension_returns_the_extension()
        {
            TestGetExtension("ext", "foo/bar.ext");
        }

        [Test]
        public void GetExtension_multiple_dots_returns_the_extension_after_last_dot()
        {
            TestGetExtension("ext", "foo/bar.moo.ext");
        }

        private void TestGetExtension(string expectedExtension, string path)
        {
            Assert.AreEqual(expectedExtension, new FileSystemPath(path).GetExtension());
        }

        #endregion

        #region IsExtension

        [Test]
        public void IsExtension_empty_extention_list_doesnt_match()
        {
            TestIsExtension(false, "foo/bar.ext");
        }

        [Test]
        public void IsExtension_extention_list_doesnt_match()
        {
            TestIsExtension(false, "foo/bar.ext", "booga", "moo");
        }

        [Test]
        public void IsExtension_extention_list_matches()
        {
            TestIsExtension(true, "foo/bar.ext", "booga", "ext", "moo");
        }

        [Test]
        public void IsExtension_ignores_case()
        {
            TestIsExtension(true, "foo/bar.exT", "eXt");
        }

        private void TestIsExtension(bool expected, string path, params string[] extensions)
        {
            Assert.AreEqual(expected, new FileSystemPath(path).IsExtension(extensions));
        }

        #endregion

        #region GetLastPartWithoutExtension

        [Test]
        public void GetLastPartWithoutExtension_no_extension_returns_last_part()
        {
            TestGetLastPartWithoutExtension("bar", "foo/bar");
        }

        [Test]
        public void GetLastPartWithoutExtension_first_character_dot_doesnt_count_as_extension_separator()
        {
            TestGetLastPartWithoutExtension(".bar", "foo/.bar");
        }

        [Test]
        public void GetLastPartWithoutExtension_last_character_dot_returns_last_part_without_dot()
        {
            TestGetLastPartWithoutExtension("bar", "foo/bar");
        }

        [Test]
        public void GetLastPartWithoutExtension_simple_extension_returns_the_part_before_extension()
        {
            TestGetLastPartWithoutExtension("bar", "foo/bar.ext");
        }

        [Test]
        public void GetLastPartWithoutExtension_multiple_dots_returns_the_part_until_last_dor()
        {
            TestGetLastPartWithoutExtension("bar.moo", "foo/bar.moo.ext");
        }

        private void TestGetLastPartWithoutExtension(string expected, string path)
        {
            Assert.AreEqual(expected, new FileSystemPath(path).GetLastPartWithoutExtension());
        }

        #endregion
    }
}
