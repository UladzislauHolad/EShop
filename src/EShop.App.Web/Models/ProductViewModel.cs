using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        //[Required(ErrorMessage = "Please enter product name")]
        //[StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Please enter product price")]
        //[DataType(DataType.Currency)]
        //[Range(0.01, 999, ErrorMessage = "Must be with range from 0.01 to 999")]
        public decimal Price { get; set; }
        //[Required(ErrorMessage = "Please enter product description")]
        //[StringLength(50, MinimumLength = 2)]
        public string Description { get; set; }
    }
}
