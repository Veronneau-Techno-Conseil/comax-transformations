using System;
using System.Threading.Tasks;
using CommunAxiom.Transformations.AppModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
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

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "https://localhost:5001/Login";
            });

            services.AddLocalization();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = PathString.FromUriComponent(new Uri("https://localhost:5001/Login"));
                options.Cookie.SameSite = SameSiteMode.None;
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = "823821899740-bc6fkjg302kk8jundfd6ff29u97mfuoi.apps.googleusercontent.com";
                options.ClientSecret = "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";
                options.Authority = "https://accounts.google.com/";
                
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.AccessDeniedPath = PathString.FromUriComponent(new Uri("https://localhost:5001/Login/forbidden"));
                options.SignedOutRedirectUri = PathString.FromUriComponent(new Uri("https://localhost:5001/Login"));
                options.MapInboundClaims = false;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Events = new OpenIdConnectEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/Login/forbidden");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    },
                    OnRedirectToIdentityProviderForSignOut = context =>
                    {
                        context.Response.Redirect(PathString.FromUriComponent(new Uri("https://localhost:5001/Login")));
                        context.HandleResponse();
                        return Task.CompletedTask;
                    },
                    OnTicketReceived = context =>
                    {
                        context.ReturnUri = "https://localhost:5001/Login/updatedatabasecall";
                        return Task.CompletedTask;
                    }
                };

            });

            services.AddAuthorization(policies =>
            {
                policies.AddPolicy("RequireAdministrator",
                    policy => policy.RequireRole("Administrator"));

                policies.AddPolicy("RequireUser",
                    policy => policy.RequireRole("User"));

                policies.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddControllersWithViews()
              .AddMvcLocalization();
                services.AddRazorPages(options =>
                {
                    options.Conventions.AuthorizeFolder("/Module");
                    options.Conventions.AuthorizeFolder("/Welcome");
                    options.Conventions.AuthorizePage("/Home/Index");
                    options.Conventions.AuthorizePage("/Home/Welcome");
                    options.Conventions.AuthorizePage("/Home/Privacy");
                    options.Conventions.AllowAnonymousToFolder("/Login");

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptionsMonitor<OpenIdConnectOptions> optionsMonitor)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

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

            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                RequireHeaderSymmetry = false,
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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

