using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace Reto_6
{
    [DebuggerDisplay("Count = {Count}, ActiveCount = {ActiveCount}")]
    public class Cache//: IDictionary<object, object>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Dictionary<object, WeakReference<object>> dic = new Dictionary<object, WeakReference<object>>();

        public object this[object key]
        {
            get
            {
                object item = null;
                dic[key].TryGetTarget(out item);
                return item;
            }

            set
            {
                dic[key] = new WeakReference<object>(value);
            }
        }

        public void Add(object key, object value)
        {
            this[key] = value;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Count
        {
            get
            {
                return dic.Count;
            }
        }


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int ActiveCount
        {
            get
            {
                var count = 0;

                foreach (var key in Keys)
                {
                    if (this[key] != null)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public T get<T>(object key)
        {
            return (T)this[key];
        }


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ICollection<object> Keys
        {
            get { return dic.Keys; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public IEnumerable<KeyValueForCache> KeyValuePairs
        {
            get
            {
                List<KeyValueForCache> result = new List<KeyValueForCache>(Count);
                foreach (var key in Keys)
                {
                    result.Add(new KeyValueForCache(key, this[key]));
                }

                return result;
            }
        }

        [DebuggerDisplay("Key = {Key}, Value = {Value}")]
        public class KeyValueForCache
        {
            public object Key { get; set; }

            public object Value { get; set; }

            public KeyValueForCache()
            {

            }

            public KeyValueForCache(object key, object value)
            {
                this.Key = key;
                this.Value = value;
            }

        }


        //public bool ContainsKey(object key)
        //{
        //    return dic.ContainsKey(key);
        //}

        //public bool Remove(object key)
        //{
        //    return dic.Remove(key);
        //}

        //public bool TryGetValue(object key, out object value)
        //{
        //    try
        //    {
        //        value = this[key];
        //        return true;
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        value = null;
        //        return false;
        //    }
        //}

        //public ICollection<object> Values
        //{
        //    get
        //    {
        //        return dic.Values.Select(wr =>
        //        {
        //            object value = null; 
        //            wr.TryGetTarget(out value); 
        //            return Values;
        //        }).ToArray();
        //    }
        //}

        //public void Add(KeyValuePair<object, object> item)
        //{
        //    this.Add(item.Key, item.Value);
        //}

        //public void Clear()
        //{
        //    dic.Clear();
        //}

        //public bool Contains(KeyValuePair<object, object> item)
        //{
        //    return dic.ContainsKey(new KeyValuePair<object, WeakReference<object>>(item.Key, new WeakReference<object>(item.Value)));
        //}

        //public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool IsReadOnly
        //{
        //    get { return false; }
        //}

        //public bool Remove(KeyValuePair<object, object> item)
        //{
        //    return this.Remove(item.Key);
        //}

        //public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        //{
        //    return new CacheEnumerator(this);
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return new CacheEnumerator(this);
        //}

        //public class CacheEnumerator : IEnumerator<KeyValuePair<object, object>>
        //{
        //    private int currentKey = 0;
        //    private Cache toEnumerate;
        //    private object[] keys;

        //    internal CacheEnumerator(Cache toEnumerate)
        //    {
        //        this.toEnumerate = toEnumerate;
        //        keys = toEnumerate.Keys.ToArray();
        //    }


        //    public KeyValuePair<object, object> Current
        //    {
        //        get { return new KeyValuePair<object, object>(keys[currentKey], toEnumerate[keys[currentKey]]); }
        //    }

        //    public void Dispose()
        //    {
        //    }

        //    object System.Collections.IEnumerator.Current
        //    {
        //        get { return toEnumerate[keys[currentKey]]; }
        //    }

        //    public bool MoveNext()
        //    {
        //        if (++currentKey >= keys.Length)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }

        //    public void Reset()
        //    {
        //        currentKey = 0;
        //    }
        //}
    }
}
