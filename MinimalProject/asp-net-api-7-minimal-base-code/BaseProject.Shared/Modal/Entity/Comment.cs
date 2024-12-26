using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class Comment :BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid AccountId { get; set; }
        public int Stars { get; set; }
        public string Title { get; set; }
        public string CommentText { get; set; }

    }
}
