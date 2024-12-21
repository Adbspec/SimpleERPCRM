namespace ERP.Services.Interfaces
{
    /// <summary>
    /// Interface for permission service to check user permissions.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Checks if the current user has the specified permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns>True if the user has the permission, otherwise false.</returns>
        bool HasPermission(string permission);
    }
}
