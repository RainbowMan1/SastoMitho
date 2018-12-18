using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class CartItemsModel
    {
        public int ItemID { get; set; }
        public Guid CartID { get; set; }
        public int Quantity { get; set; }
    }
}
