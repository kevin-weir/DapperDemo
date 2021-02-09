using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Dapper.Repository.Models;
using Dapper.Domain.Models;

namespace Dapper.API.Services
{
    public static class AutoMapperService
    {
        public static void AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerDtoInsert, Customer>();
            CreateMap<CustomerDtoUpdate, Customer>();

            CreateMap<CountryDtoInsert, Country>();
            CreateMap<CountryDtoUpdate, Country>();

            CreateMap<ProvinceDtoInsert, Province>();
            CreateMap<ProvinceDtoUpdate, Province>();
        }
    }
}
