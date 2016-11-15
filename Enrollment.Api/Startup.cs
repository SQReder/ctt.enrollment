using System;
using AutoMapper;
using Enrollment.Api.Infrastructure;
using Enrollment.Api.Infrastructure.Identity;
using Enrollment.Api.Infrastructure.Security;
using Enrollment.Api.Infrastructure.XsrfProtection;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Enrollment.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(o =>
                {
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders();


            services.AddAntiforgery(options => options.HeaderName = "x-xsrf-token");

            services.AddLogging();
            services.AddIdentity<ApplicationUser, IdentityRole>();
            services
                .AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddAuthorization(options =>
            {
                Policies.Trustee.ManageOwned.Configure(options);
            });

            services.AddSingleton<IRandomIdGenerator, RandomIdGenerator>();

            services.AddScoped<ValidateCsrfTokenFilter>();
            services.AddTransient<SecurityHelper>();

            services.AddTransient<Func<ControllerBase, ICurrentUserHelper>>(
                provider => controller => new CurrentUserHelper(provider.GetRequiredService<IUserManagerHelper>(), controller));
            services.AddTransient<IUserManagerHelper, UserManagerHelper>();

            services.AddSingleton<AutoMapper.IConfigurationProvider>(provider =>
            {
                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    MappingConfig.Configure(cfg);
                });

                return mapperConfiguration;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                ApplicationDbSeed.Initialize(serviceScope.ServiceProvider);
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseDeveloperExceptionPage();

            app.UseIdentity();

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .WithMethods("POST", "GET", "OPTIONS", "PUT", "DELETE")
                .AllowAnyOrigin()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
