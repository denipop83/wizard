using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Wizard.Api.Extensions;
using Wizard.Data;
using wizard.Utils;
using wizard.Utils.Extensions;
using wizard.Utils.Mediatr;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
ConfigureServices(builder);

var app = builder.Build();
Configure(app);
app.Run();

void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddControllers();

    webApplicationBuilder.Services.AddCommonApiVersioning();
    webApplicationBuilder.Services.AddCommonSwagger(webApplicationBuilder.Configuration,
        xmlDocAssemblyNames: new List<string>
        {
            Assembly.GetExecutingAssembly().GetName().Name!
        });

    var configuration = webApplicationBuilder.Configuration;
    var provider = configuration.GetValue("DatabaseProvider", "sqlite");
    var connectionString = configuration.GetConnectionString("Database");
    var migrationsAssembly = configuration.GetValue<string>("MigrationsAssembly");

    webApplicationBuilder.Host.ConfigureAppConfiguration(configurationBuilder => configurationBuilder
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables());

    webApplicationBuilder.Services.AddDbContext<AppDbContext>(options =>
    {
        _ = provider switch
        {
            "sqlite" => options.UseSqlite(connectionString, x => x.MigrationsAssembly(migrationsAssembly)),
            "postgres" => options.UseNpgsql(connectionString, x => x.MigrationsAssembly(migrationsAssembly)),
            _ => throw new Exception($"Unsupported provider: {provider}")
        };
    });

    builder.Services.AddCors(x => x.AddDefaultPolicy(p => p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

    builder.WebHost.UseSerilog((hostingContext, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

    webApplicationBuilder.Services.AddMediatR(Assembly.Load("Wizard.App"));
    webApplicationBuilder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    webApplicationBuilder.Services.AddScoped(typeof(ValidationBehavior<,>), typeof(LoggingBehavior<,>));
}

void Configure(WebApplication webApplication)
{
    webApplication.UseCors();

    //assuming that this app are not scaled-out.
    webApplication.MigrateDatabase();

    webApplication.UseSwagger();
    webApplication.UseSwaggerUI();

    webApplication.UseRouting();
    webApplication.UseEndpoints(endpoints => { endpoints.MapControllers(); });
}