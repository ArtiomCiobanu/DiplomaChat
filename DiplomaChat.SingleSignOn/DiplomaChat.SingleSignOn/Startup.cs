using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Configuration;
using DiplomaChat.Common.Infrastructure.Generators.Hashing;
using DiplomaChat.Common.Infrastructure.Logging.Accessors.Endpoint;
using DiplomaChat.Common.Infrastructure.Logging.Extensions;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.SingleSignOn.Constants;
using DiplomaChat.SingleSignOn.DataAccess.Context;
using DiplomaChat.SingleSignOn.DataAccess.Contracts.Context;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DiplomaChat.SingleSignOn
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
            Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

            var databaseConnectionString = Configuration.GetConnectionString(EnvironmentVariables.ConnectionString);
            Console.WriteLine($"Connection String: {databaseConnectionString}");
            services.AddDbContext<SSOContext>(options => options.UseSqlServer(databaseConnectionString).EnableDetailedErrors());

            services.AddScoped<ISSOContext, SSOContext>();

            var hashConfiguration = Configuration.GetSection("HashConfiguration").Get<HashConfiguration>();
            Console.WriteLine($"HashConfiguration: {JsonConvert.SerializeObject(hashConfiguration)}");
            services.AddScoped<IHashGenerator, SaltedHashGenerator>(s => new SaltedHashGenerator(hashConfiguration));

            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            Console.WriteLine($"Jwt Configuration: {JsonConvert.SerializeObject(jwtConfiguration)}");
            services.AddJwtAuthentication(jwtConfiguration);
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddScoped<IResponseMapper, ResponseMapper>();
            services.AddScoped<IEndpointInformationAccessor, EndpointInformationAccessor>();

            services.AddLoggingPipeline().AddLoggers().AddSanitizing(typeof(Startup).Assembly);

            services.AddControllers().AddFluentValidation(configuration =>
            {
                configuration.DisableDataAnnotationsValidation = true; //Attributes shouldn't work
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Json Web Token for authorization. Write: 'Bearer {your token}'",
                    Name = Headers.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Schemes.Bearer
                };
                options.AddSecurityDefinition(securityScheme.Scheme, securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = securityScheme.Scheme
                            },
                            Scheme = Schemes.OAuth,
                            Name = securityScheme.Scheme,
                            In = securityScheme.In
                        },
                        new List<string>()
                    }
                };

                options.AddSecurityRequirement(securityRequirement);

                var version = GetType().Assembly.GetName().Version;
                if (version is not null)
                {
                    options.SwaggerDoc("v1",
                        new OpenApiInfo { Title = $"DiplomaChat.SingleSignOn {version}", Version = "v1" });
                }
            });

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "DiplomaChat.SingleSignOn v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}