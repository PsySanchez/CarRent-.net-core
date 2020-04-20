using System;
using CarRent.DAL.Models;
using CarRent.WebApi.Helpers;
using LinqToDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CarRent.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // indicates whether the publisher will validate when validating the token
                    ValidateIssuer = true,
                    // string representing the publisher
                    ValidIssuer = Configuration.GetValue<string>("JwtAuthentication:ValidIssuer"),
                    // will the token consumer be validated
                    ValidateAudience = true,
                    // token consumer setting
                    ValidAudience = Configuration.GetValue<string>("JwtAuthentication:ValidAudience"),
                    // whether lifetime will be validated
                    ValidateLifetime = true,
                    // setting security key
                    IssuerSigningKey = AuthService.GetSymmetricSecurityKey(Configuration.GetValue<string>("JwtAuthentication:SecurityKey")),
                    // security key validation
                    ValidateIssuerSigningKey = true,
                    // change default token lifetime to 0
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
