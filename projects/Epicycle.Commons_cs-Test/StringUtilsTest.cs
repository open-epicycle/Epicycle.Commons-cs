using NUnit.Framework;
using System.Linq;

namespace Epicycle.Commons
{
    [TestFixture]
    public class StringUtilsTest
    {
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
