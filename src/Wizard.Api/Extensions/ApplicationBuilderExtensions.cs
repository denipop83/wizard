using Microsoft.EntityFrameworkCore;
using Wizard.Data;
using Wizard.Domain.Entities;

namespace Wizard.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Applies pending migrations
    /// </summary>
    /// <remarks>Use only for one instance app</remarks>
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
        
        SeedDatabase(dbContext);
    }

    private static void SeedDatabase(AppDbContext appDbContext)
    {
        if (appDbContext.Countries.Any())
            return;

        const int countriesSeedCount = 3;
        const int provinceSeedCount = 3;
        
        var countries = Enumerable.Range(0, countriesSeedCount)
            .Select(countriesIndex => new Country
            {
                Name = $"country_{countriesIndex}",
                Provinces = Enumerable.Range(0, provinceSeedCount)
                    .Select(provinceIndex => new Province
                    {
                        Name = $"country_{countriesIndex} province_{provinceIndex}"
                    }).ToArray()
            });

        appDbContext.AddRange(countries);
        appDbContext.SaveChanges();
    }
}