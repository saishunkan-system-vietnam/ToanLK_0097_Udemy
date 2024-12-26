using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class News
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? AuthorId { get; set; }
        public string? Images { get; set; }
        public string? Tags { get; set; }
        //public DateTime? CreateDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }
}
