using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class TrackOrderModel
    {
        public int TrackOrderStatus { get; set; }
        public string TrackOrderMessage { get; set; }
        public Guid OrderId { get; set; }
    }
}
