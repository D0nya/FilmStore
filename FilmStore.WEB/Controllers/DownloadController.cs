using FilmStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmStore.WEB.Controllers
{
  public class DownloadController : Controller
  {
    IOrderService _orderService;
    public DownloadController(IOrderService orderService)
    {
      _orderService = orderService;
    }
     
    public IActionResult DownloadPurchasesJson()
    {
      var stream = _orderService.GetPurchasesStream();
      return File(stream, "Application/octet-stream", "purchases.json");
    }
  }
}
