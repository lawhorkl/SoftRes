using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftRes.Auth;
using SoftRes.BlizzardAPI.Items;
using SoftRes.Config;
using SoftRes.Loaders;

namespace SoftRes
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
            services.AddControllers();
            services.AddHttpClient();

            var applicationConfig = new ApplicationConfig();
            Configuration.Bind("Application", applicationConfig);
            services.AddSingleton(applicationConfig);

            services.AddTransient<IBlizzardItemAPI, BlizzardItemAPI>();
            services.AddSingleton<IBlizzardAuthHandler, BlizzardAuthHandler>();
            services.AddHostedService<ItemLoader>();
            // services.AddOptions();

            // var authSection = Configuration.GetSection("Auth");
            // services.Configure<AuthConfig>(authSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
