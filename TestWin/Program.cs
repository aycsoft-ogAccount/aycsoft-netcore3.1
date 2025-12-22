using Autofac;
using Autofac.Extensions.DependencyInjection;
using ce.autofac.extension;
using aycsoft.application;
using aycsoft.cache;
using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IContainer = Autofac.IContainer;

namespace TestWin
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string baseDir = Directory.GetCurrentDirectory();
            ConfigHelper.SetValue("baseDir", baseDir);

            var das = ConfigHelper.GetConfig();
            Console.WriteLine(das);

            var container = ConfigureContainer();

            IocManager.Instance.Container = container.BeginLifetimeScope();

            //BuildWebHost(new string[] { "" }).Run();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            DbRegister.Register(builder);
            builder.RegisterType(typeof(CacheByRedis)).As(typeof(ICache)).SingleInstance();
            builder.RegisterType(typeof(Operator)).As(typeof(IOperator)).SingleInstance();
            builder.RegisterType(typeof(LogBLL)).As(typeof(LogIBLL)).SingleInstance();

            return builder.Build();
        }
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .UseServiceProviderFactory(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
