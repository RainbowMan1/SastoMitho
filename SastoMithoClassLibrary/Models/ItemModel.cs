using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoClassLibrary.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }
        public string Tag { get; set; }
        public bool CannotbeOrdered { get; set; }
        public decimal CostOfProduction { get; set; }
        public decimal  RunningCost { get; set; }
        public decimal AverageMarketPrice { get; set; }

    }
}
