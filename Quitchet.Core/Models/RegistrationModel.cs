using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models
{
    public class RegistrationModel
    {
        public string ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PostalCode { get; set; }
    }
}
