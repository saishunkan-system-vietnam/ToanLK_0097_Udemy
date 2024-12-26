using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class ProductInsertDto
    {
        public List<string> Images { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Guarantee { get; set; }
        public string Keyword { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Mounts { get; set; }
        public Guid TypeId { get; set; }
    }
}
