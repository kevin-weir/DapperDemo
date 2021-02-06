using AutoMapper;
using Dapper.Repository.Models;
using Dapper.Domain.Models;

namespace Dapper.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDtoInsert, Customer>();
            CreateMap<CustomerDtoUpdate, Customer>();
        }
    }
}