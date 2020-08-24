- 安装Package
`Install-Package Microsoft.Extensions.Caching.StackExchangeRedis -Version 3.1.7 -Project RedisCacheSample`

- 更新Package
`Update-Package Microsoft.Extensions.Caching.StackExchangeRedis -Version 3.1.7 -Project RedisCacheSample`

- 卸载Package
`Uninstall-Package Microsoft.Extensions.Caching.StackExchangeRedis -Version 3.1.7 -Project RedisCacheSample`
---

- 在`Startup ConfigureServices`添加如下代码
```C#
services.AddDistributedMemoryCache();

// 配置redis连接字符串，格式为 "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
services.AddStackExchangeRedisCache(options =>
{
    // 配置redis连接字符串，格式为 "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
    //options.Configuration = "192.168.199.129:6379,password=password";

    options.ConfigurationOptions= new StackExchange.Redis.ConfigurationOptions 
    {
        DefaultDatabase = 8, // 指定默认数据库
        Password = "password", // 指定密码
        KeepAlive = 60, // 设置连接时间,单位为秒
        ConnectRetry = 3, // 重试次数
        ClientName = "RedisCacheSample", 
        AsyncTimeout = 1000,
        SyncTimeout = 1000,
        AbortOnConnectFail = true,
    };
    options.ConfigurationOptions.EndPoints.Add("192.168.199.129", 6379); // 指定服务地址

    // 配置在redis中的实例名，即key的前缀
    options.InstanceName = "RedisCacheSample.";
});
```
---
测试:
```http
Request:
GET /api/Values/Set?key=nickname&value=jieke HTTP/1.1
Host: localhost:5000
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Mon, 24 Aug 2020 07:42:04 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{"key":"nickname","value":"jieke"}
```

```http
Request:
GET /api/Values/Get?key=nickname HTTP/1.1
Host: localhost:5000
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Mon, 24 Aug 2020 07:43:34 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{"key":"nickname","value":"jieke"}
```

```http
Request:
GET /api/Values/SetWithExpiration?key=nickname&value=jieke&slidingExpirationInSecond=60 HTTP/1.1
Host: localhost:5000
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Mon, 24 Aug 2020 07:45:33 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{"key":"nickname","value":"jieke","slidingExpirationInSecond":60}
```

```http
Request:
GET /api/Values/GetWithRefresh?key=nickname HTTP/1.1
Host: localhost:5000
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Mon, 24 Aug 2020 07:47:49 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{"key":"nickname","value":"jieke"}
```

```http
Request:
GET /api/Values/Remove?key=nickname HTTP/1.1
Host: localhost:5000
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Mon, 24 Aug 2020 07:49:10 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

{"key":"nickname","value":"jieke"}
```
---
> 使用Nuget 安装指定版本package或者更新package到指定版本
> https://www.cnblogs.com/wiseblog/articles/8092891.html
> 
> ASP.NET Core 使用 Redis 实现分布式缓存：Docker、IDistributedCache、StackExchangeRedis
> https://www.cnblogs.com/whuanle/p/11360468.html
