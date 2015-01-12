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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Epicycle.Commons.TestUtils;

namespace System.Numerics
{
    [TestFixture]
    public class ComplexTest
    {
        private Complex _c1;
        private Complex _c2;

        [SetUp]
        public void SetUp()
        {
            _c1 = new Complex(1, -2);
            _c2 = new Complex(-3, 4.5);
        }

        #region Basic

        [Test]
        public void constructor_sets_properties_right()
        {
            var c = new Complex(1, 2);

            Assert.AreEqual(1, c.Real);
            Assert.AreEqual(2, c.Imaginary);
        }

        [Test]
        public void Zero_correct_properties()
        {
            Assert.AreEqual(0, Complex.Zero.Real);
            Assert.AreEqual(0, Complex.Zero.Imaginary);
        }

        [Test]
        public void One_correct_properties()
        {
            Assert.AreEqual(1, Complex.One.Real);
            Assert.AreEqual(0, Complex.One.Imaginary);
        }

        [Test]
        public void ImaginaryOne_correct_properties()
        {
            Assert.AreEqual(0, Complex.ImaginaryOne.Real);
            Assert.AreEqual(1, Complex.ImaginaryOne.Imaginary);
        }

        #endregion

        #region Properties

        [Test]
        public void Magnitude_returns_correct_result()
        {
            NumericTestUtils.AssertAreEqual(Math.Sqrt(5), _c1.Magnitude);
        }

        [Test]
        public void Phase_returns_correct_result()
        {
            NumericTestUtils.AssertAreEqual(-1.1071487177, _c1.Phase);
        }

        #endregion

        #region Methods

        #region Equals & GetHashcode

        [Test]
        public void Equals_equal_objects_return_true()
        {
            Assert.True(_c1.Equals((object)new Complex(1, -2)));
        }

        [Test]
        public void Equals_equal_complex_return_true()
        {
            Assert.True(_c1.Equals(new Complex(1, -2)));
        }

        [Test]
        public void Equals_defferent_object_type_return_false()
        {
            Assert.False(_c1.Equals("123"));
        }

        [Test]
        public void Equals_not_equal_object_return_false()
        {
            Assert.False(_c1.Equals((object)new Complex(1, 3)));
        }

        [Test]
        public void Equals_not_equal_complex_return_false()
        {
            Assert.False(_c1.Equals(new Complex(1, 3)));
        }

        [Test]
        public void GetHashCode_same_objects_return_same_code()
        {
            Assert.AreEqual(new Complex(1, -2).GetHashCode(), _c1.GetHashCode());
        }

        #endregion

        #region ToString

        [Test]
        public void ToString_returns_correct_string()
        {
            Assert.AreEqual("(1, -3.5)", new Complex(1, -3.5).ToString());
        }

        #endregion

        #endregion
    
        #region Operators

        [Test]
        public void CastOperators_work_correctly()
        {
            var expected = new Complex(123, 0);

            ComplexTestUtils.AssertAreEqual(expected, (Complex)((decimal)123));
            ComplexTestUtils.AssertAreEqual(expected, (byte)123);
            ComplexTestUtils.AssertAreEqual(expected, (double)123);
            ComplexTestUtils.AssertAreEqual(expected, (short)123);
            ComplexTestUtils.AssertAreEqual(expected, (int)123);
            ComplexTestUtils.AssertAreEqual(expected, (long)123);
            ComplexTestUtils.AssertAreEqual(expected, (sbyte)123);
            ComplexTestUtils.AssertAreEqual(expected, (float)123);
            ComplexTestUtils.AssertAreEqual(expected, (ushort)123);
            ComplexTestUtils.AssertAreEqual(expected, (uint)123);
            ComplexTestUtils.AssertAreEqual(expected, (ulong)123);
        }

        [Test]
        public void Negation_return_correct_result()
        {
            var expected = new Complex(-1, 2);

            ComplexTestUtils.AssertAreEqual(expected, -_c1);
            ComplexTestUtils.AssertAreEqual(expected, Complex.Negate(_c1));
        }

        [Test]
        public void Addition_return_correct_result()
        {
            var expected = new Complex(-2, 2.5);

            ComplexTestUtils.AssertAreEqual(expected, _c1 + _c2);
            ComplexTestUtils.AssertAreEqual(expected, Complex.Add(_c1, _c2));
        }

