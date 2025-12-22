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
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace aycsoft.webapi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //配置Swagger
            //注册Swagger生成器，定义一个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "接口文档",
                    Description = "力软敏捷框架Core版webapi"
                });
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // 为 Swagger 设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
            builder.RegisterIBLL();
            DbRegister.Register(builder);
            builder.RegisterType(typeof(CacheByRedis)).As(typeof(ICache)).SingleInstance();
            builder.RegisterType(typeof(Operator)).As(typeof(IOperator)).SingleInstance();
            builder.RegisterType(typeof(LogBLL)).As(typeof(LogIBLL)).SingleInstance();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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



            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<AuthorizeMiddleware>();

            //启用中间件服务生成Swagger
            app.UseSwagger();
            //启用中间件服务生成SwaggerUI，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "aycsoft.adms.webapi.v1");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
