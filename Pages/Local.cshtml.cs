using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CapgeminiElections.Areas.Identity.Data;
using Google.Apis.CivicInfo.v2;
using Google.Apis.CivicInfo;
using Google.Apis.Discovery;
using RestSharp;
using Newtonsoft.Json;
using Google.Apis.Services;

namespace CapgeminiElections
{
    public class LocalModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
