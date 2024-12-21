using ERP.Controllers;
using ERP.Models;
using System.Security.Claims;

namespace ERP.AppCode
{
    public static class ClaimsPrincipalExtensions
    {
      
		/// <summary>
		/// Gets the user ID string from the ClaimsPrincipal.
		/// </summary>
		/// <param name="user">The ClaimsPrincipal representing the user.</param>
		/// <returns>A string containing the user ID, or an empty string if not found.</returns>
		public static string GetUserIdString(this ClaimsPrincipal user)
		{
			var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			return claim ?? "";
		}

	
		/// <summary>
		/// Retrieves the user's email from the claims principal.
		/// </summary>
		/// <param name="user">The claims principal representing the user.</param>
		/// <returns>The user's name.</returns>
		public static string? GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }


    }
}
