using Plex.ProjectPlanner.Samples;
using Xunit;

namespace Plex.ProjectPlanner.EntityFrameworkCore.Domains;

[Collection(ProjectPlannerTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProjectPlannerEntityFrameworkCoreTestModule>
{

}
