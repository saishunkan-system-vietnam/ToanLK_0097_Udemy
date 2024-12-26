using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class ProductUpdateDto
    {
        public Product product { get; set; }
        public List<String> Images { get; set; }

    }
}
