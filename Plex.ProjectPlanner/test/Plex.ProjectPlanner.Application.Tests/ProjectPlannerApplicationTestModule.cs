using Volo.Abp.Modularity;

namespace Plex.ProjectPlanner;

[DependsOn(
    typeof(ProjectPlannerApplicationModule),
    typeof(ProjectPlannerDomainTestModule)
)]
public class ProjectPlannerApplicationTestModule : AbpModule
{

}
