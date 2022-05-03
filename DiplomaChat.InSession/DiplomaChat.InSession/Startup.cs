using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Logging.Accessors.Endpoint;
using DiplomaChat.Common.Infrastructure.Logging.Extensions;
using DiplomaChat.Common.Infrastructure.MessageQueueing.Configuration;
using DiplomaChat.Common.Infrastructure.MessageQueueing.Extensions.RabbitMQ;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.InSession.DataAccess.Context;
using DiplomaChat.InSession.DataAccess.Contracts.Context;
using DiplomaChat.InSession.Hubs;
using MediatR;
using Microsoft.OpenApi.Models;
using HeaderNames = Microsoft.Net.Http.Headers.HeaderNames;

namespace DiplomaChat.InSession
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //var rabbitMqConfiguration = Configuration.GetSection("RabbitMQConfiguration").Get<RabbitMQConfiguration>();
            var rabbitMqConfiguration = new RabbitMQConfiguration
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "/",
                UserName = "guest",
                Password = "guest"
            };
            services.AddRabbitMQ(rabbitMqConfiguration);
            services.AddMessageQueueingServices(typeof(Startup));

            services.AddScoped<IResponseMapper, ResponseMapper>();
            //services.AddScoped<IEndpointInformationAccessor, EndpointInformationAccessor>();
            
            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            Console.WriteLine($"jwtConfiguration: {jwtConfiguration}");
            services.AddJwtAuthentication(jwtConfiguration);
            services.AddAuthorization();

            //services.AddLoggingPipeline().AddLoggers().AddSanitizing(typeof(Startup).Assembly);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DiplomaChat.InSession", Version = "v1" });

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

            services.AddSingleton<IInSessionContext, InMemoryInSessionContext>();
            services.AddSignalR();
            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMessageQueueingServices(typeof(Startup));

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapControllers();
            });
        }
    }
}