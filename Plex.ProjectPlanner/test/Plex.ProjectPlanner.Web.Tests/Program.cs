using Microsoft.AspNetCore.Builder;
using Plex.ProjectPlanner;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Plex.ProjectPlanner.Web.csproj"); 
await builder.RunAbpModuleAsync<ProjectPlannerWebTestModule>(applicationName: "Plex.ProjectPlanner.Web");

public partial class Program
{
}
