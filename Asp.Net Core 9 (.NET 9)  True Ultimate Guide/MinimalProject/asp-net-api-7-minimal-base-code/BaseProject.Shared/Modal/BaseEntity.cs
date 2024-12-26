using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Entity
{
    public class BaseEntity
    {
        [Key]
        public System.Guid? Id { get; set; } = System.Guid.NewGuid();
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
