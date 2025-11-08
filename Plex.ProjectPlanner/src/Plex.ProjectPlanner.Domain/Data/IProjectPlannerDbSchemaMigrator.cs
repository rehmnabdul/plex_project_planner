using System.Threading.Tasks;

namespace Plex.ProjectPlanner.Data;

public interface IProjectPlannerDbSchemaMigrator
{
    Task MigrateAsync();
}
