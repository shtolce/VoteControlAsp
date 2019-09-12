using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using VoteProject.Models;
using VoteProject.Services;

namespace VoteProject.Controllers
{
    public class HomeController : Controller
    {
        private string _fillial;

        [HttpGet]
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
