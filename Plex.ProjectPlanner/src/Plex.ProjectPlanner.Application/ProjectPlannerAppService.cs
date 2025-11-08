using Plex.ProjectPlanner.Localization;
using Volo.Abp.Application.Services;

namespace Plex.ProjectPlanner;

/* Inherit your application services from this class.
 */
public abstract class ProjectPlannerAppService : ApplicationService
{
    protected ProjectPlannerAppService()
    {
        LocalizationResource = typeof(ProjectPlannerResource);
    }
}
