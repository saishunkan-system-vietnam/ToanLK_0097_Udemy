using AutoMapper;
using BaseProject.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.AutoMapper
{
    public class ProfileAutoMapperConfig : Profile
    {
        public ProfileAutoMapperConfig()
        {
            CreateMap<Account, Account>();
            CreateMap<Product, Product>();
        }

    }
}
