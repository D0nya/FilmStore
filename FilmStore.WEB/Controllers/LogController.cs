using Microsoft.AspNetCore.Mvc;

namespace FilmStore.WEB.Controllers
{
  public class LogController : Controller
  {
    public IActionResult Login()
    {
      return Redirect("~/Identity/Account/Login");
    }
    public IActionResult LogOut()
    {
      if (HttpContext.User.Identity.IsAuthenticated)
      {
        return Redirect("~/Identity/Account/Logout");
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }
  }
}