using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SastoMithoMVC.Models
{
    public class AddtoCartViewModel
    {
        [Range(1,int.MaxValue)]
        public int Id { get; set; }

        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
    
}