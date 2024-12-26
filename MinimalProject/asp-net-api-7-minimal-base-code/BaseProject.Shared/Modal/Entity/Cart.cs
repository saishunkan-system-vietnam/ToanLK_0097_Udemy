using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class Cart : BaseEntity
    {
        public Cart(){}
        public Guid AccountId { get; set; }
        public Guid ProductId { get; set; } 
        public int Mount { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Account? Account { get; set; }
        public string Status { get; set; }
        public bool DelFlag { get; set; }
        public bool Selected { get; set; }
    }
}
