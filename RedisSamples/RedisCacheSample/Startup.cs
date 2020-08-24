using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using StackExchange.Redis;

namespace RedisCacheSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
