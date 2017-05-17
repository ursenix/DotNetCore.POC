using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.Data;
using DotNetCore.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DotNetCore.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			var documentDBSettings = Configuration.GetSection("DocumentDBSettings");
			services.Configure<DocumentDBSetting>(documentDBSettings);

			services.AddOptions();

			// Add framework services.
			services.AddMvc();


            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BloggingContext>(opt => opt.UseSqlServer(connectionString));

            //var settingsManager = new SettingsManager(connectionString.Value, Configuration.GetValue<DocumentDBSetting>("DocumentDBSettings"));

            services.AddTransient<ISettings, SettingsManager>();
            services.AddTransient<RepoManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(subApp => {
                    subApp.Run(async context => {
                        await context.Response.WriteAsync("<b>Error happened</b>");
                        await context.Response.WriteAsync(new string(' ', 512)); // Padding for IE
                    });
                });
            }

            app.UseStatusCodePages(subApp => {
                subApp.Run(async context => {
                    await context.Response.WriteAsync("<b>Page not found!</b>");
                    await context.Response.WriteAsync(new string(' ', 512)); // Padding for IE
                });
            });

            /*
            app.Run(context => 
            {
                throw new Exception("Test error");
                //context.Response.StatusCode = 404;
                //return Task.FromResult(0);
            });
            */

			app.UseDefaultFiles();
			app.UseStaticFiles();
            app.UseMvc();
            //app.UseDeveloperExceptionPage();
        }
    }
}
