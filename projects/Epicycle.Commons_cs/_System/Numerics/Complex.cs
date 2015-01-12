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

#if NET35

// TODO: Test ToString with formattings

namespace System.Numerics
{
    // compatible with .NET 4.5 (excluding BigInteger casting)
    public struct Complex : IEquatable<Complex>, IFormattable
    {
        private static readonly string FormatString = "({0}, {1})";

        #region Fields

        public static readonly Complex Zero = new Complex(0, 0);
        public static readonly Complex One = new Complex(1, 0);
        public static readonly Complex ImaginaryOne = new Complex(0, 1);

        #endregion

        #region Private members

        private double _real;
        private double _imaginary;

        #endregion

        #region C-tors

        public Complex(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

        #endregion

        #region Properties

        public double Real
        {
            get { return _real; }
        }

        public double Imaginary
        {
            get { return _imaginary; }
        }

        public double Magnitude
        {
            get { return Math.Sqrt(_real * _real + _imaginary * _imaginary); }
        }

        public double Phase
        {
            get { return Math.Atan2(_imaginary, _real); }
        }

        #endregion

        #region Methods

        public bool Equals(Complex value)
        {
            return (Real == value.Real) && (Imaginary == value.Imaginary);
        }

        public override bool Equals(Object obj)
        {
            if(!(obj is Complex))
            {
                return false;
            }

            return Equals((Complex)obj);
        }

        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(FormatString, Real, Imaginary);
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format(FormatString, Real.ToString(provider), Imaginary.ToString(provider));
        }

        public string ToString(string format)
        {
            return string.Format(FormatString, Real.ToString(format), Imaginary.ToString(format));
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format(FormatString, Real.ToString(format, provider), Imaginary.ToString(format, provider));
        }

        #endregion

        #region Operators
        
        // TODO: public static explicit operator Complex(BigInteger value)
        
        public static explicit operator Complex(decimal value)
        {
            return new Complex((double)value, 0);
        }

        public static implicit operator Complex(byte value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(double value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(short value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(int value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(long value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(sbyte value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(float value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(ushort value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(uint value)
        {
            return new Complex(value, 0);
        }

        public static implicit operator Complex(ulong value)
        {
            return new Complex(value, 0);
        }

        public static Complex operator -(Complex value)
        {
            return new Complex(-value.Real, -value.Imaginary);
        }

        public static Complex operator +(Complex left, Complex right)
        {
            return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }

        public static Complex operator -(Complex left, Complex right)
        {
            return new Complex(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }

        public static Complex operator *(Complex left, Complex right)
        {
            var real = left.Real * right.Real - left.Imaginary * right.Imaginary;
            var imaginary = left.Real * right.Imaginary + left.Imaginary * right.Real;

            return new Complex(real, imaginary);
        }

        public static Complex operator /(Complex left, Complex right)
        {
            var d = right.Real * right.Real + right.Imaginary * right.Imaginary;
            var real = (left.Real * right.Real + left.Imaginary * right.Imaginary) / d;
            var imaginary = (left.Imaginary * right.Real - left.Real * right.Imaginary) / d;

            return new Complex(real, imaginary);
        }

        public static bool operator ==(Complex left, Complex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Complex left, Complex right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Static functions

        public static double Abs(Complex value)
        {
            return value.Magnitude;
        }

        public static Complex Acos(Complex value)
        {
            return -ImaginaryOne * Log(value + ImaginaryOne * Sqrt(One - value * value));
        }

        public static Complex Add(Complex left, Complex right)
        {
            return left + right;
        }

        public static Complex Asin(Complex value)
        {
            return -ImaginaryOne * Log(ImaginaryOne * value + Sqrt(One - value * value));
        }

        public static Complex Atan(Complex value)
        {
            return (ImaginaryOne / new Complex(2.0, 0.0)) * (Log(One - ImaginaryOne * value) - Log(One + ImaginaryOne * value));
        }

        public static Complex Conjugate(Complex value)
        {
            return new Complex(value.Real, -value.Imaginary);
        }

        public static Complex Cos(Complex value)
        {
            var real = Math.Cos(value.Real) * Math.Cosh(value.Imaginary);
            var imaginary = -(Math.Sin(value.Real) * Math.Sinh(value.Imaginary));

            return new Complex(real, imaginary);
        }

        public static Complex Cosh(Complex value)
        {
            var real = Math.Cosh(value.Real) * Math.Cos(value.Imaginary);
            var imaginary = Math.Sinh(value.Real) * Math.Sin(value.Imaginary);

            return new Complex(real, imaginary);
        }

        public static Complex Divide(Complex dividend, Complex divisor)
        {
            return dividend / divisor;
        }

        public static Complex Exp(Complex value)
        {
            double r = Math.Exp(value.Real);

            return new Complex(Math.Cos(value.Imaginary) * r, Math.Sin(value.Imaginary) * r);
        }

        public static Complex FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
        }

        public static Complex Log(Complex value)
        {
            return new Complex(Math.Log(value.Magnitude), value.Phase);
        }

        public static Complex Log(Complex value, double baseValue)
        {
            return Log(value) / Math.Log(baseValue);
        }

        public static Complex Log10(Complex value)
        {
            return Log(value, 10);
        }

        public static Complex Multiply(Complex left, Complex right)
        {
            return left * right;
        }

        public static Complex Negate(Complex value)
        {
            return -value;
        }

        public static Complex Pow(Complex value, double power)
        {
            return Complex.FromPolarCoordinates(Math.Pow(value.Magnitude, power), value.Phase * power);
        }

        public static Complex Pow(Complex value, Complex power)
        {
            return Exp(power * Log(value));
        }

        public static Complex Reciprocal(Complex value)
        {
            var d = value.Real * value.Real + value.Imaginary * value.Imaginary;

            return new Complex(value.Real / d, -value.Imaginary / d);
        }

        public static Complex Sin(Complex value)
        {
            var real = Math.Sin(value.Real) * Math.Cosh(value.Imaginary);
            var imaginary = Math.Cos(value.Real) * Math.Sinh(value.Imaginary);

            return new Complex(real, imaginary);
        }

        public static Complex Sinh(Complex value)
        {
            var real = Math.Sinh(value.Real) * Math.Cos(value.Imaginary);
            var imaginary = Math.Cosh(value.Real) * Math.Sin(value.Imaginary);

            return new Complex(real, imaginary);
        }

        public static Complex Sqrt(Complex value)
        {
            return FromPolarCoordinates(Math.Sqrt(value.Magnitude), value.Phase / 2.0);
        }

        public static Complex Subtract(Complex left, Complex right)
        {
            return left - right;
        }

        public static Complex Tan(Complex value)
        {
            return Sin(value) / Cos(value);
        }

        public static Complex Tanh(Complex value)
        {
            return Sinh(value) / Cosh(value);
        }

        #endregion
    }
}

#endif
