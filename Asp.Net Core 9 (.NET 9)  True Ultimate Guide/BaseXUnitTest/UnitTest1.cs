
using BaseProject.Core.Service;
using BaseProject.Infrastructure;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using Moq;

namespace BaseXUnitTest
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        public ProductServiceTest()
        {
            //var productInitData = new List<Product>();
            //// slave
            //DbContextMock<DBSlaveContext> dbSlaveContextMock = new DbContextMock<DBSlaveContext>(
            //    new DbContextOptionsBuilder<DBSlaveContext>().Options
            //    );
            //dbSlaveContextMock.CreateDbSetMock(temp => temp.Product ,productInitData);
            //DBSlaveContext dbSlaveContext = dbSlaveContextMock.Object;

            ////master
            //DbContextMock<DBMasterContext> dbMasterContextMock = new DbContextMock<DBMasterContext>(
            //    new DbContextOptionsBuilder<DBMasterContext>().Options
            //    );
            //dbMasterContextMock.CreateDbSetMock(temp => temp.Product, productInitData);
            //DBMasterContext dbMasterContext = dbMasterContextMock.Object;

            //var unitOfWork = new UnitOfWork(dbMasterContext, dbSlaveContext);
            //_productService = new ProductService(unitOfWork);

            //Mock<I> repositoryMock = new Mock<IEmployeeRepo>();
            //var mock = new Mock<IUnitOfWork>();
            //mock.Setup(x => x.DBMasterContext);
        }

        [Fact]
        public async Task AddPerson_ProperProductName()
        {
            //arrange
            ProductInsertDto request = new ProductInsertDto()
            {
                Name = "Pretty Watch"
            };

            // Act
             Product result = await _productService.AddProduct(request);
            List<Product> products = _productService.Get().ToList();

            // Assert
            Assert.True(result.Id != Guid.Empty);
            Assert.Contains(result, products);
        }
    }
}