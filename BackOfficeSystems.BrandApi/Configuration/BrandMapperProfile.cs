using AutoMapper;
using BackOfficeSystems.BrandApi.Api.Models;
using BackOfficeSystems.BrandApi.Domain.BrandAggregate;

namespace BackOfficeSystems.BrandApi.Configuration
{
    // ReSharper disable once UnusedType.Global
    public class BrandMapperProfile : Profile
    {
        public BrandMapperProfile()
        {
            CreateMap<BrandCreateRequestModel, Brand>()
                .ForMember(dest => dest.BrandId, opt => opt.Ignore());

            CreateMap<BrandUpdateRequestModel, Brand>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Brand, BrandResponseModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BrandId));

            CreateMap<Brand, SumOfInventoryResponseModel>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.GetSumOfInventory()));
        }
    }
}