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
            // ע��Redis�ֲ�ʽ�������
            services.AddDistributedRedisCache(options => 
            {
                // ����redis�����ַ�������ʽΪ "[host or ip]:[port],password=[password][,[host or ip]:[port],password=[password]]"
                options.Configuration = "192.168.199.129:6379,password=password";

                // ������redis�е�ʵ��������key��ǰ׺
                options.InstanceName = "RedisSessionSample";
            });

            // ע��session����
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10); // �Ự��������ʱ��
                options.Cookie.HttpOnly = true; // ����Ϊ�������js���ɷ���
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

            app.UseSession(); // ʹ�ûỰ�м��

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
