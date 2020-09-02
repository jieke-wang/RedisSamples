using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace RedisDistributedLockSample
{
    public class RedisDistributedLock : IDisposable
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private bool disposedValue;

        public RedisDistributedLock(string configuration, int db = -1)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
            _database = _connectionMultiplexer.GetDatabase(db);
        }

        public async Task GetLock(string lockName, string appIdentity, TimeSpan timeout)
        {
            do
            {
                if (await _database.LockTakeAsync(lockName, $"{appIdentity}", timeout)) break;
            } while (true);
        }

        public async Task ReleaseLock(string lockName, string appIdentity)
        {
            await _database.LockReleaseAsync(lockName, $"{appIdentity}");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    _connectionMultiplexer.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~RedisDistributedLock()
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
