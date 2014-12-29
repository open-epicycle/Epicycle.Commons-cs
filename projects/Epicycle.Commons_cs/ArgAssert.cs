using System;
using System.Collections.Generic;
using System.Linq;

// Authors: untrots

namespace Epicycle.Commons
{
    /// <summary>
    /// This class contains utilities for validating arguments of methods.
    /// </summary>
    public static class ArgAssert
    {
        /// <summary>
        /// Validates that the condition is true
        /// </summary>
        /// <param name="condition">The condition to validate</param>
        /// <param name="name">The name of the argument (used in the exception message)</param>
        /// <param name="conditionText">The condition explanation (used in the exception message)</param>
        /// <exception cref="ArgumentException">Thrown when the condition is not met</exception>
        public static void That(bool condition, string name, string conditionText)
        {
            if (!condition)
            {
                var errorMessage = string.Format("The argument '{0}' must {1}", name, conditionText);

                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Validates that the argument is not null.
        /// </summary>
        /// <param name="value">The argument to validate</param>
        /// <param name="name">The name of the argument (used in the exception message)</param>
        /// <exception cref="NullReferenceException">When the argument is null</exception>
        public static void NotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name, "The argument '" + name + "' must not be null!");
            }
        }

        /// <summary>
        /// Validates that the enumerable is not null and does not contain nulls.
        /// </summary>
        /// <param name="enumerable">The enumerable to validate</param>
        /// <param name="name">The name of the argument (used in the exception message)</param>
        /// <exception cref="NullReferenceException">When the enumerable is null or contains a null</exception>
        public static void NoNullIn<T>(IEnumerable<T> enumerable, string name)
        {
            NotNull(enumerable, name);

            if (enumerable.Any(x => x == null))
            {
                throw new NullReferenceException("The argument '" + name + "' must not contain a null!");
            }
        }

        public static void Equal(object value, string name, object reference)
        {
            if (!value.Equals(reference))
            {
                var errMsg = string.Format("{0} must be equal to {1} but was {2}", name, reference, value);

                throw new ArgumentException(errMsg, name);
            }
        }

        public static void Equal(object value1, string name1, object value2, string name2)
        {
            if (!value1.Equals(value2))
            {
                var errMsg = string.Format("Expected {0} to be equal to {1} whereas actual values are {2} and {3} respectively", name1, name2, value1, value2);

                throw new ArgumentException(errMsg);
            }
        }        

        public static void InRange<T>(T value, string name, T minValue, T maxValue)
            where T : IComparable<T>
        {
            if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("{0} must be between {1} and {2} but was {3}", name, minValue, maxValue, value));
            }
        }

        public static void GreaterThan<T>(T value, string name, T reference)
            where T : IComparable<T>
        {
            if (value.CompareTo(reference) <= 0)
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("{0} must be greater than {1} but was {2}", name, reference, value));
            }
        }

        public static void LessThan<T>(T value, string name, T reference)
            where T : IComparable<T>
        {
            if (value.CompareTo(reference) >= 0)
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("{0} must be less than {1} but was {2}", name, reference, value));
            }
        }

        public static void AtLeast<T>(T value, string name, T reference)
            where T : IComparable<T>
        {
            if (value.CompareTo(reference) < 0)
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("{0} must be at least {1} but was {2}", name, reference, value));
            }
        }

        public static void AtMost<T>(T value, string name, T reference)
            where T : IComparable<T>
        {
            if (value.CompareTo(reference) > 0)
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("{0} must be at most {1} but was {2}", name, reference, value));
            }
        }

        public static void GreaterThan<T>(T value1, string name1, T value2, string name2)
            where T : IComparable<T>
        {
            if (value1.CompareTo(value2) <= 0)
            {
                var errMsg = string.Format("Expected {0} to be greater than {1} whereas actual values are {2} and {3} respectively", name1, name2, value1, value2);

                throw new ArgumentException(errMsg);
            }
        }

        public static void LessThan<T>(T value1, string name1, T value2, string name2)
            where T : IComparable<T>
        {
            if (value1.CompareTo(value2) >= 0)
            {
                var errMsg = string.Format("Expected {0} to be less than {1} whereas actual values are {2} and {3} respectively", name1, name2, value1, value2);

                throw new ArgumentException(errMsg);
            }
        }

        public static void AtLeast<T>(T value1, string name1, T value2, string name2)
            where T : IComparable<T>
        {
            if (value1.CompareTo(value2) < 0)
            {
                var errMsg = string.Format("Expected {0} to be at least {1} whereas actual values are {2} and {3} respectively", name1, name2, value1, value2);

                throw new ArgumentException(errMsg);
            }
        }

        public static void AtMost<T>(T value1, string name1, T value2, string name2)
            where T : IComparable<T>
        {
            if (value1.CompareTo(value2) > 0)
            {
                var errMsg = string.Format("Expected {0} to be at most {1} whereas actual values are {2} and {3} respectively", name1, name2, value1, value2);

                throw new ArgumentException(errMsg);
            }
        }

        public static void ValidEnum<T>(T value, string name)
        {
            Type type = typeof(T);

            if (!Enum.IsDefined(type, value))
            {
                throw new ArgumentOutOfRangeException(name, value, string.Format("Argument {0} has value {1} which is invalid for enum {2}", name, value, type));
            }
        }        
    }
}
