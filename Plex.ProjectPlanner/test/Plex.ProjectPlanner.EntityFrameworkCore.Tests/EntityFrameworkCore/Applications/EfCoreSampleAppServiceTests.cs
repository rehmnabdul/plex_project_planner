using Plex.ProjectPlanner.Samples;
using Xunit;

namespace Plex.ProjectPlanner.EntityFrameworkCore.Applications;

[Collection(ProjectPlannerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProjectPlannerEntityFrameworkCoreTestModule>
{

}
