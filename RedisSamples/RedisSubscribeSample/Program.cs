using System;
using System.Threading;
using System.Threading.Tasks;

using RedisPublishSubscribeLib;

namespace RedisSubscribeSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (RedisPubSubClient client = new RedisPubSubClient("192.168.199.117:6379,password=password"))
            {
                string topic = "DemoTopic";
                await client.Subscribe(topic, (channel, value) => 
                {
                    Console.WriteLine($"channel: {channel}; value: {value}");
                });
                await Task.Delay(Timeout.Infinite);
            }
        }
    }
}
