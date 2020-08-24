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

            // ����redis�����ַ�������ʽΪ "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
            services.AddStackExchangeRedisCache(options =>
            {
                // ����redis�����ַ�������ʽΪ "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
                //options.Configuration = "192.168.199.129:6379,password=password";

                options.ConfigurationOptions= new StackExchange.Redis.ConfigurationOptions 
                {
                    DefaultDatabase = 8, // ָ��Ĭ�����ݿ�
                    Password = "password", // ָ������
                    KeepAlive = 60, // ��������ʱ��,��λΪ��
                    ConnectRetry = 3, // ���Դ���
                    ClientName = "RedisCacheSample", 
                    AsyncTimeout = 1000,
                    SyncTimeout = 1000,
                    AbortOnConnectFail = true,
                };
                options.ConfigurationOptions.EndPoints.Add("192.168.199.129", 6379); // ָ�������ַ

                // ������redis�е�ʵ��������key��ǰ׺
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
