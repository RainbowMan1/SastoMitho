using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class OrderModel
    {
        public Guid OrderID { get; set; }
        public DateTime OrderIssued { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderPrepared { get; set; }
        public DateTime OrderCompleted { get; set; }
        public decimal OrderPrice { get; set; }
        public string DelivererPhoneNumber { get; set; }
        public Guid UserID { get; set; }


        public List<OrderedItemModel> OrderedItems { get; set; }
        public int NumberofItems { get; }
        public string AssignedDeliverer { get; }
    }
}
