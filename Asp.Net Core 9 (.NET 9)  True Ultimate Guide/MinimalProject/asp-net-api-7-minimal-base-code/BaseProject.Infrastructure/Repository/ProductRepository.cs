
using BaseProject.Shared.Entity;

namespace BaseProject.Infrastructure
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(DBMasterContext dbmasterContext, DBSlaveContext dbSlaveContext) : base (dbmasterContext, dbSlaveContext)
        {

        }
    }
}
