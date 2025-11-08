using Volo.Abp.Modularity;

namespace Plex.ProjectPlanner;

public abstract class ProjectPlannerApplicationTestBase<TStartupModule> : ProjectPlannerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
