- 安装Package
`Install-Package Microsoft.Extensions.Caching.Redis -Version 2.2.0 -Project RedisSessionSample`

- 更新Package
`Update-Package Microsoft.Extensions.Caching.Redis -Version 2.2.0 -Project RedisSessionSample`

- 卸载Package
`Uninstall-Package Microsoft.Extensions.Caching.Redis -Version 2.2.0 -Project RedisSessionSample`
---

- 在`Startup ConfigureServices`添加如下代码
```C#
// 注册Redis分布式缓存服务
services.AddDistributedRedisCache(options => 
{
    // 配置redis连接字符串，格式为 "[host or ip]:[port],password=[password]"
    options.Configuration = "192.168.199.129:6379,password=password";

    // 配置在redis中的实例名，即key的前缀
    options.InstanceName = "RedisSessionSample";
});

// 注册session服务
services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // 会话滑动过期时间
    options.Cookie.HttpOnly = true; // 设置为浏览器端js不可访问
});
```

- 在`Startup Configure`添加如下代码
```C#
app.UseSession(); // 使用会话中间件
```
---
- 创建`SessionSampleController`
- 添加`SetAndGetValue`测试保存和获取单条会话数据
- 添加`GetAllValue`测试获取会话中所有数据
---

测试:
```http
Request:
GET /api/SessionSample/SetAndGetValue?key=nickname&value=jieke HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Sun, 23 Aug 2020 04:28:46 GMT
Content-Type: text/plain
Server: Kestrel
Content-Length: 27

Key: nickname, Value: jieke
```

```http
Request:
GET /api/SessionSample/GetAllValue HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Cache-Control: no-cache

Response:
HTTP/1.1 200 OK
Date: Sun, 23 Aug 2020 04:30:55 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked

[{"Key":"nickname","Value":"jieke"}]
```
---
> 使用Nuget 安装指定版本package或者更新package到指定版本
> https://www.cnblogs.com/wiseblog/articles/8092891.html
> 
> ASP.NET Core 使用Redis存储Session
> https://www.cnblogs.com/stulzq/p/7729105.html
> 
> .net core项目中IDistributeCache接口Redis分布式缓存的使用
> https://www.jianshu.com/p/4775c131ec25
> 
> ASP.NET Core中间件实现分布式 Session
> https://www.jianshu.com/p/a772f4ca9015

