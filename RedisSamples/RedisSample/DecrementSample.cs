using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample
{
    public class DecrementSample : BaseSample
    {
        public DecrementSample(string configuration, int db) 
            : base(configuration, db)
        {
        }

        public long HashDecrement(string key, string value, long valueStep)
        {
            return _database.HashDecrement(key, value, valueStep);
        }

        public double HashDecrement(string key, string value, double valueStep)
        {
            return _database.HashDecrement(key, value, valueStep);
        }

        public double SortedSetDecrement(string key, string member, double valueStep)
        {
            return _database.SortedSetDecrement(key, member, valueStep);
        }

        public long StringDecrement(string key, long valueStep)
        {
            return _database.StringDecrement(key, valueStep);
        }

        public double StringDecrement(string key, double valueStep)
        {
            return _database.StringDecrement(key, valueStep);
        }
    }
}
