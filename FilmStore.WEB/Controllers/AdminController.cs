using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using FilmStore.DAL.Entities;
using FilmStore.WEB.Models;
using FilmStore.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStore.WEB.Controllers
{
  [Authorize(Roles="admin")]
  public class AdminController : Controller
  {
    private readonly IOrderService _orderService;
    private readonly IAdminService _adminService;
    private readonly IEmailSender _emailSender;
    private readonly IEnumerable<SelectListItem> genres;
    private readonly IEnumerable<SelectListItem> producers;
    private readonly IEnumerable<SelectListItem> countries;
    public AdminController(IOrderService orderService, IAdminService adminService, IEmailSender emailSender)
    {
      _orderService = orderService;
      _adminService = adminService;
      _emailSender = emailSender;

      genres = _adminService.GetGenres().Select(g => new SelectListItem(g.Name, g.Id.ToString()));
      producers = _adminService.GetProducers().Select(p => new SelectListItem(p.Name, p.Id.ToString()));
      countries = _adminService.GetCountries().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
    }
    public IActionResult Admin()
    {
      ViewBag.Genres = genres;
      ViewBag.Countries = countries;
      return View();
    }
    public async Task<ViewResult> Edit(int Id)
    {
      var film = await _orderService.GetFilm(Id);
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();

      var filmViewModel = mapper.Map<FilmDTO, FilmViewModel>(film);

      ViewBag.Genres = genres;
      ViewBag.Producers = producers;
      ViewBag.Countries = countries;
      return View(filmViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FilmViewModel filmViewModel)
    {
      var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
      if (ModelState.IsValid)
      {
        var filmDTO = mapper.Map<FilmViewModel, FilmDTO>(filmViewModel);
        await _adminService.SaveFilm(filmDTO);
        TempData["message"] = $"Changes in film {filmViewModel.Name} were saved successfully.";
        return RedirectToAction("Admin");
      }
      else
        return RedirectToAction("Edit");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeQuantity(FilmViewModel film)
    {
      var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
      var filmDTO = mapper.Map<FilmViewModel, FilmDTO>(film);
      await _adminService.ChangeQuantityInStock(filmDTO);
      TempData["message"] = $"{film.Name} quantity set to {film.QuantityInStock}.";
      return RedirectToAction("Admin");
    }

    public ViewResult AddFilm()
    {
      ViewBag.Genres = genres;
      ViewBag.Producers = producers;
      ViewBag.Countries = countries;
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddFilm(FilmViewModel filmViewModel)
    {
      if (ModelState.IsValid)
      {
        var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
        var filmDTO = mapper.Map<FilmViewModel, FilmDTO>(filmViewModel);
        await _adminService.SaveFilm(filmDTO);
        TempData["message"] = $"Film {filmViewModel.Name} was added successfully.";
        return RedirectToAction("Admin");
      }
      else
        return RedirectToAction("AddFilm");
    }

    public async Task<IActionResult> DeleteFilm(int id)
    {
      string filmName = (await _adminService.GetFilm(id)).Name;
      await _adminService.DeleteFilm(id);
      TempData["message"] = $"Film {filmName} was deleted successfully.";
      return RedirectToAction("Admin");
    }

    public IActionResult Purchases(string searchString, int page = 1)
    {
      var mapper = MapperService.CreateFilmDTOToFilmViewModelMapper();
      var purchases = mapper.Map<IEnumerable<PurchaseDTO>, IEnumerable<PurchaseViewModel>>(_orderService.GetPurchases(page, 7, searchString));
      var count = _orderService.GetPurchases(searchString: searchString).Count();
      ViewBag.Status = new List<SelectListItem>
      {
        new SelectListItem("Pending", Status.Pending.ToString()),
        new SelectListItem("Confirmed", Status.Confirmed.ToString()),
        new SelectListItem("Rejected", Status.Rejected.ToString())
      };
      var items = new TableViewModel<PurchaseViewModel>
      {
        PageViewModel = new Models.TableLogic.PageViewModel(count, page, 7),
        Filter = new Models.TableLogic.SearchViewModel(),
        Items = purchases
      };
      items.Filter.SearchString = searchString;
      return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPurchase(PurchaseViewModel purchase)
    {
      var mapper = MapperService.CreateFilmViewModelToFilmDTOMapper();
      PurchaseDTO purchaseDTO = mapper.Map<PurchaseViewModel, PurchaseDTO>(purchase);

      await _adminService.SavePurchase(purchaseDTO);
      TempData["message"] = $"Purchase #{purchase.Id}. Status: {purchase.Status}.";

      if(purchase.Status != Status.Pending)
       await _emailSender.SendEmailAsync(purchase.Customer.User.Email, "Purchase status", $"Your purchase #{purchase.Id} was {purchase.Status}");

      return RedirectToAction("Purchases");
    }
  }
}
