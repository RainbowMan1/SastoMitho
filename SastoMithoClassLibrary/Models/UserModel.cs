using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public PrimaryAddress PrimaryAddress { get; }
        public SecondaryAddress SecondaryAddress { get; }
        public bool LivesInsideRingRoad { get; set; }
        
    }
}
