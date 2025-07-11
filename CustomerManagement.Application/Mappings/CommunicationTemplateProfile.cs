using AutoMapper;
using CustomerManagement.Application.Dtos.CommunicationTemplates;
using CustomerManagement.Domain;

namespace CustomerManagement.Application.Mappings
{
    public class CommunicationTemplateProfile : Profile
    {

        public CommunicationTemplateProfile()
        {
            CreateMap<CreateUpdateTemplateDto, CommunicationTemplate>().ReverseMap();
            CreateMap<CommunicationTemplate, TemplateDetailsDto>().ReverseMap();
            CreateMap<CommunicationTemplate, TemplateListItemDto>().ReverseMap();
        }
    }
}
