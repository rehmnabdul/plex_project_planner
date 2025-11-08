using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Plex.ProjectPlanner.Settings;

public class ApplicationSetting : AuditedEntity<Guid>, IMultiTenant
{
    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid? TenantId { get; set; }

    protected ApplicationSetting()
    {
    }

    public ApplicationSetting(Guid id, string key, string value, string? description = null, Guid? tenantId = null)
        : base(id)
    {
        Key = key;
        Value = value;
        Description = description;
        TenantId = tenantId;
    }
}

