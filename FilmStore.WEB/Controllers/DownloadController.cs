using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      IEnumerable<PurchaseDTO> purchasesDTO = _orderService.GetPurchases();
      var purchaseViewModels = mapper
        .Map<IEnumerable<PurchaseDTO>, IEnumerable<PurchaseViewModel>>(purchasesDTO);

      string purchasesJson = JsonConvert.SerializeObject(purchaseViewModels);
      byte[] bytes = Encoding.ASCII.GetBytes(purchasesJson);
      MemoryStream stream = new MemoryStream(bytes);
      return File(stream, "Application/octet-stream", "purchases.json");
    }
  }
}
