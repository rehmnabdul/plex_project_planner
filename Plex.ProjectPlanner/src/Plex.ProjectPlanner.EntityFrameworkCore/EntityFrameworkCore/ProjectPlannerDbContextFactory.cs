using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Plex.ProjectPlanner.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ProjectPlannerDbContextFactory : IDesignTimeDbContextFactory<ProjectPlannerDbContext>
{
    public ProjectPlannerDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ProjectPlannerEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ProjectPlannerDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new ProjectPlannerDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Plex.ProjectPlanner.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
