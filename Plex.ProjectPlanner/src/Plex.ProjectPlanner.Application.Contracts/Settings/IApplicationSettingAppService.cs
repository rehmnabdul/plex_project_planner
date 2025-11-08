using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Plex.ProjectPlanner.Settings;

public interface IApplicationSettingAppService :
    ICrudAppService<
        ApplicationSettingDto,
        System.Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateApplicationSettingDto>
{
}

