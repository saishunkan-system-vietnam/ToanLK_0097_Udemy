using Amazon.S3;
using Amazon.S3.Model;
using BaseProject.API.Auth;
using BaseProject.API.ServiceHelper;
using BaseProject.Core.Mail;
using BaseProject.Core.Service;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.ResponseObject;
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Agreement.JPake;
using System.Data;
using System.Text.RegularExpressions;

namespace BaseProject.API.Module
{
    public class ProductModule : IModule
    {

        public IServiceCollection RegisterModule(IServiceCollection builder)
        {
            return builder;
        }

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var product = endpoints.MapGroup("/product")
                        .WithTags("Product").AddEndpointFilter<LoggerFilter>();

            product.MapGet("", GetAllProduct).Produces<bool>(200);
            product.MapGet("/{id}", GetById);
            product.MapGet("/send-mail", TestSendingEmail).Produces<bool>(200);
            product.MapGet("/upload-file", uploadFile);
            product.MapGet("/{numberOfPage}/{mountOfItems}", GetByPage);
            product.MapGet("/get-new",GetNewProductList);
            product.MapGet("/get-by-type/{id}/{quantity}/{page}", GetByType);
            product.MapGet("/get-related/{keyword}", GetRelatedProduct); 

            product.MapPut("", Update);
            product.MapPost("/delete", DeleteRange);
            product.MapDelete("/{id}", DeleteProduct);
            product.MapPost("/add", AddProductNewTech);
            product.MapPost("/newest/{quantity}", GetByQuantity);

            product.MapGet("/cache", GetAllProduct).Produces<bool>(200);


            return endpoints;
        }
        public async Task<Response> AddProductNewTech(ProductInsertDto product, [FromServices] IProductService productService)
        {
            var result = await productService.AddProduct(product);
            if(result == null)
            {
                return new E<object>()
                {
                    Code = 500,
                    Message = ResponseMessage.SERVER_ERROR
                };
            }
            else
            {
                return new R<Product>()
                {
                    Code = 200,
                    Message = ResponseMessage.RESPONSE_SUCCESS,
                    Payload = result
                };
            }
        }

        //public R<List<Product>> GetByMountsOfSelling(int quantity, [FromServices] IProductService productService)
        //{
        //    productService.Get().OrderByDescending((product) => product.)
        //}

        public R<List<Product>>? GetByQuantity(int quantity, [FromServices] IProductService productService) 
        {
            var result = productService.GetByQuantity(quantity);
            return new R<List<Product>>()
            {
                Payload = result
            };
        }

        public R<Product?> GetById(Guid id, [FromServices] IProductService productService)
        {
            var result = productService.GetById(id);

            return new R<Product?>()
            {
                Payload = result
            };
        }

        public async Task<List<Product>> GetByPage(int numberOfPage, int mountOfItems, [FromServices] IProductService productService)
        {
            return await productService.GetByPage(numberOfPage, mountOfItems);
        }

        public Response GetAllProduct([FromServices] IProductService productService)
        {
            var productList =  productService.GetIncludeTechnology();

            return new R<List<Product>>()
            {
                Code = 200,
                Message = ResponseMessage.RESPONSE_SUCCESS,
                Payload = productList,
            };
        }
        
        public Response DeleteProduct(Guid id, [FromServices] IProductService productService)
        {
            var result = productService.Delete(x => x.Id == id);
            return new R<bool>()
            {
                Code = 200,
                Message = ResponseMessage.RESPONSE_SUCCESS,
                Payload = result,
            };
        }

        public void TestSendingEmail([FromServices] IMailSender mailSender) {
            var directory = Directory.GetCurrentDirectory();
            string context = File.ReadAllText(directory + "\\..\\BaseProject.Core\\Mail\\MailTemplate\\ValidateEmail.html");
            Message a = new Message(
                new string[] {"khanhtoan.le.6555@gmail.com"},
                "this is the email subject",
               context,
                "toan");
            mailSender.SendEmail(a,true);
        }
        
        public async Task uploadFile(IFormFile file, [FromServices] IAmazonS3 _s3Client)
        {
            var bucketName = "toanlk";
            var prefix = "test";
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return;

            var key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}";
            var inputStream = file.OpenReadStream();
            var request = new PutObjectRequest()
            {
                BucketName = bucketName!,
                Key = key!,
                InputStream = inputStream!,
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return;
        }

        public async Task<bool> Update(ProductUpdateDto productUpdateDto, [FromServices] IProductService productService)
        {
            return await productService.UpdateProduct(productUpdateDto);
        }

        public R<object> DeleteRange(List<Guid?> guids, [FromServices] IProductService productService)
        {
            var result = productService.DeleteRange(guids);
            return new R<object>()
            {
                Payload = result
            };
        }

        //new product list (max 8 items)
        public R<List<Product>> GetNewProductList([FromServices] IProductService productService)
        {
            var result = productService.Get().OrderByDescending(p => p.CreateDate).Take(10).ToList();
            var typeOfFirstProduct = result[0].Type;
            var returnVal = result ?? new List<Product>();
            return new R<List<Product>>()
            {
                Payload = returnVal
            };
        }

        public R<ProductPagingByType> GetByType(Guid id,int quantity,int page, [FromServices] IProductService productService)
        {   
            var result = productService.GetByType(id, quantity,page);
            return new R<ProductPagingByType>()
            {
                Payload = result
            };
        }

        public R<List<Product>> GetRelatedProduct(string keyword, [FromServices] IProductService productService)
        {
            var result = productService.GetRelatedProduct(keyword);
            return new R<List<Product>>()
            {
                Payload = result
            };
        }
    }
}
