using System;
using System.Threading.Tasks;

using RedisQueueLib;

namespace RedisQueueProducerSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (RedisMQClient client = new RedisMQClient("192.168.199.117", 6379, "password", 8))
            {
                string queueName = "DemoQueue";
            Start:
                string message = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fffff} - {Guid.NewGuid()}";
                Console.WriteLine(message);
                client.SendMessage(queueName, message);
                await Task.Delay(100);
                goto Start;
            }
        }
    }
}
