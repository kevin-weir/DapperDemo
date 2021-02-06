using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Repository.Models;
using Dapper.Domain.Models;

namespace Dapper.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CustomerDtoInsert, Customer>().ReverseMap();
            CreateMap<CustomerDtoInsert, Customer>();
            CreateMap<CustomerDtoUpdate, Customer>();
        }
    }
}