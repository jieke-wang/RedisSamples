using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample
{
    public class StringSample : BaseSample
    {
        public StringSample(string configuration, int db) 
            : base(configuration, db)
        {
        }

        public void Set(string key, string value, TimeSpan? expiry)
        {
            _database.StringSet(key, value, expiry);
        }

        public void Append(string key, string value)
        {
            _database.StringAppend(key, value);
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }
    }
}
