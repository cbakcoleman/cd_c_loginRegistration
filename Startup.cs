using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ADD THESE LINES -->
using cd_c_loginRegistration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // <-- THROUGH HERE

namespace cd_c_loginRegistration
{
    public class Startup
    {
        // ADD THESE LINES -->
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        } // <-- THROUGH HERE 

        // ADD THIS LINE -->
        public IConfiguration Configuration {get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // ADD THESE LINES -->
            services.AddDbContext<UserContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            // <-- THROUGH HERE
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ADD THESE LINES -->
            app.UseStaticFiles();
            app.UseMvc();
            // <-- THROUGH HERE, AND THEN COMMENT OUT THE FOLLOWING LINES
            // -->
            // app.UseRouting();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // }); <-- THROUGH HERE
        }
    }
}
