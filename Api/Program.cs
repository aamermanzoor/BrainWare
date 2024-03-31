using Api.Data.Infrastructure;
using Api.Data.Services;
using Api.Data.Services.Interfaces;
using Api.Mapping.Profiles;
using Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder);
ConfigureLogger(builder);
ConfigureServices(builder);

var app = builder.Build();

ConfigureApp(app);

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddLogging();

    builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(DatabaseContext))));

    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
    builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
}

void ConfigureConfiguration(WebApplicationBuilder builder)
{
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

    builder.Configuration.AddConfiguration(configuration);
}

void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalErrorHandlerMiddleware>();

    app.UseAuthorization();

    app.MapControllers();
}

void ConfigureLogger(WebApplicationBuilder builder)
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog();
}


