using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic;

using StackExchange.Redis;

namespace RedisSample
{
    public class HashSample : BaseSample
    {
        public HashSample(string configuration, int db) : base(configuration, db)
        {
        }

        public void HashSet(string key, IDictionary<string, object> values)
        {
            List<HashEntry> hashEntries = new List<HashEntry>(values.Count);
            foreach (var value in values)
            {
                RedisValue redisValue;
                if (value.Value is bool boolValue) redisValue = Convert.ToInt32(boolValue);
                else if (value.Value is DateTime dateTimeValue) redisValue = dateTimeValue.ToString("yyyyMMddHHMMssfffff");
                else redisValue = Convert.ToString(value.Value);
                hashEntries.Add(new HashEntry(value.Key, ""));
            }

            this.HashSet(key, hashEntries.ToArray());
        }

        public void HashSet(string key, object instanceValue)
        {
            var props = instanceValue.GetType().GetProperties();
            List<HashEntry> hashEntries = new List<HashEntry>(props.Length);
            foreach (var prop in props)
            {
                object propValue = prop.GetValue(instanceValue);
                RedisValue redisValue;
                if (propValue is bool boolValue) redisValue = Convert.ToInt32(boolValue);
                else if (propValue is DateTime dateTimeValue) redisValue = dateTimeValue.ToString("yyyyMMddHHmmssfffff");
                else redisValue = Convert.ToString(propValue);
                hashEntries.Add(new HashEntry(prop.Name, redisValue));
            }

            HashSet(key, hashEntries.ToArray());
        }

        public void HashSet(string key, params HashEntry[] hashEntries)
        {
            if (hashEntries.Length == 0) return;
            _database.HashSet(key, hashEntries);
        }

        public T HashGet<T>(string key)
        {
            var hashValues = _database.HashGetAll(key);
            if (hashValues == null || hashValues.Length == 0) return default;

            T instance = Activator.CreateInstance<T>();
            var propDics = instance.GetType().GetProperties().ToDictionary(key => key.Name, value => value, StringComparer.OrdinalIgnoreCase);
            for (int index = 0; index < hashValues.Length; index++)
            {
                var hashValue = hashValues[index];
                if (propDics.TryGetValue(hashValue.Name, out var prop) == false) continue;

                if(prop.PropertyType == typeof(DateTime))
                {
                    prop.SetValue(instance, DateTime.ParseExact(hashValue.Value, "yyyyMMddHHmmssfffff", null));
                }
                else
                {
                    prop.SetValue(instance, Convert.ChangeType(hashValue.Value, prop.PropertyType));
                }
            }

            return instance;
        }
    }
}
