using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.Service
{
    public interface IProductService : IGenericService<Product>
    {
        public List<Product> GetIncludeTechnology();
        public Task<Product> AddProduct(ProductInsertDto product);
        public Task<List<Product>> GetByPage(int numberOfPage, int mountOfItem);
        public Task<bool> UpdateProduct(ProductUpdateDto modified);
        public bool DeleteRange(List<Guid?> guids);
        public ProductPagingByType GetByType(Guid typeId,int quantity,int page);
        public Product GetById(Guid typeId);
        public List<Product> GetRelatedProduct(string keyword);
        public List<Product>? GetByQuantity(int quantity);
    }
}
