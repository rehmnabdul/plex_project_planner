using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Plex.ProjectPlanner.Localization;

namespace Plex.ProjectPlanner.Web;

[Dependency(ReplaceServices = true)]
public class ProjectPlannerBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ProjectPlannerResource> _localizer;

    public ProjectPlannerBrandingProvider(IStringLocalizer<ProjectPlannerResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
