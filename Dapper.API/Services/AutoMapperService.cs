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
            CreateMap<CustomerPostDTO, Customer>();
            CreateMap<CustomerPutDTO, Customer>();
            CreateMap<Customer, CustomerResponseDTO>();

            CreateMap<CountryPostDTO, Country>();
            CreateMap<CountryPutDTO, Country>();
            CreateMap<Country, CountryResponseDTO>();

            CreateMap<ProvincePostDTO, Province>();
            CreateMap<ProvincePutDTO, Province>();
            CreateMap<Province, ProvinceResponseDTO>();

            CreateMap<OrderPostDTO, Order>();
            CreateMap<OrderPutDTO, Order>();
            CreateMap<Order, OrderResponseDTO>();

            // Paged mappings
            CreateMap<PagedResults<Order>, PagedResults<OrderResponseDTO>>();
        }
    }
}
