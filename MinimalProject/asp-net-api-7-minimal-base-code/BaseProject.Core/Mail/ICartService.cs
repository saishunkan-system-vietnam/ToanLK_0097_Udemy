using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;

namespace BaseProject.Core.Service
{
    public interface ICartService : IGenericService<Cart>
    {
        public bool InsertIntoCart(Guid accountId, Guid productId, int quantity);
        public List<Cart> GetProductByCart(Guid accountId);
        public bool DeleteCartById(Guid cartId);
        public bool ChangeSelectedById(Guid cartId);
    }
}
