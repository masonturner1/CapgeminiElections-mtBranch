using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CapgeminiElections.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.Rest.Api.V2010.Account;




namespace CapgeminiElections.Areas.Identity.Pages.Account.Manage
{


    public partial class IndexModel : PageModel
    {

        
        private readonly UserManager<CapgeminiElectionsUser> _userManager;
        private readonly SignInManager<CapgeminiElectionsUser> _signInManager;

        public IndexModel(
            UserManager<CapgeminiElectionsUser> userManager,
            SignInManager<CapgeminiElectionsUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Contact Preference")]
            public string ContactPreference { get; set; }
        }

        private async Task LoadAsync(CapgeminiElectionsUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ContactPreference = user.ContactPreference
                
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
           

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }

                phoneNumber = await _userManager.GetPhoneNumberAsync(user);

                const string accountSid = "AC61c00512ecb005a992cb58094aaa2bd2";
                const string authToken = "7703d4ec17bb5817f40828875fc2ff09";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Thank You For Updating Your Phone Number",
                    from: new Twilio.Types.PhoneNumber("+17062257031"),
                    to: new Twilio.Types.PhoneNumber("+1" + phoneNumber)
                    );
            }

            //var cont = Request.Form["contact"].ToString();

            if (Input.ContactPreference != user.ContactPreference)
                user.ContactPreference = Input.ContactPreference;
            

            

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
