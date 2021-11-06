using System;
using System.Collections.Generic;
using System.Linq;

namespace Northvolt.Models
{
    public static class KeyValuePairModel
    {
        private static readonly Dictionary<string, (double value, DateTime timestamp)> _backingStore = 
            new Dictionary<string, (double value, DateTime timestamp)>();

        public static IEnumerable<KeyValuePair<string, double>> GetList() => 
            _backingStore.Select(kvp => new KeyValuePair<string, double>(kvp.Key, kvp.Value.value));

        public static string Add(string key, double value)
        {
            string response = "Key must be provided";
            if (!string.IsNullOrEmpty(key))
            {
                var timestamp = DateTime.Now;
                _backingStore[key] = (value, timestamp);
                response = $"Added ({key}, {value}) at {timestamp}";
            }
            return response;
        }
    }
}
