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

using System.Collections.Generic;

#if NET35 || NET40

namespace System.Collections.ObjectModel
{
    public class ReadOnlyCollection<T> : 
        IList<T>,
        ICollection<T>,
        IList,
        ICollection,
        IReadOnlyList<T>,
        IReadOnlyCollection<T>,
        IEnumerable<T>,
        IEnumerable
    {
        private readonly IList<T> _list;

        #region Constructors

        public ReadOnlyCollection(IList<T> list)
        {
            _list = list;
        }

        #endregion

        #region Properties

        public int Count
        {
            get { return _list.Count; }
        }

        public T this[int index]
        {
            get { return _list[index]; }
        }

        protected IList<T> Items
        {
            get { return _list; }
        }

        #endregion

        #region Methods

        public bool Contains(T value)
        {
            return _list.Contains(value);
        }

        public void CopyTo(T[] array, int index)
        {
            _list.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T value)
        {
            return _list.IndexOf(value);
        }

        #endregion

        #region Explicit Interface Implementations

        void ICollection<T>.Add(T value)
        {
            ((ICollection<T>) _list).Add(value);
        }

        void ICollection<T>.Clear()
        {
            ((ICollection<T>)_list).Clear();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_list).CopyTo(array, index);
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return ((ICollection<T>)_list).IsReadOnly; }
        }

        bool ICollection.IsSynchronized
        {
            get { return ((ICollection)_list).IsSynchronized; }
        }

        bool ICollection<T>.Remove(T value)
        {
            return ((ICollection<T>)_list).Remove(value);
        }

        Object ICollection.SyncRoot
        {
            get { return ((ICollection)_list).SyncRoot; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }

        int IList.Add(Object value)
        {
            return ((IList)_list).Add(value);
        }

        void IList.Clear()
        {
            ((IList)_list).Clear();
        }

        bool IList.Contains(Object value)
        {
            return ((IList)_list).Contains(value);
        }

        int IList.IndexOf(Object value)
        {
            return ((IList)_list).IndexOf(value);
        }

        void IList<T>.Insert(int index, T value)
        {
            ((IList<T>)_list).Insert(index, value);
        }

        void IList.Insert(int index, Object value)
        {
            ((IList)_list).Insert(index, value);
        }

        bool IList.IsFixedSize
        {
            get { return ((IList)_list).IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { return ((IList)_list).IsReadOnly; }
        }

        T IList<T>.this[int index]
        {
            get { return ((IList<T>)_list)[index]; }
            set { ((IList<T>)_list)[index] = value; }
        }

        Object IList.this[int index]
        {
            get { return ((IList)_list)[index]; }
            set { ((IList)_list)[index] = value; }
        }

        void IList.Remove(Object value)
        {
            ((IList)_list).Remove(value);
        }

        void IList<T>.RemoveAt(int index)
        {
            ((IList<T>)_list).RemoveAt(index);
        }

        void IList.RemoveAt(int index)
        {
            ((IList)_list).RemoveAt(index);
        }

        #endregion
    }
}

#endif

