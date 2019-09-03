using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmStore.WEB.Areas.Identity.Pages.Account
{
  public class ManageAccountModel : PageModel
  {
    private readonly IUserService _userService;

    public ManageAccountModel(IUserService userService)
    {
      _userService = userService;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    
    public class InputModel
    {
      [Display(Name = "User Name")]
      public string UserName { get; set; }

      [Display(Name = "First Name")]
      public string FirstName { get; set; }
      
      [Display(Name = "Last Name")]
      public string LastName{ get; set; }

      [Display(Name = "Birth date")]
      public int Day { get; set; }

      public int Month { get; set; }

      public int Year { get; set; }
    }

    [TempData]
    public string ErrorMessage { get; set; }
    
    public void OnGet(string firstName, string lastName, string birthDay)
    {
      Input = new InputModel();
      Input.FirstName = firstName;
      Input.LastName = lastName;
      var date = DateTime.Parse(birthDay);
      Input.Day = date.Day;
      Input.Month = date.Month;
      Input.Year = date.Year;
    }


    public async Task<IActionResult> OnPostAsync()
    {
      UserDTO user = new UserDTO { UserName = Input.UserName, Name = User.Identity.Name, Customer = new CustomerDTO() };
      DateTime date;
      if (DateTime.TryParseExact($"{Input.Year}.{Input.Month}.{Input.Day}", "yyyy.M.d", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
        user.Customer.BirthDay = date;
      else
      {
        ModelState.AddModelError(string.Empty, "Wrong date format");
        return Page();
      }
      user.Customer.Name = Input.UserName;
      user.Customer.FirstName = Input.FirstName;
      user.Customer.LastName = Input.LastName;

      var result = await _userService.EditUserAsync(user);
      if (result.Succeeded)
        return Redirect("~/Home");
      ModelState.AddModelError(string.Empty, result.Message);
      return Page();
    }
  }
}