using AutoMapper;
using CustomerOrderApp.Core.DTOs;
using CustomerOrderApp.Core.Models;

namespace CustomerOrderApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CustomerOrder, CustomerOrderInsertDto>().ReverseMap();
            CreateMap<CustomerOrderInsertDto, CustomerOrder>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderAddressUpdateDto>().ReverseMap();
            CreateMap<CustomerOrderAddressUpdateDto, CustomerOrder>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderQuantityUpdateDto>().ReverseMap();
            CreateMap<CustomerOrderQuantityUpdateDto, CustomerOrder>().ReverseMap();
        }
    }
}
