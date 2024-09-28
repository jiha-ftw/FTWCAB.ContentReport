﻿using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;
using FTWCAB.ContentReport.Authorization;
using FTWCAB.ContentReport.Services;
using FTWCAB.ContentReport.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FTWCAB.ContentReport.Extensions
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [ModuleDependency(typeof(EPiServer.Shell.UI.InitializationModule))]
    public class InitializationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services
                .AddSingleton<IContentTypeGroupService, ContentTypeGroupService>()
                .AddSingleton<IContentTypeInstancesService, ContentTypeInstancesService>()
                .AddSingleton<IContentLoaderWrapper, ContentLoaderWrapper>()
                .AddSingleton<IContentInstanceUsageService, ContentInstanceUsageService>()
                .Configure<ProtectedModuleOptions>(o =>
                {
                    var moduleName = typeof(InitializationModule).Assembly.GetName().Name;

                    o.Items.Add(new ModuleDetails { Name = moduleName });
                });

            context.Services.AddAuthorization(authOptions =>
            {
                var serviceProvider = context.Services.BuildServiceProvider();
                var authorizationRolesProvider = serviceProvider.GetService<IAuthorizationRolesProvider>();

                authOptions.AddPolicy(
                    Constants.Autorization.PolicyName,
                    policy => policy.RequireRole(authorizationRolesProvider?.AccessRoles ?? ["CmsAdmins"]));
            });
        }

        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }
    }
}
