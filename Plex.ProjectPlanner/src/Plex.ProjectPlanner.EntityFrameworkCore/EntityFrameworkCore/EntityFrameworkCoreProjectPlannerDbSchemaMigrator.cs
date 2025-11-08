using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plex.ProjectPlanner.Data;
using Volo.Abp.DependencyInjection;

namespace Plex.ProjectPlanner.EntityFrameworkCore;

public class EntityFrameworkCoreProjectPlannerDbSchemaMigrator
    : IProjectPlannerDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreProjectPlannerDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ProjectPlannerDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ProjectPlannerDbContext>()
            .Database
            .MigrateAsync();
    }
}
