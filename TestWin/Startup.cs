using Autofac;
using Autofac.Extensions.DependencyInjection;
using ce.autofac.extension;
using aycsoft.cache;
using aycsoft.database;
using aycsoft.util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace TestWin
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
            builder.RegisterIBLL();
            DbRegister.Register(builder);
            builder.RegisterType(typeof(CacheByRedis)).As(typeof(ICache)).SingleInstance();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            IocManager.Instance.Container = app.ApplicationServices.GetAutofacRoot();
            string baseDir = Directory.GetCurrentDirectory();
            ConfigHelper.SetValue("baseDir", baseDir);

        }
    }
}
