using System;
using AutoMapper;
using Enrollment.EntityFramework;
using Enrollment.Model;
using Enrollment.ServiceLayer;
using Enrollment.Web.Infrastructure.ViewModels;
using Microsoft.AspNet.Routing.Template;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Enrollment.Web
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
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders();
            ;

            services.AddLogging();
            services.AddIdentity<ApplicationUser, IdentityRole>();
            services
                .AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddSingleton<IRandomIdGenerator, RandomIdGenerator>();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("AngularDeepLinkingRoute", "{*url}",
                    new { controller = "Home", action = "Index" });

                ///*config.Routes.MapHttpRoute(
                //        name: "Enrollments",
                //        routeTemplate: "api/courses/{courseId}/students/{userName}",
                //        defaults: new { controller = "Enrollments", userName = RouteParameter.Optional }
                //        );
                //        */
                //routes.MapRoute(
                //    name: "Unities",
                //    template:"api/unityGroups/{unityGroupId}/unities/{unityId?}",
                //    defaults: new { controller = "UnitiesController" },
                //    constraints: new
                //    {
                //        unityGroupId = new GuidRouteConstraint(),
                //        unityId = new GuidRouteConstraint()
                //    });
            });

            ConfigureMapper();
        }

        private void ConfigureMapper()
        {

            Mapper.Initialize(config =>
            {
                config.CreateMap<Enrollee, EnrolleeViewModel>()
                    .ForMember(
                        dest => dest.Address,
                        opts => opts.MapFrom(src => src.Address.Raw));
                config.CreateMap<EnrolleeViewModel, Enrollee>()
                    .ForMember(
                        dest => dest.Address,
                        opts => opts.MapFrom(src => new Address { Raw = src.Address }));

                config.CreateMap<UnityGroup, UnityGroupViewModel>();
                config.CreateMap<UnityGroupViewModel, UnityGroup>();

                config.CreateMap<Unity, UnityViewModel>();
                config.CreateMap<UnityViewModel, Unity>();

                config.CreateMap<Address, AddressViewModel>();

                config.CreateMap<Trustee, TrusteeViewModel>()
                    .ForMember(
                        dest => dest.Address,
                        opts => opts.MapFrom(src => src.Address.Raw));

                config.CreateMap<Admission, AdmissionViewModel>();
                config.CreateMap<AdmissionViewModel, Admission>();
            });
        }
    }
}
