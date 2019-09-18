using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
    public IActionResult History(int page = 1)
    {
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var count = _orderService.GetPurchases(name: HttpContext.User.Identity.Name).Count();
      var purchases = mapper.Map<IEnumerable<PurchaseDTO>, IEnumerable<PurchaseViewModel>>(_orderService.GetPurchases(page, 7, name: HttpContext.User.Identity.Name));
      var table = new TableViewModel<PurchaseViewModel>
      {
        PageViewModel = new Models.TableLogic.PageViewModel(count, page, 7),
        Items = purchases
      };
      return View(table);
    }
  }
}
