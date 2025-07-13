
﻿using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ValidityControl;
using System.Text;
using Microsoft.OpenApi.Models;
using ValidityControl.Infraestrutura.Repositories;
using ValidityControl.DoMain.Models;
using Microsoft.AspNetCore.Mvc;
using ValidityControl.Swagguer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ValidityControl.Infraestrutura;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<SwagguerDefaultsValue>();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ReportApiVersions = true;
        });

        builder.Services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddTransient<IUsuarioRepository, UsuarioRespository>();
        builder.Services.AddTransient<IProductControlRepository, ProductControlRepository>();
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagguerOptions>();

      

        // Autenticação JWT
        var key = Encoding.ASCII.GetBytes(Key.Secret);




        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddHangfire(config =>
            config.UsePostgreSqlStorage(connectionString));

        builder.Services.AddHangfireServer();

        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        builder.WebHost.UseUrls($"http://*:{port}");





        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "MyPolicy",
                policy =>
                {

                    policy.WithOrigins("http://localhost:8080",
                        "http://localhost:8080/validity-control-frontend/"


                        )


                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        var app = builder.Build();

        var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/erro-development");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"ValidyControl - {description.GroupName.ToUpper()}");
                }
            });
        }
        else
        {
            app.UseExceptionHandler("/erro");
        }

        app.UseCors("MyPolicy");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHangfireDashboard();



        app.UseHangfireDashboard();


        RecurringJob.AddOrUpdate<IProductControlRepository>(
            "Remove-products",
            repo => repo.RemoveProduct(),
            Cron.Daily);

        app.MapControllers();

        app.Run();
    }
}



