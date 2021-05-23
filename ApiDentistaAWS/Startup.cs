using ApiDentistaAWS.Data;
using ApiDentistaAWS.Helper;
using ApiDentistaAWS.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDentistaAWS
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
            String cadena = this.Configuration.GetConnectionString("mysql");
            services.AddTransient<RepositoryClientes>();
            services.AddTransient<RepositoryTratamientos>();
            services.AddTransient<HelperToken>();
            services.AddDbContextPool<DentistaContext>
                (options => options.UseMySql(
                    cadena, ServerVersion.AutoDetect(cadena)
                    ));
            services.AddCors(options => options.AddPolicy("AlowOrigin", c => c.AllowAnyOrigin()));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Api MySql AWS",
                    Version = "v1",
                    Description = "Api MySql AWS"
                });
            });
            HelperToken helper = new HelperToken(Configuration);
            //Add Authentication with the options to helper
            services.AddAuthentication(helper.GetAuthOptions()).AddJwtBearer(helper.GetJwtBearerOptions());
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                url: "/swagger/v1/swagger.json", name: "Api v1");
                options.RoutePrefix = "";
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
