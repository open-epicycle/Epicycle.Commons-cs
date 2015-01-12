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
using System.Numerics;

namespace Epicycle.Commons.TestUtils
{
    public static class ComplexTestUtils
    {
        public static bool AreEqual(Complex c1, Complex c2, double eplsilon = NumericTestUtils.Epsilon)
        {
            return NumericTestUtils.AreEqual(c1.Real, c2.Real) && NumericTestUtils.AreEqual(c1.Imaginary, c2.Imaginary);
        }

        public static void AssertAreEqual(Complex expected, Complex complex)
        {
            Assert.That(ComplexTestUtils.AreEqual(complex, expected));
        }

        public static void AssertAreEqual(double expectedReal, double expectedImaginary, Complex complex)
        {
            AssertAreEqual(new Complex(expectedReal, expectedImaginary), complex);
        }
    }
}
