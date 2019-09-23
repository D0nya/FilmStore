using FilmStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmStore.WEB.Controllers
{
  public class LogController : Controller
  {
    private readonly IUserService _userService;
    public LogController(IUserService userService)
    {
      _userService = userService;
    }

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
    public IActionResult Register()
    {
      return Redirect("~/Identity/Account/Register");
    }

    public IActionResult Manage()
    {
      var user = _userService.GetUserByNameAsync(User.Identity.Name).Result;
      if (user == null)
        return Redirect("~/Home");

      string urlWithData = $"~/Identity/Account/ManageAccount/?firstName={user.Customer.FirstName}&lastName={user.Customer.LastName}&birthDay={user.Customer.BirthDay.ToShortDateString()}";
      return Redirect(urlWithData);
    }
  }
}