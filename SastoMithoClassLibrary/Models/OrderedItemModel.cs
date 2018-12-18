using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class OrderedItemModel: ItemModel
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
