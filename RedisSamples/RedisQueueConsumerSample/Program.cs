using System;
using System.Threading.Tasks;

using RedisQueueLib;

namespace RedisQueueConsumerSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await ConsumerSample1();
            await ConsumerSample2();
        }

        static async Task ConsumerSample1()
        {
            using (RedisMQClient client = new RedisMQClient("192.168.199.117", 6379, "password", 8))
            {
                string queueName = "DemoQueue";
                while (true)
                {
                    string message = client.GetMessage(queueName);
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine("暂无消息");
                        await Task.Delay(10);
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                }
            }
        }

        static async Task ConsumerSample2()
        {
            using (RedisMQClient client = new RedisMQClient("192.168.199.117", 6379, "password", 8))
            {
                string queueName = "DemoQueue";
                TimeSpan timeout = TimeSpan.FromSeconds(10);
                while (true)
                {
                    string message = client.GetMessageBlock(queueName, timeout);
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine("暂无消息");
                        await Task.Delay(0);
                    }
                    else
                    {
                        Console.WriteLine(message);
                    }
                }
            }
        }
    }
}
