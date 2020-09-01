using System;
using System.Text;

using ServiceStack.IO;
using ServiceStack.Redis;

namespace RedisQueueLib
{
    public class RedisMQClient : IDisposable
    {
        private readonly RedisClient _redisClient;

        public RedisMQClient(string host, int port, string password = null, long db = 0)
            : this(new RedisEndpoint(host, port, password, db))
        {
        }

        public RedisMQClient(RedisEndpoint endpoint)
        {
            _redisClient = new RedisClient(endpoint);
        }

        #region 左进右出
        /// <summary>
        /// 将消息发送到队列中
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public long SendMessage(string queueName, string message)
        {
            // 编码消息
            byte[] data = Encoding.UTF8.GetBytes(message);
            // 将消息入队
            long count = _redisClient.LPush(queueName, data);
            return count;
        }

        /// <summary>
        /// 同步非阻塞获取消息(拉)
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <returns>消息</returns>
        public string GetMessage(string queueName)
        {
            // 将消息出队
            byte[] data = _redisClient.RPop(queueName);
            // 解码消息
            string message = data == null ? null : Encoding.UTF8.GetString(data);
            return message;
        }
        #endregion

        /// <summary>
        /// 同步阻塞获取消息(推)
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="timeout">阻塞超时时间</param>
        /// <returns>消息</returns>
        public string GetMessageBlock(string queueName, TimeSpan? timeout = null)
        {
            // 阻塞等待获取消息
            string message = _redisClient.BlockingPopItemFromList(queueName, timeout);
            return message;
        }

        public long GetMessageCount(string queueName)
        {
            return _redisClient.GetListCount(queueName);
        }

        public void Dispose()
        {
            _redisClient.Dispose();
        }
    }
}
