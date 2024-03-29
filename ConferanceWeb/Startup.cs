﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using ConferanceWeb.Filters;
using DAL;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Repositories;
using Shared.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace ConferanceWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ConferanceDbContext>(
                options => options.UseSqlServer(
                     "Data Source=.;Database=Conferance;Trusted_Connection=True;Integrated Security=True"));
            services.AddScoped<IConferanceRepository, ConferanceRepository>();
            services.AddScoped<IConferanceService, ConfranceService>();

            services.AddMemoryCache();
            services.AddResponseCaching();


            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0);
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddMvc(
                options=>
                {
                    options.FormatterMappings
                                    .SetMediaTypeMappingForFormat("xml", "application/xml");
                    options.FormatterMappings
                                    .SetMediaTypeMappingForFormat("txt", "text/plain");
                    options.Filters.Add(new LogFilterAttribute("Start request {0}", "End request {0}"));
                    options.CacheProfiles.Add("5min", new Microsoft.AspNetCore.Mvc.CacheProfile() { Duration = 5 * 60 });
                })
            .AddXmlSerializerFormatters();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ConferenceAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Nagłowek używany do autoryzacji",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>()
                {
                  { "Bearer", new string[]{ } }
                });
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg => {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost:54658",
                    ValidAudience = "http://localhost:54658",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superTajnyKlucz123$"))
                };
                cfg.SaveToken = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConferenceAPI");
            });
            app.UseAuthentication();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(1)
                    };
                }
            });

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => routes
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"));


        }
    }
}
