using Amazon.S3.Model;
using AutoMapper;
using BaseProject.Core.Mail;
using BaseProject.Core.Token;
using BaseProject.Infrastructure;
using BaseProject.Shared;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Dto;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.StringHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BaseProject.Core.Service
{
    public class NewsService : GenericService<News>, INewsService
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;
        public NewsService(IUnitOfWork repository, TokenService tokenService, IMailSender mailSender, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _unitOfWork = repository;
            _tokenService = tokenService;
            _mailSender = mailSender;
            _mapper =  mapper;
            _configuration = configuration;
        }

        public bool DeleteRange(List<Guid> listOfId)
        {
            _unitOfWork.DBMasterContext.News.RemoveRange(_unitOfWork.NewsRepository.GetByWhere(x => listOfId.Contains(x.Id)));
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool AddNews(NewsInsertDto news)
        {
            News newProduct = new News()
            {
                AuthorId = news.news.AuthorId,
                Content = news.news.Content,
                Id = Guid.NewGuid(),
                Tags = news.news.Tags,
                Title = news.news.Title,
                //CreateDate = DateTime.Now,
                //ModifiedDate = DateTime.Now
            };
            var newProductId = newProduct.Id;
            //var bucketName = "toanlk";
            //var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            //if (!bucketExists) return null;
            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            foreach (var item in news.images.Select((value, i) => new { i, value }))
            {
                var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                Byte[] bytes = Convert.FromBase64String(x);
                var stream = new MemoryStream(bytes);

                var key = newProductId + "_" + item.i;
                if (item.i == 0)
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
            _unitOfWork.NewsRepository.Add(newProduct);
            _unitOfWork.SaveChanges();
            newProduct.Content= "";
            return true;
        }


        public bool UpdateNews(NewsInsertDto news)
        {
            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            string[] files = Directory.GetFiles(currentPath);
            foreach (var file in files)
            {
                if (File.Exists(file) && file.Contains(news.news.Id.ToString())
                    && !news.images.Contains(file.Replace(currentPath + "\\", ""))
                    )
                {
                    Console.WriteLine(file);
                    File.Delete(file);
                }
            }

            var currentMax = -1;
            foreach (var file in files)
            {
                if (file.Contains(news.news.Id.ToString()))
                {
                    var fileName = file.Replace($"{currentPath + "\\" + news.news.Id}_", "");
                    var indexVal = int.Parse(fileName.Replace(".png", ""));

                    if (indexVal > currentMax)
                    {
                        currentMax = indexVal;
                    }
                }
            }

            foreach (var item in news.images.Select((value, i) => new { i, value }))
            {

                if (!item.value.Contains(news.news.Id.ToString()))
                {
                    var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                    Byte[] bytes = Convert.FromBase64String(x);
                    var stream = new MemoryStream(bytes);

                    var key = news.news.Id + "_" + (currentMax + item.i);
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
            news.news.Images = imageskey;

            _unitOfWork.NewsRepository.Update(news.news);
            _unitOfWork.SaveChanges();
            return true;
        }

    }
}
