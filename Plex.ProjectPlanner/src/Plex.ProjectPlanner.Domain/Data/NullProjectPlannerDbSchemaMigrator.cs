using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Plex.ProjectPlanner.Data;

/* This is used if database provider does't define
 * IProjectPlannerDbSchemaMigrator implementation.
 */
public class NullProjectPlannerDbSchemaMigrator : IProjectPlannerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
