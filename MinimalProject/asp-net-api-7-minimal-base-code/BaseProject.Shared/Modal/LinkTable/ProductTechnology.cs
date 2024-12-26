using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.LinkTable
{
    public class ProductTechnology
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid TechnologyId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Technology? Technology { get; set; }
    }
}
