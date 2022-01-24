using CommunAxiom.Transformations.AppModel.Repositories;
using CommunAxiom.Transformations.DAL.Models;
using CommunAxiom.Transformations.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<LoginController> _logger;

        private readonly TransformationsDbContext _context;

        private readonly IUserRepository _userRepository;

        public LoginController(ILogger<LoginController> logger, TransformationsDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OIDC()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "https://localhost:5001/Home/Welcome"
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }
        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult Updatedatabasecall()
        {
            string username = HttpContext.User.Identity.Name;
            _userRepository.UpdateDatabase(username);
            return RedirectToAction("Welcome", "Home");
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = "https://localhost:5001/Login/index.html"
            });
        }
    }
}
