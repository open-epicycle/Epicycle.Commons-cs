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

namespace Epicycle.Commons
{
    [TestFixture]
    public class StringUtilsTest
    {
        [Test]
        public void ParseParseString_String_returns_same_string()
        {
            var str = "booga!";
            Assert.That(StringUtils.ParseString<string>(str), Is.EqualTo(str));
        }

        [Test]
        public void ParseParseString_Int_returns_correct_value()
        {
            var str = "123";
            var expected = 123;

            Assert.That(StringUtils.ParseString<int>(str), Is.EqualTo(expected));
        }
        
        [Test]
        public void ParseParseString_Long_returns_correct_value()
        {
            var str = "123456789012345";
            var expected = 123456789012345L;

            Assert.That(StringUtils.ParseString<long>(str), Is.EqualTo(expected));
        }

        [Test]
        public void ParseParseString_Float_returns_correct_value()
        {
            var str = "1.0E-02";
            var expected = 0.01f;

            Assert.That(StringUtils.ParseString<float>(str), Is.EqualTo(expected));
        }

        [Test]
        public void ParseParseString_Double_returns_correct_value()
        {
            var str = "1.0E-02";
            var expected = 0.01;

            Assert.That(StringUtils.ParseString<double>(str), Is.EqualTo(expected));
        }
        
        [Test]
        public void ParseParseString_Bool_returns_correct_value()
        {
            Assert.That(StringUtils.ParseString<bool>("true"), Is.True);
            Assert.That(StringUtils.ParseString<bool>("True"), Is.True);
            Assert.That(StringUtils.ParseString<bool>("TRUE"), Is.True);
            Assert.That(StringUtils.ParseString<bool>("false"), Is.False);
            Assert.That(StringUtils.ParseString<bool>("False"), Is.False);
            Assert.That(StringUtils.ParseString<bool>("FALSE"), Is.False);
        }

        #region Repeat

        [Test]
        public void Repeat_empty_string_yield_empty_string()
        {
            Assert.That("".Repeat(123), Is.EqualTo(""));
        }

        [Test]
        public void Repeat_zero_times_yield_empty_string()
        {
            Assert.That("moo".Repeat(0), Is.EqualTo(""));
        }

        [Test]
        public void Repeat_one_time_yields_similar_string()
        {
            Assert.That("moo".Repeat(1), Is.EqualTo("moo"));
        }

        [Test]
        public void Repeat_multiple_times_produces_repeated_string()
        {
            Assert.That("moo".Repeat(4), Is.EqualTo("moomoomoomoo"));
        }

        #endregion

        [Test]
        public void SimpleWordWrap_empty_string_yields_empty_line()
        {
            AssertSimpleWordWrap("", 5, "");
        }

        [Test]
        public void SimpleWordWrap_short_string_yields_itself()
        {
            AssertSimpleWordWrap("ab c", 5, "ab c");
        }

        [Test]
        public void SimpleWordWrap_wrap_spaces()
        {
            AssertSimpleWordWrap("abc xy z", 3, "abc", "xy ", "z");
            AssertSimpleWordWrap("abc xy z", 4, "abc", "xy z");
            AssertSimpleWordWrap("abc xy z", 5, "abc", "xy z");
            AssertSimpleWordWrap("abc xy z", 6, "abc xy", "z");
            AssertSimpleWordWrap("abc xy z", 7, "abc xy ", "z");
            AssertSimpleWordWrap("abc xy z", 8, "abc xy z");
        }

        [Test]
        public void SimpleWordWrap_wrap_longword()
        {
            AssertSimpleWordWrap("abcxyz fx", 2, "ab", "cx", "yz", "fx");
            AssertSimpleWordWrap("abcxyz fx", 3, "abc", "xyz", "fx");
            AssertSimpleWordWrap("abcxyz fx", 4, "abcx", "yz f", "x");
            AssertSimpleWordWrap("abcxyz fx", 5, "abcxy", "z fx");
        }

        private void AssertSimpleWordWrap(string text, int maxLineWidth, params string[] expectedLines)
        {
            var lines = text.SimpleWordWrap(maxLineWidth).ToArray();

            Assert.AreEqual(expectedLines, lines);
        }
    }
}
