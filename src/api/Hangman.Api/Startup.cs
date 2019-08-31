using Hangman.Core;
using Hangman.Persistence.InMemory.Queries;
using Hangman.Persistence.MongoDb;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangman.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddCors();
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add core requests and notifications
            services.AddMediatR(typeof(Game));

            var mongoDbSection = Configuration.GetSection("MongoDb");
            if (mongoDbSection.Exists())
            {
                // Use mongodb as persistence
                services.Configure<ConfigMongoDb>(mongoDbSection);
                services.AddMongoDb();
            }
            else
            {
                // Use in-memory database if mongodb is not defined
                services.AddInMemoryDb();
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable cors for anyone in dev env only
                app.UseCors(policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                );
            }

            app.UseMvc();
        }
    }
}
