using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net.Mail;
using CapgeminiElections.Email;
using CapgeminiElections.Classes;


namespace CapgeminiElections.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {

        }



        public async Task OnPostAsync()
        {
            
            var fname = Request.Form["fname"];
            var lname = Request.Form["lname"]
;           var dob = Request.Form["dob"];
            var number = Request.Form["phone"];
            var contact = Request.Form["contact"].ToString();
            var email = Request.Form["eaddress"];

            


            if(contact == "phone")
            {
                const string accountSid = "AC61c00512ecb005a992cb58094aaa2bd2";
                const string authToken = "7703d4ec17bb5817f40828875fc2ff09";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Welcome! Thank You " + fname + " " + lname + " for Registering with Capgemini Elections",
                    from: new Twilio.Types.PhoneNumber("+17062257031"),
                    to: new Twilio.Types.PhoneNumber("+1" + number)
                    );
            } else if(contact == "email")
            {

                MessageService message = new MessageService();

                await message.SendEmailAsync(
                    fromDisplayName: "Capgemini Elections",
                    fromEmailAddress: "CapgeminiElections@gmail.com",
                    toName: fname + " " + lname,
                    toEmailAddress: email,
                    subject: "Registration!",
                    message: "Welcome! Thank You for registering with Capgemini Elections");
            } else
            {
                const string accountSid = "AC61c00512ecb005a992cb58094aaa2bd2";
                const string authToken = "7703d4ec17bb5817f40828875fc2ff09";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Welcome! Thank You " + fname + " " + lname + " for Registering with Capgemini Elections",
                    from: new Twilio.Types.PhoneNumber("+17062257031"),
                    to: new Twilio.Types.PhoneNumber("+1" + number)
                    );

            }


            

        }
    }
}