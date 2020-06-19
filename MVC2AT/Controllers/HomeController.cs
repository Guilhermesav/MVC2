using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC2AT.Models;

namespace MVC2AT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<HomeController> logger)
            
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("cookieTest", out var cookieTest);
            var bibliotecaTokenExists =
                _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("bibliotecaToken",
                    out var bibliotecaToken);

            ViewBag.CookieTest = cookieExists ? cookieTest : "Cookie not found!";
            ViewBag.BibliotecaToken = bibliotecaTokenExists ? bibliotecaToken : "Cookie bibliotecaToken not found!";

            var options = new CookieOptions();
            options.HttpOnly = true;
            options.Secure = true;
            options.SameSite = SameSiteMode.Strict;
            options.MaxAge = TimeSpan.FromMinutes(2);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("cookieTest");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("cookieTest", $"CookieTestValue [{Guid.NewGuid()}]", options);

            return View();
        }



        public IActionResult CidadeModels()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
