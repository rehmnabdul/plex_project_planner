using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace Plex.ProjectPlanner.Settings;

// TODO: Add proper authorization with ApplicationSettings permissions
// Temporarily allowing anonymous for testing
[AllowAnonymous]
public class ApplicationSettingAppService : ApplicationService, IApplicationSettingAppService
{
    private readonly IRepository<ApplicationSetting, Guid> _repository;

    public ApplicationSettingAppService(IRepository<ApplicationSetting, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<ApplicationSettingDto> GetAsync(Guid id)
    {
        var setting = await _repository.GetAsync(id);
        return ObjectMapper.Map<ApplicationSetting, ApplicationSettingDto>(setting);
    }

    public async Task<PagedResultDto<ApplicationSettingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Key" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var settings = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ApplicationSettingDto>(
            totalCount,
            ObjectMapper.Map<List<ApplicationSetting>, List<ApplicationSettingDto>>(settings)
        );
    }

    // TODO: Add proper authorization
    public async Task<ApplicationSettingDto> CreateAsync(CreateUpdateApplicationSettingDto input)
    {
        // Check if setting with same key already exists for current tenant
        var queryable = await _repository.GetQueryableAsync();
        var exists = await AsyncExecuter.AnyAsync(queryable.Where(x => x.Key == input.Key));

        if (exists)
        {
            throw new InvalidOperationException($"Application setting with key '{input.Key}' already exists.");
        }

        var setting = new ApplicationSetting(
            GuidGenerator.Create(),
            input.Key,
            input.Value,
            input.Description
        );
        await _repository.InsertAsync(setting);
        return ObjectMapper.Map<ApplicationSetting, ApplicationSettingDto>(setting);
    }

    // TODO: Add proper authorization
    public async Task<ApplicationSettingDto> UpdateAsync(Guid id, CreateUpdateApplicationSettingDto input)
    {
        var setting = await _repository.GetAsync(id);
        ObjectMapper.Map(input, setting);
        await _repository.UpdateAsync(setting);
        return ObjectMapper.Map<ApplicationSetting, ApplicationSettingDto>(setting);
    }

    // TODO: Add proper authorization
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}

