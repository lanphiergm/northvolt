using System;
using System.Collections.Generic;
using System.Linq;

namespace Northvolt.Models
{
    public static class KeyValuePairModel
    {
        private static readonly Dictionary<string, (double value, DateTime timestamp)> _backingStore = new();

        public static IEnumerable<KeyValuePair<string, double>> GetList() => 
            _backingStore.Select(kvp => new KeyValuePair<string, double>(kvp.Key, kvp.Value.value));

        public static void Add(string key, double value) => _backingStore.Add(key, (value, DateTime.Now));
    }
}
