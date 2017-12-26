using InventoryManagement.Library.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Models
{
    public class CarModel
    {
        public long CarId { get; set; }

        [Required]
        [Display(Name = "Brand", ResourceType = typeof(Label))]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Model", ResourceType = typeof(Label))]
        public string Model { get; set; }

        public string Year { get; set; }

        public decimal? Price { get; set; }
        public bool New { get; set; }
    }
}