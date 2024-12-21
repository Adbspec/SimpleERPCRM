using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Net.Mail;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace ERP.AppCode
{
	public class MiscFunctions
	{
		

		/// <summary>
		/// Hashes the given password using bcrypt.
		/// </summary>
		/// <param name="ClearPass">The clear text password to hash.</param>
		/// <returns>The hashed password.</returns>
		public static string HashedPassword(string ClearPass)
		{

			string EncryptedPass = "";
			try
			{
				EncryptedPass = BCrypt.Net.BCrypt.HashPassword(ClearPass);
			}
			catch
			{
				return "";
			}
			return EncryptedPass;
		}

		/// <summary>
		/// Verifies whether the provided user input password matches the stored hashed password.
		/// </summary>
		/// <param name="userInputPassword">The password provided by the user for verification.</param>
		/// <param name="storedHashedPassword">The hashed password stored in the database.</param>
		/// <returns>True if the user input password matches the stored hashed password, otherwise false.</returns>
		public static bool VerifyPassword(string userInputPassword, string storedHashedPassword)
		{
			// Compare the user input password with the stored hashed password
			return BCrypt.Net.BCrypt.Verify(userInputPassword, storedHashedPassword);
		}

	}
}
