using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FilmStore.BLL.DTO;
using FilmStore.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FilmStore.WEB.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class RegisterModel : PageModel
  {
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IUserService _userService;

    public RegisterModel(
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        IUserService userService)
    {
      _logger = logger;
      _emailSender = emailSender;
      _userService = userService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public class InputModel
    {
      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [Display(Name = "User Name")]
      public string UserName { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }
    }

    public void OnGet(string returnUrl = null)
    {
      ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");
      if (ModelState.IsValid)
      {
        var user = new UserDTO
        { UserName = Input.UserName, Email = Input.Email,
          Customer = new CustomerDTO() { Name = Input.UserName},
          Password = Input.Password, Role = "user", Name = Input.UserName};

        var result = await _userService.Create(user);
        if (result.Succeeded)
        {
          _logger.LogInformation("User created a new account with password.");

          user.Id = await _userService.GetUserIdAsync(user);
          var code = await _userService.CreateEmailToken(user);
          var callbackUrl = Url.Page(
              "/Account/ConfirmEmail",
              pageHandler: null,
              values: new { userId = user.Id, code = code },
              protocol: Request.Scheme);

          await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
              $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          return LocalRedirect(returnUrl);
        }
        ModelState.AddModelError(string.Empty, result.Message);
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}