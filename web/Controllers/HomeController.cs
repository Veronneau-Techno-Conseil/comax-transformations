using CommunAxiom.Transformations.DAL.Models;
using CommunAxiom.Transformations.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly TransformationsDbContext _context;

        public HomeController(ILogger<HomeController> logger)//, TransformationsDbContext context)
        {
            //_context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Welcome");
        }

        public IActionResult Welcome()
        {
            return View("Welcome");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
