using AutoMapper;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Models.DTO.StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все услуги студии
        /// </summary>
        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = _repo.GetAll();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        public async Task<ServiceDto?> GetByIdAsync(int id)
        {
            var service = _repo.GetById(id);
            return service == null ? null : _mapper.Map<ServiceDto>(service);
        }

        /// <summary>
        /// Создать новую услугу
        /// </summary>
        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var existingService = _repo.GetAll().FirstOrDefault(s => s.Name == dto.Name);
            if (existingService != null)
            {
                throw new InvalidOperationException("Услуга с таким названием уже существует");
            }

            var service = _mapper.Map<Service>(dto);
            var created = _repo.Create(service);
            return _mapper.Map<ServiceDto>(created);
        }

        /// <summary>
        /// Обновить услугу
        /// </summary>
        public async Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto dto)
        {
            var service = _repo.GetById(id);
            if (service == null) throw new KeyNotFoundException("Услуга не найдена");

            // Автоматический маппинг
            _mapper.Map(dto, service);
            var updated = _repo.Update(service);
            return _mapper.Map<ServiceDto>(updated);
        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return _repo.Delete(id);
        }
    }
}