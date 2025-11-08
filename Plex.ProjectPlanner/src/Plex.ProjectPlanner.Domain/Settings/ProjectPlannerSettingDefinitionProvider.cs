using Volo.Abp.Settings;

namespace Plex.ProjectPlanner.Settings;

public class ProjectPlannerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProjectPlannerSettings.MySetting1));
    }
}
