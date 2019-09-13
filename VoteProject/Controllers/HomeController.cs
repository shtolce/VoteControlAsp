using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteProject.Models;
using VoteProject.Services;
using VoteProject.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace VoteProject.Controllers
{
    public class HomeController : Controller
    {
        private string _fillial;
        private UserContext _db;
        public HomeController(UserContext context)
        {
            _db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user!=null)
                {
                    await Authenticate(user.Login);
                    Response.Cookies.Append("fillialName", user.Login);
                }

            }
            return Redirect("/Home/Index");
        }


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendData([FromBody] VoteData data,[FromServices] IMessageSender mail)
        {
            if (Request.Cookies.ContainsKey("fillialName"))
                _fillial = Request.Cookies["fillialName"];
            else
                _fillial = "филлиал не установлен в настройках";
            _fillial = HttpUtility.UrlDecode(_fillial);
            mail.Send(data.FIO,data.phone,data.message,data.message,_fillial);

            return Json(data);
        }

        [HttpPost]
        public JsonResult SendDataOk([FromBody] VoteData data, [FromServices] IMessageSender mail)
        {
            if (Request.Cookies.ContainsKey("fillialName"))
                _fillial = Request.Cookies["fillialName"];
            else
                _fillial = "филлиал не установлен в настройках";
            _fillial = HttpUtility.UrlDecode(_fillial);

            mail.Send(data.FIO, data.phone, data.message, data.message,_fillial);

            return Json(data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
