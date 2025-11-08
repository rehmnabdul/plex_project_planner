using System;
using Volo.Abp.Application.Dtos;

namespace Plex.ProjectPlanner.Settings;

public class ApplicationSettingDto : AuditedEntityDto<Guid>
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
}

