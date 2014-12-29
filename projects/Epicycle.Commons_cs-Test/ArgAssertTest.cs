using NUnit.Framework;
using System;

namespace Epicycle.Commons
{
    [TestFixture]
    class ArgAssertTest
    {
        [Test]
        public void NotNull_not_null_does_nothing()
        {
            ArgAssert.NotNull("foo", "someArg");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_null_throws_NullReferenceException()
        {
            ArgAssert.NotNull(null, "someArg");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NoNullIn_null_argument_throws_NullReferenceException()
        {
            string[] arg = null;
            ArgAssert.NoNullIn(arg, "someArg");
        }

        [Test]
        public void NoNullIn_empty_array_does_nothing()
        {
            ArgAssert.NoNullIn(new string[0], "someArg");
        }

        [Test]
        public void NoNullIn_array_without_nulls_does_nothing()
        {
            ArgAssert.NoNullIn(new string[] { "aa", "bb" }, "someArg");
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void NoNullIn_array_with_nulls_throws_NullReferenceException()
        {
            ArgAssert.NoNullIn(new string[] { "aa", null, "bb" }, "someArg");
        }
    }
}
