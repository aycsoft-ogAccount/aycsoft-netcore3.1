using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aycsoft.im
{
    public class Startup
    {
        public const string CorsName = "SignalRCors";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors(op => { op.AddPolicy(CorsName, set => { set.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials(); }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseCors(CorsName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatsHub>("/ChatsHub");
            });
        }
    }
}
