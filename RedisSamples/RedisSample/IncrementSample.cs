using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample
{
    public class IncrementSample : BaseSample
    {
        public IncrementSample(string configuration, int db) 
            : base(configuration, db)
        {
        }

        public long HashIncrement(string key, string value, long valueStep)
        {
            return _database.HashIncrement(key, value, valueStep);
        }

        public double HashIncrement(string key, string value, double valueStep)
        {
            return _database.HashIncrement(key, value, valueStep);
        }

        public double SortedSetIncrement(string key, string member, double valueStep)
        {
            return _database.SortedSetIncrement(key, member, valueStep);
        }

        public long StringIncrement(string key, long valueStep)
        {
            return _database.StringIncrement(key, valueStep);
        }

        public double StringIncrement(string key, double valueStep)
        {
            return _database.StringIncrement(key, valueStep);
        }
    }
}
