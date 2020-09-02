using System;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace RedisPublishSubscribeLib
{
    public class RedisPubSubClient : IDisposable
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private bool disposedValue;

        public RedisPubSubClient(string configuration)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);
        }

        public Task<long> Publish(string topic, string message)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            return subscriber.PublishAsync(topic, message);
        }

        public Task Subscribe(string topic, Action<RedisChannel, RedisValue> handler)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            return subscriber.SubscribeAsync(topic, handler);
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
        // ~RedisPubSubClient()
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
