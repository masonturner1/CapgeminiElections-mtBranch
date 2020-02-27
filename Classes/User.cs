using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapgeminiElections.Classes
{
    public class User
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string dob { get; set; }
        public string contact { get; set; }

        public User(string _name, string _phoneNumber, string _dob, string _contact)
        {
            name = _name;
            phoneNumber = _phoneNumber;
            dob = _dob;
            contact = _contact;

        }
    }
}
