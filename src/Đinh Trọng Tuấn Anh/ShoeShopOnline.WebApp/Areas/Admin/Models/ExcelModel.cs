using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShopOnline.WebApp.Areas.Admin.Models
{
    public class ExcelModel
    {
        public string Customer { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset CreatedOn { get; set; }

        

    }
}