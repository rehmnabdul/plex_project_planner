using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Volo.Abp.Application.Dtos;

namespace Plex.ProjectPlanner.Configuration;

public class ApplicationSettingsTests : ProjectPlannerWebTestBase
{
    [Fact]
    public async Task ApplicationSettings_Should_Create_Setting()
    {
        // Arrange
        var createDto = new
        {
            key = "ArchiveRetentionDays",
            value = "30",
            description = "Number of days to keep completed tasks in archive"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/app/applicationSetting", createDto);

        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
        var result = await response.Content.ReadFromJsonAsync<ApplicationSettingDto>();
        result.ShouldNotBeNull();
        result.Key.ShouldBe("ArchiveRetentionDays");
        result.Value.ShouldBe("30");
    }

    [Fact]
    public async Task ApplicationSettings_Should_Get_Setting_By_Id()
    {
        // Arrange
        var createDto = new
        {
            key = "TestSetting",
            value = "TestValue",
            description = "Test description"
        };

        // Create setting first
        var createResponse = await Client.PostAsJsonAsync("/api/app/applicationSetting", createDto);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();
        var created = await createResponse.Content.ReadFromJsonAsync<ApplicationSettingDto>();

        // Act
        var getResponse = await Client.GetAsync($"/api/app/applicationSetting/{created!.Id}");

        // Assert
        getResponse.IsSuccessStatusCode.ShouldBeTrue();
        var result = await getResponse.Content.ReadFromJsonAsync<ApplicationSettingDto>();
        result.ShouldNotBeNull();
        result.Key.ShouldBe("TestSetting");
        result.Value.ShouldBe("TestValue");
    }

    [Fact]
    public async Task ApplicationSettings_Should_Update_Setting()
    {
        // Arrange
        var createDto = new
        {
            key = "UpdateTest",
            value = "OriginalValue",
            description = "Original description"
        };

        // Create setting first
        var createResponse = await Client.PostAsJsonAsync("/api/app/applicationSetting", createDto);
        createResponse.IsSuccessStatusCode.ShouldBeTrue();
        var created = await createResponse.Content.ReadFromJsonAsync<ApplicationSettingDto>();

        // Act
        var updateDto = new
        {
            key = "UpdateTest",
            value = "UpdatedValue",
            description = "Updated description"
        };
        var updateResponse = await Client.PutAsJsonAsync($"/api/app/applicationSetting/{created!.Id}", updateDto);

        // Assert
        updateResponse.IsSuccessStatusCode.ShouldBeTrue();
        var updated = await updateResponse.Content.ReadFromJsonAsync<ApplicationSettingDto>();
        updated.ShouldNotBeNull();
        updated.Value.ShouldBe("UpdatedValue");
        updated.Description.ShouldBe("Updated description");
    }

    [Fact]
    public async Task ApplicationSettings_Should_Support_Archive_Retention_Configuration()
    {
        // Arrange
        var archiveRetentionKey = "ArchiveRetentionDays";
        var archiveRetentionValue = "30";

        var createDto = new
        {
            key = archiveRetentionKey,
            value = archiveRetentionValue,
            description = "Number of days to keep completed tasks in archive before permanent deletion"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/app/applicationSetting", createDto);

        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
        var result = await response.Content.ReadFromJsonAsync<ApplicationSettingDto>();
        result.ShouldNotBeNull();
        result.Key.ShouldBe(archiveRetentionKey);
        int.Parse(result.Value).ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task ApplicationSettings_Should_List_All_Settings()
    {
        // Arrange
        // Create a test setting
        var createDto = new
        {
            key = "ListTestSetting",
            value = "ListTestValue",
            description = "Test for listing"
        };
        await Client.PostAsJsonAsync("/api/app/applicationSetting", createDto);

        // Act
        var response = await Client.GetAsync("/api/app/applicationSetting");

        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
        var result = await response.Content.ReadFromJsonAsync<PagedResultDto<ApplicationSettingDto>>();
        result.ShouldNotBeNull();
        result.Items.ShouldNotBeNull();
        result.TotalCount.ShouldBeGreaterThan(0);
    }
}

// DTOs for testing (these should match the actual DTOs we'll create)
public class ApplicationSettingDto : AuditedEntityDto<Guid>
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
}

