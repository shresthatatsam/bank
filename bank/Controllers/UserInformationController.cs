using bank.Models.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bank.Controllers
{
    public class UserInformationController : Controller
    {
        private readonly IUserInformation _userInformation;

        public UserInformationController(IUserInformation userInformation)
        {
            _userInformation = userInformation;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(IFormCollection frm)
        {

            var userName = frm["user_name"].ToString();
            var password = frm["password"].ToString();
            var role = frm["role"].ToString();
            _userInformation.Model.user_name = userName;
            _userInformation.Model.password = password;
            _userInformation.Model.role = role;

            _userInformation.Register();
            return View(frm);
        }

        [HttpPost]
        public IActionResult Login(IFormCollection frm)
        {
            var userName = frm["user_name"].ToString();
            var password = frm["password"].ToString();

            var isAuthenticated = _userInformation.Login(userName, password);
            if (isAuthenticated)
            {
                var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                TempData["SuccessMessage"] = "Login successful.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid UserName Or Password.";
                //ModelState.AddModelError("", "Invalid username or password.");
                return RedirectToAction("Index", "UserInformation");
            }
        }

    }
}
