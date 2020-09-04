using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace RedisSample
{
    public abstract class BaseSample : IDisposable
    {
        private readonly ConnectionMultiplexer _connection;
        protected readonly IDatabase _database;
        private bool disposedValue;

        protected BaseSample(string configuration, int db)
        {
            _connection = ConnectionMultiplexer.Connect(configuration);
            _database = _connection.GetDatabase(db);
        }

        public bool KeyDelete(string key)
        {
            return _database.KeyDelete(key);
        }

        public bool KeyExists(string key)
        {
            return _database.KeyExists(key);
        }

        public byte[] KeyDump(string key)
        {
            return _database.KeyDump(key);
        }

        public bool KeyExpire(string key, TimeSpan? expiry)
        {
            return _database.KeyExpire(key, expiry);
        }

        public bool KeyExpire(string key, DateTime? expiry)
        {
            return _database.KeyExpire(key, expiry);
        }

        public TimeSpan? KeyIdleTime(string key)
        {
            return _database.KeyIdleTime(key);
        }

        public TimeSpan? KeyTimeToLive(string key)
        {
            return _database.KeyTimeToLive(key);
        }

        public bool KeyTouch(string key)
        {
            return _database.KeyTouch(key);
        }

        public RedisType KeyType(string key)
        {
            return _database.KeyType(key);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    _connection.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~BaseSample()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
