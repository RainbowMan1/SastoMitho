using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class UserCartModel
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public decimal CartTotalPrice { get;}
    }
}
