using BaseProject.Shared.Modal.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class Setting 
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int DisOrder { get; set; }

    }
}
