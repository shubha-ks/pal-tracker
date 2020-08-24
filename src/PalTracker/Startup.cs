using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PalTracker
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

            var message = Configuration.GetValue<string>("WELCOME_MESSAGE");
            if (string.IsNullOrEmpty(message))
            {
                throw new ApplicationException("WELCOME_MESSAGE not configured.");
            }
            services.AddSingleton(sp => new WelcomeMessage(message));

            var cfInstanceIndex = Configuration.GetValue<string>("CF_INSTANCE_INDEX");
            if (string.IsNullOrEmpty(cfInstanceIndex))
            {
                throw new ApplicationException("Cf Instance Index not configured.");
            }
            var port = Configuration.GetValue<string>("PORT");
            if (string.IsNullOrEmpty(port))
            {
                throw new ApplicationException("Port not configured.");
            }
            var memoryLimit = Configuration.GetValue<string>("MEMORY_LIMIT");
            if (string.IsNullOrEmpty(memoryLimit))
            {
                throw new ApplicationException("Memory Limit not configured.");
            }
            var cfInstanceAddr = Configuration.GetValue<string>("CF_INSTANCE_ADDR");
            if (string.IsNullOrEmpty(cfInstanceAddr))
            {
                throw new ApplicationException("Cf Instance Addr not configured.");
            }
            services.AddSingleton(sp => new CloudFoundryInfo(port, memoryLimit, cfInstanceIndex, cfInstanceAddr));
            
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
