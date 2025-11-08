using Plex.ProjectPlanner.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Plex.ProjectPlanner.Web.Pages;

public abstract class ProjectPlannerPageModel : AbpPageModel
{
    protected ProjectPlannerPageModel()
    {
        LocalizationResourceType = typeof(ProjectPlannerResource);
    }
}
