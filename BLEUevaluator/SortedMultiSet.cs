using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEUevaluator
{
    public class MultiSet<T>
    {
        private Dictionary<T, int> _dict;

        public MultiSet()
        {
            _dict = new Dictionary<T, int>();
        }
        
        public MultiSet(IEnumerable<T> items) : this()
        {
            Add(items);
        }

        public int this[T key]
        {
            get
            {
                return _dict[key];
            } 
            set
            {
                if (_dict.ContainsKey(key))
                    _dict[key] = value;
                else
                    _dict.Add(key, 1);
            }
        }

        public bool Contains(T item)
        {
            return _dict.ContainsKey(item);
        }

        public void Add(T item)
        {
            if (_dict.ContainsKey(item))
                _dict[item]++;
            else
                _dict[item] = 1;
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public bool Remove(T item)
        {
            if (!_dict.ContainsKey(item))
                return false;
            if (--_dict[item] == 0)
            {
                _dict.Remove(item);
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var kvp in _dict)
                for (int i = 0; i < kvp.Value; i++)
                    yield return kvp.Key;
        }
    }
}
