using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using CapgeminiElections.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CapgeminiElections.Email;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CapgeminiElections.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CapgeminiElectionsUser> _signInManager;
        private readonly UserManager<CapgeminiElectionsUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<CapgeminiElectionsUser> userManager,
            SignInManager<CapgeminiElectionsUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Contact Preference")]
            public string ContactPreference { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }




        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new CapgeminiElectionsUser { UserName = Input.Email, Email = Input.Email, ContactPreference = Input.ContactPreference, PhoneNumber = Input.PhoneNumber};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                  
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);


                        MessageService message = new MessageService();

                        await message.SendEmailAsync(
                            fromDisplayName: "Capgemini Elections",
                            fromEmailAddress: "CapgeminiElections@gmail.com",
                            toName: "",
                            toEmailAddress: Input.Email,
                            subject: "Registration!",
                            message: "Welcome! Thank You for registering with Capgemini Elections. Please Confirm Your Account Here: \n\n" + HtmlEncoder.Default.Encode(callbackUrl));
                    

                        if(Input.ContactPreference == "phone")
                    {
                        const string accountSid = "AC61c00512ecb005a992cb58094aaa2bd2";
                        const string authToken = "7703d4ec17bb5817f40828875fc2ff09";

                        TwilioClient.Init(accountSid, authToken);

                        var messages = MessageResource.Create(
                            body: "Welcome! Thank You for Registering with Capgemini Elections. Please check your email for a confirmation.",
                            from: new Twilio.Types.PhoneNumber("+17062257031"),
                            to: new Twilio.Types.PhoneNumber("+1" + Input.PhoneNumber)
                            );

                    }


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
