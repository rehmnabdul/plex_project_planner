using Plex.ProjectPlanner.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Plex.ProjectPlanner.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProjectPlannerController : AbpControllerBase
{
    protected ProjectPlannerController()
    {
        LocalizationResource = typeof(ProjectPlannerResource);
    }
}
