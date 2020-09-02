using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisDistributedLockSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (RedisDistributedLock distributedLock = new RedisDistributedLock("192.168.199.117:6379,password=password", 8))
            {
                string lockName = "LockDemo";
                string appIdentity = $"RedisDistributedLockSample-{Guid.NewGuid()}";
                TimeSpan timeout = TimeSpan.FromSeconds(10);
                Random random = new Random();
                byte[] randomData = new byte[1];
                int maxWaitMillisecond = 256;

                while (true)
                {
                    await distributedLock.GetLock(lockName, appIdentity, timeout);

                    Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss.fffff}\t{appIdentity}");
                    random.NextBytes(randomData);
                    await Task.Delay(randomData[0] % maxWaitMillisecond);

                    await distributedLock.ReleaseLock(lockName, appIdentity);

                    random.NextBytes(randomData);
                    await Task.Delay(randomData[0] % maxWaitMillisecond);
                }
            }
        }
    }
}

// https://stackexchange.github.io/StackExchange.Redis/Configuration