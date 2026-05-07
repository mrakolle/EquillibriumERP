using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using EquillibriumERP.Infrastructure.Persistence;
using System.IO;

namespace EquillibriumERP.Infrastructure.Data;

public class TenantDbContextFactoryDesignTime : IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("MasterDb");

        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        // HARD FIX: never allow tenant resolution here
        return new TenantDbContext(options, "ignored");
    }
}