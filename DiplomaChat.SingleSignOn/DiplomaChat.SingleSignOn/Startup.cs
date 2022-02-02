using System;
using System.Collections.Generic;
using DiplomaChat.Common.Accessors.Endpoint;
using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Extensions;
using DiplomaChat.Common.Infrastructure.Configuration;
using DiplomaChat.Common.Infrastructure.Generators.Hashing;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Common.Infrastructure.Selectors;
using DiplomaChat.Common.Logging.Extensions;
using DiplomaChat.SingleSignOn.DataAccess.Context;
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
        private IServiceProvider _serviceProvider;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseConnectionString = Configuration.GetConnectionString("DIPLOMA_CHAT_SSO_DB_CONNECTION_STRING");
            services.AddDbContext<SSOContext>(options => options.UseSqlServer(databaseConnectionString!));

            services.AddScoped<ISSOContext, SSOContext>();

            var hashConfiguration = Configuration.GetSection("HashConfiguration").Get<HashConfiguration>();
            services.AddScoped<IHashGenerator, SaltedHashGenerator>(_ => new SaltedHashGenerator(hashConfiguration));

            services.AddScoped<IResponseMapper, ResponseMapper>();
            services.AddScoped<ICoalesceSelector, CoalesceSelector>();
            services.AddScoped<IEndpointInformationAccessor, EndpointInformationAccessor>();

            services.AddLoggingPipeline().AddLoggers().AddSanitizing(typeof(Startup).Assembly);

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddControllers().AddFluentValidation(configuration =>
            {
                configuration.DisableDataAnnotationsValidation = true; //Attributes shouldn't work
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddJwt(jwtConfiguration);

            services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Json Web Token for authorization. Write: 'Bearer {your token}'",
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Schemes.Bearer
                };
                options.AddSecurityDefinition(securityScheme.Scheme, securityScheme);

                var requirement = new OpenApiSecurityRequirement
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

                options.AddSecurityRequirement(requirement);

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DiplomaChat.SingleSignOn", Version = "v1" });
            });

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _serviceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiplomaChat.SingleSignOn v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}