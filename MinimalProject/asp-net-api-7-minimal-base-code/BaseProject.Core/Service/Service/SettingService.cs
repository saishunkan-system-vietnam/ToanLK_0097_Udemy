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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BaseProject.Core.Service
{
    public class SettingService : GenericService<Setting>, ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;
        public SettingService(IUnitOfWork repository, TokenService tokenService, IMailSender mailSender, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _unitOfWork = repository;
            _tokenService = tokenService;
            _mailSender = mailSender;
            _mapper =  mapper;
            _configuration = configuration;
        }

        public bool AddSetting(string image)
        {
            var currentPath = Path.GetFullPath("wwwroot");
            Setting addedSetting = new Setting()
            {
                Id = Guid.NewGuid(),
                Type = SettingType.LandingPageSlideImage
            };
            
            var x = Regex.Replace(image, @"^data:image\/[a-zA-Z]+;base64,", "");
            //Byte[] bytes = Convert.FromBase64String(x);
            //var stream = new MemoryStream(bytes);
            string filePath = $"{currentPath}\\{addedSetting.Id}.png";
            File.WriteAllBytes(filePath, Convert.FromBase64String(x));

            addedSetting.Content = $"{addedSetting.Id}.png";

            _unitOfWork.SettingRepository.Add(addedSetting);
            return true;
        }
    }
}
