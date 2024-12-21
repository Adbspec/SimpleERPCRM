using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ERP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERP.AppCode;
using ERP.Dto; 
namespace ERP.Controllers
{
    public class AuthController : Controller
    {
        private readonly DBContext _dBContext;

        public AuthController(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated) return RedirectToAction("ViewCustomer", "Crm");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthDto authDto, string? ReturnUrl)
        {
            IActionResult RedirectToLogin() => RedirectToAction("Login", "Auth");
            try
            {
                User? user = await _dBContext.Users.FirstOrDefaultAsync(u=>u.UserEmail== authDto.LoginId || u.UserId==authDto.LoginId);

                if(user ==null)
                {
                    TempData["ValidationMessage"] = "No User Found";
                    return RedirectToLogin();
                }

                string hash = user.PasswordHash;
                if (!MiscFunctions.VerifyPassword(authDto.Password, hash)) //incorrect passsword
                {
                    TempData["ValidationMessage"] = "Invalid Password";
                    return RedirectToLogin();
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                };


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = false,
                    IsPersistent = user.PeristantLogin
                };

                user.PeristantLogin = authDto.PersistantLogin;
                await _dBContext.SaveChangesAsync(true);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl)) return Redirect(ReturnUrl);
                return RedirectToLogin();
            }

            catch (Exception ex)
            {
               
                return RedirectToLogin();
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                string userId = User.GetUserIdString();
              
                return RedirectToAction("Login", "Auth");
            }
            catch
            {
                return RedirectToAction("Login", "Auth");
            }

        }

       
    }
}
