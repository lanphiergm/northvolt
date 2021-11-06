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

        public static string Prune()
        {
            int count = 0;
            // Sorting the keys and taking only the last 5 would be O(n log n) complexity, but
            // building up a list of the 5 most-recent kvps is O(n) complexity and so should be faster
            var mostRecent = new List<DateTime>();
            foreach (var kvp in _backingStore)
            {
                if (mostRecent.Count < 5)
                {
                    mostRecent.Add(kvp.Value.timestamp);
                }
                else if (mostRecent.Min() < kvp.Value.timestamp)
                {
                    mostRecent.Remove(mostRecent.Min());
                    mostRecent.Add(kvp.Value.timestamp);
                }
            }
            for (int i = 0; i < _backingStore.Count; )
            {
                string key = _backingStore.Keys.ElementAt(i);
                var timestamp = _backingStore[key].timestamp;
                if (mostRecent.Contains(timestamp))
                {
                    //This timestamp was found so we remove it from the list so another message
                    //with an identical timestamp won't also get skipped
                    mostRecent.Remove(timestamp);
                    i++;
                }
                else
                {
                    //The timestamp isn't in the most recent list so remove it
                    _backingStore.Remove(key);
                    count++;
                }
            }

            return $"Pruned {count} messages";
        }
    }
}
