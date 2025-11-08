using System.ComponentModel.DataAnnotations;

namespace Plex.ProjectPlanner.Settings;

public class CreateUpdateApplicationSettingDto
{
    [Required]
    [StringLength(256)]
    public string Key { get; set; } = string.Empty;

    [Required]
    [StringLength(2048)]
    public string Value { get; set; } = string.Empty;

    [StringLength(512)]
    public string? Description { get; set; }
}

