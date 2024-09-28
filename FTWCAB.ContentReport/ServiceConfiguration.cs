using FTWCAB.ContentReport.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FTWCAB.ContentReport;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureContentReport(
        this IServiceCollection services,
        Action<AuthorizationOptions> configureOptions)
    {
        var options = new AuthorizationOptions();
        configureOptions(options);

        if (options.AccessRoles?.Any() ?? false)
        {
            services.AddSingleton<IAuthorizationRolesProvider>(new AuthorizationRolesProvider 
            { 
                AccessRoles = options.AccessRoles,
            });
        }

        return services;
    }

    public class AuthorizationOptions
    {
        internal List<string>? AccessRoles { get; set; }

        public void SetAccessRoles(params string[] roles)
        {
            AccessRoles = roles.ToList();
        }
    }
}
