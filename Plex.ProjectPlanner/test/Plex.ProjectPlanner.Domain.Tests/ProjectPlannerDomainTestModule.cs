using Volo.Abp.Modularity;

namespace Plex.ProjectPlanner;

[DependsOn(
    typeof(ProjectPlannerDomainModule),
    typeof(ProjectPlannerTestBaseModule)
)]
public class ProjectPlannerDomainTestModule : AbpModule
{

}
