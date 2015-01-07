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
using System.Linq;

namespace Epicycle.Commons.FileSystem
{
    [TestFixture]
    class FileSystemPathTest
    {
        #region Canonization

        [Test]
        public void Canonization_empty_path_yields_empty_path()
        {
            AssertPath(@"");
        }

        [Test]
        public void Canonization_root_path_yields_root_path()
        {
            AssertPath(@"\");
        }

        [Test]
        public void Canonization_single_part_remains_the_same()
        {
            AssertPath(@"foo");
        }

        [Test]
        public void Canonization_multiple_parts_remain_the_same()
        {
            AssertPath(@"foo\bar\baz");
        }

        [Test]
        public void Canonization_relative_to_root_single_part_remains_the_same()
        {
            AssertPath(@"\foo");
        }

        [Test]
        public void Canonization_relative_to_root_multiple_parts_remain_the_same()
        {
            AssertPath(@"\foo\bar\baz");
        }

        [Test]
        public void Canonization_trailing_delimiter_is_removed()
        {
            AssertPath(@"\bar\foo\boo", @"\bar\foo\boo\");
        }

        [Test]
        public void Canonization_delimiters_are_canonized()
        {
            AssertPath(@"\bar\foo\boo", @"/bar\foo/boo");
        }

        [Test]
        public void Canonization_duplicate_delimiters_are_removed()
        {
            AssertPath(@"\bar\foo\goo\boo", @"\//\bar/\foo///goo\\\boo\\\");
        }

        private void AssertPath(string input)
        {
            AssertPath(input, input);
        }

        private void AssertPath(string expected, string input)
        {
            Assert.AreEqual(expected, new FileSystemPath(input).PathString);
        }

        #endregion

        #region Join

        [Test]
        public void ToString_return_path_as_is()
        {
            Assert.AreEqual(@"foo\bar", new FileSystemPath(@"foo\bar").PathString);
        }

        [Test]
        public void Join_no_parts_result_is_same()
        {
            AssertJoin(@"foo\bar", @"foo\bar");
        }

        [Test]
        public void Join_simple_parts_join_well()
        {
            AssertJoin(@"foo\bar\moo\booga", @"foo\bar", "moo", "booga");
        }

        [Test]
        public void Join_empty_parts_are_ignored()
        {
            AssertJoin(@"foo\bar\moo\booga", @"foo\bar", "moo", "", "booga", "");
        }

        [Test]
        public void Join_delimiters_are_incorporated_and_duplicates_removed()
        {
            AssertJoin(@"foo\bar\ab\c\def\booga", @"foo\bar/", @"\\ab/c//", @"\\def\", @"booga\\");
        }

        [Test]
        public void JoinPaths_simple_parts_join_well()
        {
            Assert.AreEqual(@"foo\bar\moo\booga", new FileSystemPath(@"foo\bar").Join(new FileSystemPath(@"moo"), new FileSystemPath(@"booga")).PathString);
        }

        private void AssertJoin(string expected, string pathString, params string[] parts)
        {
            Assert.AreEqual(expected, new FileSystemPath(pathString).Join(parts).PathString);
        }

        #endregion

        #region

        [Test]
        public void SubPath_of_empty_path_is_empty_path()
        {
            AssertSubPath(@"", @"");
        }

        [Test]
        public void SubPath_of_root_path_is_empty_path()
        {
            AssertSubPath(@"", @"\");
        }

        [Test]
        public void SubPath_of_single_relative_to_root_part_path_is_same_part_not_relative_to_root()
        {
            AssertSubPath(@"foo", @"\foo");
        }

        [Test]
        public void SubPath_of_relative_to_root_path_is_same_path_not_relative_to_root()
        {
            AssertSubPath(@"foo\bar\baz", @"\foo\bar\baz");
        }

        [Test]
        public void SubPath_of_single_part_path_is_empty_path()
        {
            AssertSubPath(@"", @"foo");
        }


        [Test]
        public void SubPath_of_multiple_part_path_is_all_parts_except_first()
        {
            AssertSubPath(@"bar\baz", @"foo\bar\baz");
        }

        private void AssertSubPath(string expected, string pathString)
        {
            Assert.AreEqual(expected, new FileSystemPath(pathString).SubPath.PathString);
        }

        #endregion

        #region Parent

        [Test]
        public void Parent_of_empty_path_is_empty_path()
        {
            AssertParent("", "");
        }

        [Test]
        public void Parent_of_root_path_is_root_path()
        {
            AssertParent(@"\", @"\");
        }

        [Test]
        public void Parent_of_single_part_path_is_empty_path()
        {
            AssertParent(@"", @"foo");
        }

        [Test]
        public void Parent_of_relative_to_root_single_part_path_is_root_path()
        {
            AssertParent(@"\", @"\foo");
        }

        [Test]
        public void Parent_of_multi_part_path_is_all_parts_without_last()
        {
            AssertParent(@"foo\bar", @"foo\bar\baz");
        }

        private void AssertParent(string expected, string pathString)
        {
            Assert.AreEqual(expected, new FileSystemPath(pathString).Parent.PathString);
        }

        #endregion

        #region FirstPart

        [Test]
        public void FirstPart_empty_path_yeilds_empty_part()
        {
            AssertFirstPart(@"", @"");
        }

        [Test]
        public void FirstPart_root_path_yeilds_empty_part()
        {
            AssertFirstPart(@"", @"\");
        }

        [Test]
        public void FirstPart_single_relative_to_root_part_yeilds_empty_string()
        {
            AssertFirstPart(@"", @"\foo");
        }

        [Test]
        public void FirstPart_multiple_relative_to_root_parts_yeild_empty_string()
        {
            AssertFirstPart(@"", @"\foo\bar\baz");
        }

        [Test]
        public void FirstPart_single_part_yeilds_itself()
        {
            AssertFirstPart(@"foo", @"foo");
        }

        [Test]
        public void FirstPart_multiple_parts_yeild_first_part()
        {
            AssertFirstPart(@"foo", @"foo\bar\baz");
        }

        private void AssertFirstPart(string expected, string pathString)
        {
            Assert.AreEqual(expected, new FileSystemPath(pathString).FirstPart);
        }

        #endregion

        #region LastPart

        [Test]
        public void LastPart_empty_path_yeilds_empty_part()
        {
            AssertLastPart(@"", @"");
        }

        [Test]
        public void LastPart_root_path_yeilds_empty_part()
        {
            AssertLastPart(@"", @"\");
        }

        [Test]
        public void LastPart_single_part_yeilds_itself()
        {
            AssertLastPart(@"foo", @"foo");
        }

        [Test]
        public void LastPart_single_relative_to_root_part_yeilds_itself()
        {
            AssertLastPart(@"foo", @"\foo");
        }

        [Test]
        public void LastPart_multiple_parts_yeild_last_part()
        {
            AssertLastPart(@"baz", @"foo\bar\baz");
        }

        private void AssertLastPart(string expected, string pathString)
        {
            Assert.AreEqual(expected, new FileSystemPath(pathString).LastPart);
        }

        #endregion

        #region Iteration

        [Test]
        public void Iteration_empty_path_has_one_empty_part()
        {
            TestIteration(@"", "");
        }

        [Test]
        public void Iteration_root_path_has_two_parts()
        {
            TestIteration(@"\", "", "");
        }

        [Test]
        public void Iteration_single_part_path_has_single_part()
        {
            TestIteration(@"foo", "foo");
        }

        [Test]
        public void Iteration_path_iterates_over_multiple_parts()
        {
            TestIteration(@"foo\bar\moo", "foo", "bar", "moo");
        }

        [Test]
        public void Iteration_absolute_path_start_with_empty_part()
        {
            TestIteration(@"\foo\bar\moo", "", "foo", "bar", "moo");
        }

        private void TestIteration(string path, params string[] expectedParts)
        {
            Assert.AreEqual(expectedParts, new FileSystemPath(path).ToArray());
        }

        #endregion

        #region Comparison

        [Test]
        public void CompareTo_standard_lexicographic_order()
        {
            AssertCompareToSmaller("abc", "bbc");
            AssertCompareToSmaller("abc", "acc");
        }

        [Test]
        public void CompareTo_standard_lexicographic_case_ignored()
        {
            AssertCompareToSmaller("abc", "BCE");
            AssertCompareToSmaller("ABC", "bce");
        }

        [Test]
        public void CompareTo_multipart_paths_compred_by_parent_path_prarts_first()
        {
            AssertCompareToSmaller("abc/abc", "abc/xyz");
            AssertCompareToSmaller("abc/xyz", "xyz/abc");
        }

        [Test]
        public void CompareTo_same_paths_are_equal()
        {
            AssertCompareToEqual("foo/bar", "foo/bar");
        }

        [Test]
        public void CompareTo_same_paths_with_different_case_are_equal()
        {
            AssertCompareToEqual("foo/bar", "Foo/Bar");
        }

        private void AssertCompareToSmaller(string path1, string path2)
        {
            Assert.Less(new FileSystemPath(path1).CompareTo(new FileSystemPath(path2)), 0);
            Assert.Greater(new FileSystemPath(path2).CompareTo(new FileSystemPath(path1)), 0);
        }

        private void AssertCompareToEqual(string path1, string path2)
        {
            Assert.AreEqual(0, new FileSystemPath(path1).CompareTo(new FileSystemPath(path2)));
        }

        #endregion
    }
}
