using System.IdentityModel.Tokens.Jwt;
using Enrollment.EntityFramework;
using Enrollment.EntityFramework.Identity.DbContexts;
using Enrollment.EntityFramework.Identity.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace EnrollmentApplication
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication();
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
                .AddDbContext<EnrollmentDbContext>(options => 
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));


            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonLetterOrDigit = true;
                o.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            services.AddOptions();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "Cookies";
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });

            //app.UseOpenIdConnectAuthentication(options =>
            //{
            //    options.AutomaticChallenge = true;
            //    options.AuthenticationScheme = "Oidc";
            //    options.SignInScheme = "Cookies";

            //    options.Authority = "http://id.cttit.local/";
            //    options.RequireHttpsMetadata = false;

            //    options.ClientId = "EnrollmentApplication";
            //    options.ResponseType = "id_token token";

            //    options.Scope.Add("openid");
            //    options.Scope.Add("email");
            //    options.Scope.Add("api1");
            //});

            app.UseDeveloperExceptionPage();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("AngularDeepLinkingRoute", "{*url}",
                    new { controller = "Home", action = "Index" });
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
