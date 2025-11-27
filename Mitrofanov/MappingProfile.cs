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
            CreateMap<UpdateClientDto, Client>();
            CreateMap<CreateClientDto, Client>();

            CreateMap<Engineers, EngineerDto>();
            CreateMap<CreateEngineerDto, Engineers>();
            CreateMap<UpdateEngineerDto, Engineers>();

            CreateMap<Service, ServiceDto>();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<ServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>();

            CreateMap<Admin, AdminDto>();
            CreateMap<CreateAdminDto, Admin>()
       .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<Request, RequestDto>()
        .ForMember(d => d.ClientName, o => o.MapFrom(s => $"{s.Client.FirstName} {s.Client.LastName}"))
        .ForMember(d => d.EngineerName, o => o.MapFrom(s => $"{s.Engineer.FirstName} {s.Engineer.LastName}"))
        .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.Name));
            CreateMap<CreateRequestDto, Request>();
        }
    }
}