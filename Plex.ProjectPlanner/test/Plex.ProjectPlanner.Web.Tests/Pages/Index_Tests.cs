using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Plex.ProjectPlanner.Pages;

[Collection(ProjectPlannerTestConsts.CollectionDefinitionName)]
public class Index_Tests : ProjectPlannerWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
