using Microsoft.Extensions.Options;
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

        // Repositórios
        builder.Services.AddTransient<IUsuarioRepository, UsuarioRespository>();
        builder.Services.AddTransient<IProductControlRepository, ProductControlRepository>();
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagguerOptions>();

        // Hangfire com PostgreSQL (corrigido Database)
        builder.Services.AddHangfire(config =>
            config.UsePostgreSqlStorage("Host=localhost;" +
                "Port=5432;" +
                "Database=postgres;" +
                "User Id=postgres;" +
                "Password=ishmael;" +
                "Include Error Detail=true"));
        builder.Services.AddHangfireServer();

        // Autenticação JWT
        var key = Encoding.ASCII.GetBytes(Key.Secret);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "MyPolicy",
                policy =>
                {
                    policy.WithOrigins("http://localhost:8082")
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
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        var app = builder.Build();

        var versionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Pipeline HTTP
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

        // Hangfire Dashboard (visível em /hangfire)
        app.UseHangfireDashboard();

        // Job Recorrente - Executado diariamente
        RecurringJob.AddOrUpdate<IProductControlRepository>(
            "Remove-products",
            repo => repo.RemoveProduct(),
            Cron.Daily);

        app.MapControllers();

        app.Run();
    }
}
