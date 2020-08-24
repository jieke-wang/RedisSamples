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

namespace RedisSessionSample
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
            // 注册Redis分布式缓存服务
            services.AddDistributedRedisCache(options => 
            {
                // 配置redis连接字符串，格式为 "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession(); // 使用会话中间件

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
