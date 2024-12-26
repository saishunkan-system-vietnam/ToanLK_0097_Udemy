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
using GrapeCity.Enterprise.Data.Expressions.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BaseProject.Core.Service
{
    public class TypeService : GenericService<Shared.Modal.Entity.Type>, ITypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;
        public TypeService(IUnitOfWork repository, TokenService tokenService, IMailSender mailSender, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _unitOfWork = repository;
            _tokenService = tokenService;
            _mailSender = mailSender;
            _mapper =  mapper;
            _configuration = configuration;
        }

        public bool Update(TypeInsertDto type)
        {

            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            string[] files = Directory.GetFiles(currentPath);
            foreach (var file in files)
            {
                if (File.Exists(file) && file.Contains(type.type.Id.ToString())
                    && !type.Images.Contains(file.Replace(currentPath + "\\", ""))
                    )
                {
                    File.Delete(file);
                }
            }

            var currentMax = -1;
            foreach (var file in files)
            {
                if (file.Contains(type.type.Id.ToString()))
                {
                    var fileName = file.Replace($"{currentPath + "\\" + type.type.Id}_", "");
                    var indexVal = int.Parse(fileName.Replace(".png", ""));

                    if (indexVal > currentMax)
                    {
                        currentMax = indexVal;
                    }
                }
            }

            foreach (var item in type.Images.Select((value, i) => new { i, value }))
            {

                if (!item.value.Contains(type.type.Id.ToString()))
                {
                    var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                    Byte[] bytes = Convert.FromBase64String(x);
                    var stream = new MemoryStream(bytes);

                    var key = type.type.Id + "_" + (currentMax + item.i);
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
            type.type.Images = imageskey;

            _unitOfWork.TypeRepository.Update(type.type);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool AddType(TypeInsertDto type)
        {
            
            var addedType = new Shared.Modal.Entity.Type()
            {
                Id = Guid.NewGuid(),
                Name = type.type.Name,
            };


            var imageskey = "";
            var currentPath = Path.GetFullPath("wwwroot");

            foreach (var item in type.Images.Select((value, i) => new { i, value }))
            {
                var x = Regex.Replace(item.value, @"^data:image\/[a-zA-Z]+;base64,", "");
                Byte[] bytes = Convert.FromBase64String(x);
                var stream = new MemoryStream(bytes);

                var key = addedType.Id + "_" + item.i;
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
            addedType.Images = imageskey;
            _unitOfWork.TypeRepository.Add(addedType);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool UpdateFlagShow(TypeUpdateFlagShowDto updateDto)
        {
            _unitOfWork.TypeRepository.GetByWhere(p => p.Id == updateDto.Id).ExecuteUpdate(b => b.SetProperty(u => u.isShowInLandingPage, updateDto.IsShow));
            _unitOfWork.SaveChanges();
            return true;
        }
        public List<Shared.Modal.Entity.Type> GetByShowLandingPage()
        {
            var result = _unitOfWork.TypeRepository.GetByWhere(t => t.isShowInLandingPage).ToList();
            return result;
        }

    }
}
