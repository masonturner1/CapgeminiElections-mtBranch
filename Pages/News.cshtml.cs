using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CapgeminiElections
{
    public class NewsModel : PageModel
    {
        //Default search set to bernie
        public string Ref = "bernie";
        public class SampleModel
        {
            [Required]
            public string Search { get; set; }

        }
            
        [BindProperty]
        public SampleModel Input { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPostSearch()
        {
            if (!ModelState.IsValid)
                return Page();

            Ref = Input.Search;
            return Page();
        }
       
    }
}