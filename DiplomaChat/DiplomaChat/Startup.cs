using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Extensions;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Common.MessageQueueing.Configuration;
using DiplomaChat.Common.MessageQueueing.Extensions.RabbitMQ;
using DiplomaChat.Constants;
using DiplomaChat.DataAccess.Context;
using DiplomaChat.Domain.Models.Configurations;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using HeaderNames = DiplomaChat.Constants.HeaderNames;
using Schemes = DiplomaChat.Constants.Schemes;

namespace DiplomaChat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseConnectionString = Configuration.GetConnectionString(EnvironmentVariables.DatabaseConnectionString);
            services.AddDbContext<DiplomaChatContext>(options => options.UseSqlServer(databaseConnectionString).EnableDetailedErrors());

            services.AddScoped<IDiplomaChatContext, DiplomaChatContext>();

            var rabbitMqConfiguration = Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
            services.AddRabbitMQ(rabbitMqConfiguration);

            services.AddSingleton(_ =>
            {
                var requestLimitConfiguration = Configuration
                    .GetSection(AppSettings.RequestLimitConfiguration)
                    .Get<RequestLimitConfiguration>();

                return requestLimitConfiguration;
            });

            services.AddScoped<IResponseMapper, ResponseMapper>();

            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddJwt(jwtConfiguration);

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers()
                .AddFluentValidation(configuration =>
                {
                    configuration.DisableDataAnnotationsValidation = true; //Attributes shouldn't work
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DiplomaChat", Version = "v1" });

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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiplomaChat v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}