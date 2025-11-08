using Volo.Abp.Modularity;

namespace Plex.ProjectPlanner;

/* Inherit from this class for your domain layer tests. */
public abstract class ProjectPlannerDomainTestBase<TStartupModule> : ProjectPlannerTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
