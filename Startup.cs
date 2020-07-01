using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using MongoDB.Driver;
using SoftRes.Auth;
using SoftRes.BlizzardAPI.Items;
using SoftRes.Config;
using SoftRes.Loaders;
using SoftRes.SharpMongo;
using SoftRes.db;

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
            bool inMemory = Configuration.GetValue<bool>("MongoDB:InMemory");
            services.AddControllers();
            services.AddHttpClient();

            var applicationConfig = new ApplicationConfig();
            Configuration.Bind("Application", applicationConfig);
            services.AddSingleton(applicationConfig);

            // With SharpMongo ORM Layer we never directly DI MongoClient
            // anywhere else and creating a new interface just to DI
            // a connection string is kind of silly.
            MongoClient client;
            if (inMemory)
            {
                var runner = MongoDbRunner.Start();
                client = new MongoClient(runner.ConnectionString);
            }
            else
            {
                var connectionString = Configuration.GetValue<string>("MongoDB:ConnectionString");
                client = new MongoClient(connectionString);
            }
            var dbName = Configuration.GetValue<string>("MongoDB:DatabaseName");
            services.AddTransient<IMongoFactory, MongoFactory>(_ =>
                new MongoFactory(
                    client, 
                    dbName
                )
            );
            
            services.AddTransient<IBlizzardItemAPI, BlizzardItemAPI>();
            services.AddSingleton<IBlizzardAuthHandler, BlizzardAuthHandler>();
            services.AddTransient<IFileLoader, FileLoader>();
            services.AddHostedService<ItemLoader>();
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
