using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.LinkTable;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace BaseProject.Shared.Entity
{
    public class Product : BaseEntity
    {
        public Product(){}
        private ILazyLoader LazyLoader { get; set; }
        public Product(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        


        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Keyword { get; set; } = string.Empty;
        public int Guarantee { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Mounts { get; set; }
        public bool IsOutOfStock { get; set; } = false;
        public string Images { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionEmbedVideos { get; set; } = string.Empty;
        public string DescriptionImages { get; set; } = string.Empty;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<ProductTechnology> ProductTechnologies { get; set; }
        public Guid TypeId { get; set; }
        public virtual Shared.Modal.Entity.Type? Type{ get=> LazyLoader.Load(this, ref _type); set => _type = value; }
        private Shared.Modal.Entity.Type _type;

    }
}
