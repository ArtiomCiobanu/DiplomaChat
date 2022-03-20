using System;
using System.Collections.Generic;
using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Infrastructure.Configuration;
using DiplomaChat.Common.Infrastructure.Generators.Hashing;
using DiplomaChat.Common.Infrastructure.Generators.QRCode;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Common.Infrastructure.Selectors;
using DiplomaChat.Common.Logging.Extensions;
using DiplomaChat.SingleSignOn.Accessors.Endpoint;
using DiplomaChat.SingleSignOn.Constants;
using DiplomaChat.SingleSignOn.DataAccess.Context;
using DiplomaChat.SingleSignOn.Extensions;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            var databaseConnectionString = Configuration.GetConnectionString(EnvironmentVariables.ConnectionString);
            services.AddDbContext<SSOContext>(options => options.UseSqlServer(databaseConnectionString).EnableDetailedErrors());

            services.AddScoped<ISSOContext, SSOContext>();

            var hashConfiguration = Configuration.GetSection("HashConfiguration").Get<HashConfiguration>();
            services.AddScoped<IHashGenerator, SaltedHashGenerator>(s => new SaltedHashGenerator(hashConfiguration));

            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddJwt(jwtConfiguration);

            services.AddScoped<IResponseMapper, ResponseMapper>();
            services.AddScoped<IEndpointInformationAccessor, EndpointInformationAccessor>();

            services.AddLoggingPipeline().AddLoggers().AddSanitizing(typeof(Startup).Assembly);

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

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
                    Scheme = SecuritySchemes.Bearer
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
                            Scheme = SecuritySchemes.OAuth,
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
                        new OpenApiInfo { Title = $"CeremonyPassportAPI {version}", Version = "v1" });
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