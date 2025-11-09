using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Plex.ProjectPlanner.EntityFrameworkCore;
using Plex.ProjectPlanner.Web;
using Plex.ProjectPlanner.Web.Menus;
using Volo.Abp.AspNetCore.TestBase;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.UI.Navigation;

namespace Plex.ProjectPlanner;

[DependsOn(
    typeof(AbpAspNetCoreTestBaseModule),
    typeof(ProjectPlannerWebModule),
    typeof(ProjectPlannerApplicationTestModule),
    typeof(ProjectPlannerEntityFrameworkCoreTestModule)
)]
public class ProjectPlannerWebTestModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", false);
        builder.AddJsonFile("appsettings.secrets.json", true);
        context.Services.ReplaceConfiguration(builder.Build());

        context.Services.PreConfigure<IMvcBuilder>(builder =>
        {
            builder.PartManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(typeof(ProjectPlannerWebModule).Assembly));
        });

        context.Services.GetPreConfigureActions<OpenIddictServerBuilder>().Clear();
        PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
        {
            options.AddDevelopmentEncryptionAndSigningCertificate = true;
        });
        
        // Disable OpenIddict validation in tests - it causes redirects
        context.Services.GetPreConfigureActions<OpenIddictValidationBuilder>().Clear();
        
        // Configure test authentication before main module configures authentication
        context.Services.AddAuthentication("Test")
            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", options => { });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Allow all authorization checks to pass in tests
        context.Services.AddAlwaysAllowAuthorization();
        
        // Configure authorization to allow anonymous access in tests
        context.Services.Configure<AuthorizationOptions>(options =>
        {
            // Set default policy to allow anonymous
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAssertion(_ => true)
                .Build();
            options.FallbackPolicy = null; // No fallback policy - allow anonymous
        });
        
        // Make Test the default scheme (authentication was added in PreConfigureServices)
        context.Services.Configure<AuthenticationOptions>(options =>
        {
            options.DefaultScheme = "Test";
            options.DefaultAuthenticateScheme = "Test";
            options.DefaultChallengeScheme = "Test";
        });
        
        ConfigureLocalizationServices(context.Services);
        ConfigureNavigationServices(context.Services);
    }
    
    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        // Override authentication configuration after main module configures it
        context.Services.Configure<AuthenticationOptions>(options =>
        {
            options.DefaultScheme = "Test";
            options.DefaultAuthenticateScheme = "Test";
            options.DefaultChallengeScheme = "Test";
        });
    }
    
    // Test authentication handler that always succeeds
    private class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, System.Text.Encodings.Web.UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
                new Claim(ClaimTypes.Name, "test-user")
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // Don't redirect, return 401
            Response.StatusCode = 401;
            return Task.CompletedTask;
        }
    }

    private static void ConfigureLocalizationServices(IServiceCollection services)
    {
        var cultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("tr") };
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;
        });
    }

    private static void ConfigureNavigationServices(IServiceCollection services)
    {
        services.Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ProjectPlannerMenuContributor());
        });
    }
}
