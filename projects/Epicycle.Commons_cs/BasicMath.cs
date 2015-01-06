// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
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

namespace Epicycle.Commons
{
    public static class BasicMath
    {
        public const double Sqrt2 = 1.41421356237309504880168872420969807856967187537694807317667973799;

        public const double Epsilon = 1e-100;

        public static double RadToDeg(double angle)
        {
            return (angle / Math.PI) * 180.0;
        }

        public static double DegToRad(double angle)
        {
            return (angle / 180.0) * Math.PI;
        }

        public static int Round(float x)
        {
            return (int)Math.Round(x);
        }

        public static int Round(double x)
        {
            return (int)Math.Round(x);
        }

        public static byte RoundToByte(double x)
        {
            return (byte)Clip(Round(x), 0, 255);
        }

        public static int Floor(float x)
        {
            return (int)Math.Floor(x);
        }

        public static int Floor(double x)
        {
            return (int)Math.Floor(x);
        }

        public static int Ceiling(float x)
        {
            return (int)Math.Ceiling(x);
        }

        public static int Ceiling(double x)
        {
            return (int)Math.Ceiling(x);
        }

        public static int Clip(int x, int min, int max)
        {
            return Math.Max(min, Math.Min(x, max));
        }

        public static float Clip(float x, float min, float max)
        {
            return Math.Max(min, Math.Min(x, max));
        }

        public static double Clip(double x, double min, double max)
        {
            return Math.Max(min, Math.Min(x, max));
        }

        public static int RoundToDecimalPrecision(double x, int precision)
        {
            if(precision <= 0)
            {
                return Round(x);
            }

            var f = Round(Math.Pow(10, precision));

            return (Round(x) / f) * f;
        }

        public static float Interpolate(float x, float min, float max)
        {
            return min + x * (max - min);
        }

        public static double Interpolate(double x, double min, double max)
        {
            return min + x * (max - min);
        }

        public static double Max(double a, double b, double c)
        {
            return Math.Max(a, Math.Max(b, c));
        }

        public static double Min(double a, double b, double c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        public static int Sqr(int x)
        {
            return x * x;
        }

        public static float Sqr(float x)
        {
            return x * x;
        }

        public static double Sqr(double x)
        {
            return x * x;
        }

        public static int Cube(int x)
        {
            return x * x * x;
        }

        public static float Cube(float x)
        {
            return x * x * x;
        }

        public static double Cube(double x)
        {
            return x * x * x;
        }

        public static double Sqrt(double x)
        {
            var answer = Math.Sqrt(x);

            if (!double.IsNaN(answer))
            {
                return answer;
            }

            return 0;
        }

        // differs from System.Math.Sign since it never returns 0
        public static int Sign(double x)
        {
            return x >= 0 ? 1 : -1;
        }

        public static int Mod(this int x, int y)
        {
            var rem = x % y;

            return (rem < 0) ? (rem + y) : rem;
        }

        // guarantees to return a value between 0 and PI (not NaN)
        public static double Acos(double c)
        {
            var answer = Math.Acos(c);

            if (!double.IsNaN(answer))
            {
                return answer;
            }

            if (c > 0)
            {
                return 0;
            }
            else
            {
                return Math.PI;
            }
        }

        // guarantees to return a value between -PI/2 and +PI/2 (not NaN)
        public static double Asin(double c)
        {
            var answer = Math.Asin(c);

            if (!double.IsNaN(answer))
            {
                return answer;
            }

            if (c > 0)
            {
                return Math.PI / 2;
            }
            else
            {
                return -Math.PI / 2;
            }
        }
    }
}
