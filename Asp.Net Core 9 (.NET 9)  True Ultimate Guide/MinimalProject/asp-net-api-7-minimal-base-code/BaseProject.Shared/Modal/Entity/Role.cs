using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
