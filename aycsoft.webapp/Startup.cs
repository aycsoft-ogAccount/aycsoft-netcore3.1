 using Autofac;
using Autofac.Extensions.DependencyInjection;
using ce.autofac.extension;
using aycsoft.application;
using aycsoft.cache;
using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aycsoft.webapp
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
            services.AddResponseCompression();
            services.AddControllersWithViews().AddControllersAsServices();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ��������ӷ���ע��
            builder.RegisterIBLL();
            DbRegister.Register(builder);
            builder.RegisterType(typeof(CacheByRedis)).As(typeof(ICache)).SingleInstance();
            builder.RegisterType(typeof(Operator)).As(typeof(IOperator)).SingleInstance();
            builder.RegisterType(typeof(LogBLL)).As(typeof(LogIBLL)).SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            IocManager.Instance.Container = app.ApplicationServices.GetAutofacRoot();
            string baseDir = env.ContentRootPath;
            ConfigHelper.SetValue("baseDir", baseDir);
            if (env.IsDevelopment())
            {
                ConfigHelper.SetValue("env", "dev");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ConfigHelper.SetValue("env", "pro");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<AuthorizeMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

            QuartzHelper.InitJob().GetAwaiter();
        }
    }
}
