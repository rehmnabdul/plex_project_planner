using Plex.ProjectPlanner.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Plex.ProjectPlanner.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProjectPlannerEntityFrameworkCoreModule),
    typeof(ProjectPlannerApplicationContractsModule)
)]
public class ProjectPlannerDbMigratorModule : AbpModule
{
}
