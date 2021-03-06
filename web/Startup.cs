using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web.Helpers;

namespace web
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
            services.AddSingleton<SiteCache>();

            services.AddScoped<IOperationAccessor, OperationAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            CommunAxiom.Transformations.Business.Setup.Configure(services);
            CommunAxiom.Transformations.DAL.Setup.Configure("DbConfig", services, Configuration);

            services.AddLocalization();

            services.AddControllersWithViews()
                .AddMvcLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            SeedTestData.Execute(app.ApplicationServices).GetAwaiter().GetResult();
        }
    }
}
