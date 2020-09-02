using System;
using System.Threading.Tasks;

using RedisPublishSubscribeLib;

namespace RedisPublishSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (RedisPubSubClient client = new RedisPubSubClient("192.168.199.117:6379,password=password"))
            {
                string topic = "DemoTopic";
                while (true)
                {
                    string message = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fffff} - {Guid.NewGuid()}";
                    Console.WriteLine(message);
                    await client.Publish(topic, message);
                    await Task.Delay(100);
                }
            }
        }
    }
}
