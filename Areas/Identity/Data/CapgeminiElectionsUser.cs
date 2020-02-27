using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CapgeminiElections.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CapgeminiElectionsUser class
    public class CapgeminiElectionsUser : IdentityUser
    {
        [PersonalData]
        public string ContactPreference { get; set; }
    }

    public class CapgeminiElectionsRole : IdentityRole
    {

    }
}
