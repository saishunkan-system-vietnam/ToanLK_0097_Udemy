using Amazon.S3;
using Amazon.S3.Model;
using BaseProject.Infrastructure;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.ResponseObject;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static GrapeCity.Enterprise.Data.DataEngine.ExpressionEvaluation.Eval;

namespace BaseProject.Core.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IAmazonS3 _s3Client;
        public ProductService(IUnitOfWork repository) : base(repository)
        {
            _unitOfWork = repository;
            //_s3Client = s3Client;
        }

        public List<Product> GetIncludeTechnology()
        {
            //var result = _unitOfWork.ProductRepository.get.FirstOrDefault();
            //_unitOfWork.DBSlaveContext.Entry(result).Collection(s => s.ProductTechnologies).Load();
            var result =_unitOfWork.ProductRepository.Get().ToList();
            return result;
        }

        public List<Product>? GetByQuantity(int quantity)
        {
            var result = _unitOfWork.ProductRepository.Get().Take(quantity).OrderByDescending((product) => product.CreateDate).ToList();
            return result;
        }

        public async Task<Product?> AddProduct(ProductInsertDto product)
        {
            Product newProduct = new Product()
            {
                Code = product.Code,
                CreateDate = DateTime.Now,
                Description = product.Description,
                Guarantee = product.Guarantee,
                Id = Guid.NewGuid(),
                Keyword = product.Keyword,
                Name = product.Name,
                Price = product.Price,
                ModifiedDate = DateTime.Now,
                TypeId = product.TypeId
                
            };
            var newProductId = newProduct.Id;
            //var bucketName = "toanlk";
            //var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            //if (!bucketExists) return null;

            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            foreach (var item in product.Images.Select((value, i) => new { i, value }))
            {
                var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                Byte[] bytes = Convert.FromBase64String(x);
                var stream = new MemoryStream(bytes);

                var key = newProductId + "_" +item.i;
                if(item.i == 0)
                {
                    imageskey += $"{key}.png";
                }
                else
                {
                    imageskey += $",{key}.png";
                }
                //var request = new PutObjectRequest()
                //{
                //    BucketName = bucketName!,
                //    Key = key!.ToString(),
                //    InputStream = stream,
                //};
                //request.Metadata.Add("Content-Type", file.ContentType);
                string filePath = $"{currentPath}\\{key}.png";
                File.WriteAllBytes(filePath, Convert.FromBase64String(x));
            }

            newProduct.Images = imageskey;
            _unitOfWork.ProductRepository.Add(newProduct);
            _unitOfWork.SaveChanges();
            newProduct.Description = "";
            return newProduct;
        }

        public async Task<List<Product>> GetByPage(int numberOfPage, int mountOfItem)
        {
            var productList = _unitOfWork.DBSlaveContext.Product.ToList().OrderBy(x=>x.Name).Skip((numberOfPage-1) * mountOfItem).Take(mountOfItem);
            return productList.ToList();
        }

        public async Task<bool> UpdateProduct(ProductUpdateDto modified)
        {
            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            string[] files = Directory.GetFiles(currentPath);
            foreach(var file in files)
            {
                if (File.Exists(file) && file.Contains(modified.product.Id.ToString()) 
                    && !modified.Images.Contains(file.Replace(currentPath + "\\",""))
                    )
                {
                    Console.WriteLine(file);
                    File.Delete(file);
                }
            }

            var currentMax = -1;
            foreach (var file in files)
            {
                if (file.Contains(modified.product.Id.ToString())){
                    var fileName = file.Replace($"{currentPath + "\\" + modified.product.Id}_", "");
                    var indexVal = int.Parse(fileName.Replace(".png",""));

                    if (indexVal > currentMax)
                    {
                        currentMax = indexVal;
                    }
                }
            }

            foreach (var item in modified.Images.Select((value, i) => new { i, value }))
            {
                
                if (!item.value.Contains(modified.product.Id.ToString()))
                {
                    var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                    Byte[] bytes = Convert.FromBase64String(x);
                    var stream = new MemoryStream(bytes);

                    var key = modified.product.Id + "_" + (currentMax + item.i);
                    if (item.i == 0)
                    {
                        imageskey += $"{key}.png";
                    }
                    else
                    {
                        imageskey += $",{key}.png";
                    }
                    string filePath = $"{currentPath}\\{key}.png";
                    File.WriteAllBytes(filePath, Convert.FromBase64String(x));
                }
                else
                {
                    if (item.i == 0)
                    {
                        imageskey += $"{item.value}";
                    }
                    else
                    {
                        imageskey += $",{item.value}";
                    }
                }
            }
            modified.product.Images = imageskey;

            foreach(var entity in _unitOfWork.DBMasterContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
            _unitOfWork.ProductRepository.Update(modified.product);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteRange(List<Guid?> guids){
            _unitOfWork.DBMasterContext.Product.RemoveRange(_unitOfWork.ProductRepository.GetByWhere(x => guids.Contains(x.Id)));
            _unitOfWork.SaveChanges();
            return true;
        }

        public ProductPagingByType GetByType (Guid typeId,int quantity,int page)
        {
            var count = _unitOfWork.ProductRepository.GetByWhere(p => p.TypeId == typeId).Count();
            if(count < quantity)
            {
                quantity = count;
            }
            var result = _unitOfWork.ProductRepository.GetByWhere(p => p.TypeId == typeId).OrderByDescending((item)=>item.CreateDate).Skip((quantity) * (page-1)).Take(quantity).ToList();
            int totalPageInt = count / (quantity == 0? 1 : quantity);
            int totalPage = totalPageInt + ((count % (quantity == 0 ? 1 : quantity) == 0) ? 0 : 1);
            var pagingResult = new ProductPagingByType()
            {
                Product = result,
                Count = totalPage,
                QuantityOfEachPage = quantity,
                CurrentPage = page
            }; 
            return pagingResult;
        }

        public Product GetById(Guid id)
        {
            var result = _unitOfWork.ProductRepository.GetByWhere(p => p.Id == id).FirstOrDefault();
            //var test = result.Type;
            //var result = _unitOfWork.ProductRepository.GetByWhere(p => p.Id == id).Include(t => t.Type).FirstOrDefault();
            return result;
        }

        public List<Product> GetRelatedProduct(string keyword)
        {
            return _unitOfWork.ProductRepository.GetByWhere((x) => EF.Functions.Like(x.Keyword, keyword)).Take(10).ToList();
        }
    }
}
