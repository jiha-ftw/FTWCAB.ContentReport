namespace FTWCAB.ContentReport.Authorization
{
    internal interface IAuthorizationRolesProvider
    {
        internal List<string>? AccessRoles { get; }
    }

    internal class AuthorizationRolesProvider : IAuthorizationRolesProvider
    {
        public required List<string>? AccessRoles { get; set; }
    }
}
