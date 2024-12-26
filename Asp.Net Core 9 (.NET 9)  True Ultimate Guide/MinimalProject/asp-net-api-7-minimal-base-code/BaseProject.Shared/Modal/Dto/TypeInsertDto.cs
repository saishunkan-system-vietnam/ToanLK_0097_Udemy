using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class TypeInsertDto 
    {
        public Shared.Modal.Entity.Type type { get; set; }
        public List<string> Images { get; set; }
    }
}
