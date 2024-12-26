using BaseProject.Shared.Modal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class NewsInsertDto
    {
        public List<string> images { get; set; }
        public News news { get; set; }
    }
}
