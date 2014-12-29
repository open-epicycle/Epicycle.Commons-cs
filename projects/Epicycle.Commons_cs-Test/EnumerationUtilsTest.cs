using NUnit.Framework;
using System.Linq;

namespace Epicycle.Commons.Collections
{
    [TestFixture]
    public class EnumerationUtilsTest
    {
        [Test]
        public void Concat_no_enumerables_produce_empty_enumerable()
        {
            Assert.That(EnumerationUtils.Concat(new int[][] { }).ToArray(), Is.EqualTo(new string[] { }));
        }

        [Test]
        public void Concat_one_enumerable_produces_it_data()
        {
            Assert.That(EnumerationUtils.Concat(new int[][] {new int[] { 1, 2, 3 }}).ToArray(), Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        [Test]
        public void Concat_multiple_enumerables_produce_merged_enumerable()
        {
            Assert.That(EnumerationUtils.Concat(new string[][] {
                new string[] { "a", "b", "c" }, 
                new string[] { "u", "v", "w" }, 
                new string[] { "x", "y", "z" }}).ToArray(), 
                Is.EqualTo(new string[] { "a", "b", "c", "u", "v", "w", "x", "y", "z" }));
        }
    }
}
