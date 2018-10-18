using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.CategoriesViewModels
{
    public class CategoryTableViewModel
    {
        public int Length { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
