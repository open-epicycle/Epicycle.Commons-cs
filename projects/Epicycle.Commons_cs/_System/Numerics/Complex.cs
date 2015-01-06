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

namespace System.Numerics
{
    public struct Complex
    {
        private double _real;
        private double _imaginary;

        public Complex(double real, double imaginary)
        {
            _real = real;
            _imaginary = imaginary;
        }

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

        public static Complex FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
        }

        public static Complex Pow(Complex value, double power)
        {
            return Complex.FromPolarCoordinates(Math.Pow(value.Magnitude, power), value.Phase * power);
        }

        public static Complex operator *(Complex left, Complex right)
        {
            var real = left.Real * right.Real - left.Imaginary * right.Imaginary;
            var imaginary = left.Real * right.Imaginary + left.Imaginary * right.Real;

            return new Complex(real, imaginary);
        }
    }
}
