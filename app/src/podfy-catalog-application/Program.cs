using podfy_catalog_application.Cache;
using podfy_catalog_application.Context;
using podfy_catalog_application.Migrate;
using podfy_catalog_application.Repository;
using podfy_catalog_application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<ICatalogCommandContext, CatalogCommandContext>();
builder.Services.AddScoped<ICatalogQueryContext, CatalogQueryContext>();
builder.Services.AddScoped<ISecretManagerContext, SecretManagerContext>();
builder.Services.AddScoped<IParameterStoreContext, ParameterStoreContext>();
builder.Services.AddScoped<ICatalogCommandoRepository, CatalogCommandRepository>();
builder.Services.AddScoped<ICatalogQueryRepository, CatalogQueryRepository>();
builder.Services.AddScoped<ICatalogQueryService, CatalogQueryService>();
builder.Services.AddScoped<ICatalogCommandService, CatalogCommandService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var cache = builder.Services.BuildServiceProvider().GetService<IDistributedCache>();
var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Migrate>>();

builder.Services.AddSingleton(new Migrate(builder.Configuration, logger, cache));
builder.Services.AddDbContext<CatalogCommandContext>(options =>
{
    var secretModel = new SecretManagerContext(cache).GetSecretValue(builder.Configuration.GetSection("AWS:DbSecretManager").Value);
    var connection = $"Server={secretModel.Host};Port={secretModel.Port};Database={secretModel.DbName};Uid={secretModel.UserName};Pwd={secretModel.Password}";
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
