using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Extensions;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Common.MessageQueueing.Configuration;
using DiplomaChat.Common.MessageQueueing.Extensions.RabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using TileGameServer.InSession.DataAccess.Context;
using TileGameServer.InSession.Hubs;
using HeaderNames = Microsoft.Net.Http.Headers.HeaderNames;

namespace TileGameServer.InSession
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
            var rabbitMqConfiguration = Configuration.GetSection("RabbitMQCOnfiguration").Get<RabbitMQConfiguration>();
            services.AddRabbitMQ(rabbitMqConfiguration);
            services.AddMessageQueueingServices(typeof(Startup));

            services.AddScoped<IResponseMapper, ResponseMapper>();

            var jwtConfiguration = Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            services.AddJwt(jwtConfiguration);

            services.AddAuthentication();
            services.AddAuthorization();

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

            services.AddSingleton<IInSessionContext, LazyInSessionContext>();
            services.AddSignalR();
            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMessageQueueingServices(typeof(Startup));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiplomaChat.InSession v1"));
            }

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