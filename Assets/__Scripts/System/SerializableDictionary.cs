using System;
using System.Collections.Generic;
using UnityEngine;

namespace __Scripts.System
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> _keys = new();
        [SerializeField] private List<TValue> _values = new();
    
        private Dictionary<TKey, TValue> _dictionary;
    
        public Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                if (_dictionary == null)
                    BuildDictionary();
                return _dictionary;
            }
        }
    
        private void BuildDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            int count = Mathf.Min(_keys.Count, _values.Count);
        
            for (int i = 0; i < count; i++)
            {
                if (!_dictionary.ContainsKey(_keys[i]))
                    _dictionary.Add(_keys[i], _values[i]);
            }
        }
    
        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
            _dictionary?.Clear();
        }
    }
}