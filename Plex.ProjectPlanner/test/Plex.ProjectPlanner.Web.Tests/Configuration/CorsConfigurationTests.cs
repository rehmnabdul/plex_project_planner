using System.Net.Http;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Plex.ProjectPlanner.Configuration;

public class CorsConfigurationTests : ProjectPlannerWebTestBase
{
    [Fact]
    public async Task API_Should_Allow_CORS_From_Flutter_Origins()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Options, "/api/v1/health-status");
        request.Headers.Add("Origin", "http://localhost:3000"); // Flutter web default
        request.Headers.Add("Access-Control-Request-Method", "GET");

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.Headers.ShouldContain(h => h.Key == "Access-Control-Allow-Origin");
    }

    [Fact]
    public async Task API_Should_Include_CORS_Headers_In_Response()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/health-status");
        request.Headers.Add("Origin", "http://localhost:3000");

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.Headers.ShouldContain(h => h.Key == "Access-Control-Allow-Origin");
    }

    [Fact]
    public async Task API_Should_Allow_All_Required_CORS_Methods()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Options, "/api/v1/health-status");
        request.Headers.Add("Origin", "http://localhost:3000");
        request.Headers.Add("Access-Control-Request-Method", "GET");
        request.Headers.Add("Access-Control-Request-Headers", "authorization,content-type");

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.Headers.ShouldContain(h => h.Key == "Access-Control-Allow-Methods");
        var allowMethods = response.Headers.GetValues("Access-Control-Allow-Methods");
        allowMethods.ShouldContain("GET");
        allowMethods.ShouldContain("POST");
        allowMethods.ShouldContain("PUT");
        allowMethods.ShouldContain("DELETE");
        allowMethods.ShouldContain("PATCH");
    }
}

