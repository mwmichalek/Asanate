using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Mwm.Asana.Service.Utils;
using Mwm.MyQ.Application.Utils;
using Mwm.MyQ.Common.Utils;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mwm.MyQ.Data.Utils;

namespace Mwm.MyQ.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            var configuration = services.AddConfigurationWithUserSecrets();
            services.AddLogging();
            services.AddDatabaseContext(configuration);
            services.AddRepositories();
            //services.AddAsanaServices();
            services.AddMediatR();


            //TODO:(MWM) - policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
            services.AddCors(opt => {
                opt.AddPolicy("DevCorsPolicy", builder => {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );




            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mwm.MyQ.Server", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mwm.MyQ.Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("DevCorsPolicy");

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
