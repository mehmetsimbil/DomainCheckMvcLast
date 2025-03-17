using AutoMapper;
using Business.Dto_s.Domain;
using Business.Requests.Domain;
using Business.Responses.Domain;
using Entities;


namespace Business.Profiles
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<AddDomainRequest, Domain>();
            CreateMap<Domain, AddDomainResponse>();

            CreateMap<Domain, DomainListItemDto>();
            CreateMap<IList<Domain>, GetDomainListResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<Domain, DeleteDomainResponse>();
            CreateMap<UpdateDomainRequest, Domain>().ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<Domain, UpdateDomainResponse>();

            CreateMap<Domain, GetDomainByIdResponse>();
        }
    }
}
