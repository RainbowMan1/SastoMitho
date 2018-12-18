using SastoMithoClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SastoMithoMVC.Models
{
    public class CartViewModel
    {
        public List<OrderedCartItemModel> Items { get; set; }
    }


}