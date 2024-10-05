namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DataContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            //.UseNpgsql(Configuration.GetConnectionString("ApiDatabase"))
            .UseNpgsql(@"Host=localhost;Username=postgres;Database=relativity")
            .UseSnakeCaseNamingConvention();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<ShowCategory> ShowCategories { get; set; }
}