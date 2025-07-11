using AutoMapper;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Domain;

namespace CustomerManagement.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateUpdateCustomerDto, Customer>().ReverseMap();
            CreateMap<Customer, CustomerDetailsDto>().ReverseMap();
        }
    }
}