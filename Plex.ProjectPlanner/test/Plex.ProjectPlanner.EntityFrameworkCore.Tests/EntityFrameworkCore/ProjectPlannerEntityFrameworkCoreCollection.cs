using Xunit;

namespace Plex.ProjectPlanner.EntityFrameworkCore;

[CollectionDefinition(ProjectPlannerTestConsts.CollectionDefinitionName)]
public class ProjectPlannerEntityFrameworkCoreCollection : ICollectionFixture<ProjectPlannerEntityFrameworkCoreFixture>
{

}
