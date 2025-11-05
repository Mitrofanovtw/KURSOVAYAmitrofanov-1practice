using AutoMapper;
using StudioStatistic.DTO;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;

namespace StudioStatistic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
            CreateMap<Engineers, EngineerDto>();
            CreateMap<CreateEngineerDto, Engineers>();
            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<Admin, AdminDto>();
            CreateMap<CreateAdminDto, Admin>();
            CreateMap<Request, RequestDto>()
                .ForMember(d => d.ClientName, o => o.MapFrom(s => s.Client.Name))
                .ForMember(d => d.EngineerName, o => o.MapFrom(s => s.Engineer.Name))
                .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.Name));

            CreateMap<CreateRequestDto, Request>();
        }
    }
}