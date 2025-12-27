
using AutoMapper;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IEngineersRepository _engineerRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly IMapper _mapper;

        public RequestService(
            IRequestRepository requestRepo,
            IClientRepository clientRepo,
            IEngineersRepository engineerRepo,
            IServiceRepository serviceRepo,
            IMapper mapper)
        {
            _requestRepo = requestRepo;
            _clientRepo = clientRepo;
            _engineerRepo = engineerRepo;
            _serviceRepo = serviceRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestDto>> GetAllAsync()
        {
            var requests = await Task.Run(() => _requestRepo.GetAll());
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }

        public async Task<RequestDto?> GetByIdAsync(int id)
        {
            var request = await Task.Run(() => _requestRepo.GetById(id));
            return request == null ? null : _mapper.Map<RequestDto>(request);
        }

        public async Task<RequestDto> CreateAsync(CreateRequestDto dto)
        {
            var request = _mapper.Map<Request>(dto);

            var client = await Task.Run(() => _clientRepo.GetById(dto.ClientId));
            if (client == null)
                throw new KeyNotFoundException("Клиент не найден");

            var engineer = await Task.Run(() => _engineerRepo.GetById(dto.EngineerId));
            if (engineer == null)
                throw new KeyNotFoundException("Инженер не найден");

            var service = await Task.Run(() => _serviceRepo.GetById(dto.ServiceId));
            if (service == null)
                throw new KeyNotFoundException("Услуга не найдена");

            request.Client = client;
            request.Engineer = engineer;
            request.Service = service;
            request.Cost = service.Price;

            var created = await Task.Run(() => _requestRepo.Create(request));
            return _mapper.Map<RequestDto>(created);
        }

        public async Task<List<RequestDto>> GetByClientIdAsync(int clientId)
        {
            var requests = await _requestRepo.GetByClientIdAsync(clientId);
            return _mapper.Map<List<RequestDto>>(requests);
        }
    }
}