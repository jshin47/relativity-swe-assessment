using System.Globalization;
using System.Text.Json.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using WebApi.Helpers;
using WebApi.Models.Shows;
using WebApi.Services;

await using var ctx = new DataContext();
await ctx.Database.EnsureDeletedAsync();
await ctx.Database.EnsureCreatedAsync();

ctx.SaveChanges();

var builder = WebApplication.CreateBuilder(args);

// Add services to DI Container
{
    var services = builder.Services;
 
    services.AddDbContext<DataContext>();
    services.AddOutputCache();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Configure services used by controllers
    services.AddScoped<IShowService, ShowService>();
    services.AddScoped<IShowCategoryService, ShowCategoryService>();
}

var app = builder.Build();

{
    // TODO: Add appropriate CORS restrictions for security.
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // Adds a user-friendly default error handler.
    app.UseMiddleware<ErrorHandlerMiddleware>();
    
    // Adds controller middleware
    app.MapControllers();
}

// Import the netflix1.csv data

using var scope = app.Services.CreateScope();

var svc = scope.ServiceProvider.GetService<IShowService>();
var config = new CsvConfiguration(CultureInfo.InvariantCulture);
using (var reader = new StreamReader("netflix1.csv"))
using (var csv = new CsvReader(reader, config))
{
    var records = csv.GetRecords<ShowCsvRowDto>().ToList();
    svc.CreateMany(records);
}

// TODO: Customize URI
app.Run("http://localhost:9080");
