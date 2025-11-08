using Plex.ProjectPlanner.Books;
using Xunit;

namespace Plex.ProjectPlanner.EntityFrameworkCore.Applications.Books;

[Collection(ProjectPlannerTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<ProjectPlannerEntityFrameworkCoreTestModule>
{

}