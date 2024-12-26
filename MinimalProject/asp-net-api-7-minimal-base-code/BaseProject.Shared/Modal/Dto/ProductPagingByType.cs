using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class ProductPagingByType
    {
        public List<Product>? Product { get; set; } = new List<Product>();
        public int? Count { get; set; }
        public int? CurrentPage { get; set; }
        public int? QuantityOfEachPage { get; set; }

    }
}
