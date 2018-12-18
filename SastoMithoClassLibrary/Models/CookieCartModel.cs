using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class CookieCartModel
    {
        public Guid CookieID { get; set; }
        public List<OrderedItemModel> OrderedItems { get;}
        public decimal CartTotalPrice { get;}
        public DateTime CartTerminationDate { get; set; }
        public bool ToBeTerminated { get; }
    }
}
