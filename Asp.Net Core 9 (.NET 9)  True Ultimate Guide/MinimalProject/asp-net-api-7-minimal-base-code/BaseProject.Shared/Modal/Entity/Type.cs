using BaseProject.Shared.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Entity
{
    public class Type : BaseEntity
    {
        private ICollection<Product> _products;
        public Type(){}
        public Type(ILazyLoader lazy)
        {
            _lazy = lazy;
        }
        private ILazyLoader _lazy { get; set; } 
        public string Name { get; set; }
        public string Images { get; set; }
        public bool isShowInLandingPage { get; set; }
        public virtual ICollection<Product>? Products{ get=>_lazy.Load(this, ref _products); set => _products = value; }
    }
}
