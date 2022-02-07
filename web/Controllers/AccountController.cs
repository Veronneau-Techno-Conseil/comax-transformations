using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Forbidden", "Login");
        }
    }
}