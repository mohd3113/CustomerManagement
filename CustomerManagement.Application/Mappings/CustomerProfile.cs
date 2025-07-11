using AutoMapper;
using CustomerManagement.Application.Dtos.Customers;
using CustomerManagement.Domain;

namespace CustomerManagement.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            _ = CreateMap<CreateUpdateCustomerDto, Customer>();
            _ = CreateMap<Customer, CustomerDetailsDto>();
        }
    }
}