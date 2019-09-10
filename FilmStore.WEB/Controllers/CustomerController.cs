using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilmStore.WEB.Controllers
{
  [Authorize]
  public class CustomerController : Controller
  {
    private readonly IOrderService _orderService;
    public CustomerController(IOrderService orderService)
    {
      _orderService = orderService;
    }
    public IActionResult History()
    {
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var purchases = mapper.Map<IEnumerable<PurchaseDTO>, IEnumerable<PurchaseViewModel>>(_orderService.GetPurchases(HttpContext.User.Identity.Name));

      return View(purchases);
    }
  }
}
