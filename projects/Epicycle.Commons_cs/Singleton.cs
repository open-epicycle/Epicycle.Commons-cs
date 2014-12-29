using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons
{
    // especially useful for T = interface since interfaces have no static methods
    public static class Singleton<T>
        where T : new()
    {
        private static readonly T _instance = new T();

        public static T Instance
        {
            get { return _instance; }
        }
    }
}