        [Test]
        public void Subtraction_return_correct_result()
        {
            var expected = new Complex(4, -6.5);

            ComplexTestUtils.AssertAreEqual(expected, _c1 - _c2);
            ComplexTestUtils.AssertAreEqual(expected, Complex.Subtract(_c1, _c2));
        }

        [Test]
        public void Multiplication_return_correct_result()
        {
            var expected = new Complex(6, 10.5);

            ComplexTestUtils.AssertAreEqual(expected, _c1 * _c2);
            ComplexTestUtils.AssertAreEqual(expected, Complex.Multiply(_c1, _c2));
        }

        [Test]
        public void Division_return_correct_result()
        {
            var expected = new Complex(-0.410256, 0.051282);

            ComplexTestUtils.AssertAreEqual(expected, _c1 / _c2);
            ComplexTestUtils.AssertAreEqual(expected, Complex.Divide(_c1, _c2));
        }

        [Test]
        public void EqualsOperator_equal_objects_return_true()
        {
            var c1 = new Complex(12, -34);
            var c2 = new Complex(12, -34);

            Assert.True(c1 == c2);
        }

        [Test]
        public void EqualsOperator_not_equal_objects_return_false()
        {
            var c1 = new Complex(12, -34);
            var c2 = new Complex(-12, -34);

            Assert.False(c1 == c2);
        }

        [Test]
        public void NotEqualsOperator_not_equal_objects_return_true()
        {
            var c1 = new Complex(12, -34);
            var c2 = new Complex(-12, -34);

            Assert.True(c1 != c2);
        }

        [Test]
        public void NotEqualsOperator_equal_objects_return_true()
        {
            var c1 = new Complex(12, -34);
            var c2 = new Complex(12, -34);

            Assert.False(c1 != c2);
        }

        #endregion

        #region Static functions

        [Test]
        public void Abs_returns_correct_result()
        {
            NumericTestUtils.AssertAreEqual(Math.Sqrt(5), Complex.Abs(_c1));
        }

        [Test]
        public void Acos_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(1.143717, 1.528570, Complex.Acos(_c1));
        }

        [Test]
        public void Asin_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.427078, -1.528570, Complex.Asin(_c1));
        }

        [Test]
        public void Atan_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(1.338972, -0.402359, Complex.Atan(_c1));
        }

        [Test]
        public void Conjugate_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(1, 2, Complex.Conjugate(_c1));
        }

        [Test]
        public void Cos_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(2.032723, 3.051897, Complex.Cos(_c1));
        }

        [Test]
        public void Cosh_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(-0.642148, -1.068607, Complex.Cosh(_c1));
        }

        [Test]
        public void Exp_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(-1.131204, -2.471726, Complex.Exp(_c1));
        }

        [Test]
        public void FromPolarCoordinates_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(_c1, Complex.FromPolarCoordinates(2.23607, -1.10715));
        }

        [Test]
        public void Log_natural_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.804718, -1.107148, Complex.Log(_c1));
        }

        [Test]
        public void Log_custom_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.313736, -0.431645, Complex.Log(_c1, 13));
        }

        [Test]
        public void Log10_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.349485, -0.480282, Complex.Log10(_c1));
        }

        [Test]
        public void Pow_scalar_power_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(-0.026858, 0.297861, Complex.Pow(_c1, -1.5));
        }

        [Test]
        public void Pow_complex_power_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(10.3054309, 7.989778, Complex.Pow(_c1, _c2));
        }

        [Test]
        public void Reciprocal_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.2, 0.4, Complex.Reciprocal(_c1));
        }

        [Test]
        public void Sin_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(3.165778, -1.959601, Complex.Sin(_c1));
        }

        [Test]
        public void Sinh_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(-0.489056, -1.403119, Complex.Sinh(_c1));
        }

        [Test]
        public void Sqrt_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(1.272019, -0.786151, Complex.Sqrt(_c1));
        }

        [Test]
        public void Tan_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(0.033812, -1.014793, Complex.Tan(_c1));
        }

        [Test]
        public void Tanh_returns_correct_result()
        {
            ComplexTestUtils.AssertAreEqual(1.166736, 0.243458, Complex.Tanh(_c1));
        }

        #endregion
    }
}
