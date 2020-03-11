using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using acaShare.BLL.Models;
using acaShare.WebAPI.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace acaShare.WebAPI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [StringLength(22, ErrorMessage = "Pole {0} musi mieć przynajmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
            [Display(Name = "Nazwa użytkownika")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [EmailAddress(ErrorMessage = "Pole {0} nie jest prawidłowym aresem e-mail.")]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [StringLength(100, ErrorMessage = "Pole {0} musi mieć przynajmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [DataType(DataType.Password)]
            [Display(Name = "Potwierdź hasło")]
            [Compare("Password", ErrorMessage = "Wpisane hasła nie są takie same.")]
            public string ConfirmPassword { get; set; }
            
            [Required(ErrorMessage = "Aby założyć konto konieczna jest akceptacja regulaminu!")]
            public bool AcceptRegulations { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!Input.AcceptRegulations)
            {
                ModelState.AddModelError("AcceptRegulations", "Aby założyć konto konieczna jest akceptacja regulaminu!");
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Create application user
                    var acaShareUser = new User(user.Id, Input.UserName, DateTime.Today);
                    _userService.RegisterNewUser(acaShareUser);
                    // End create application user

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _userManager.AddToRoleAsync(user, Roles.MemberRole);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
