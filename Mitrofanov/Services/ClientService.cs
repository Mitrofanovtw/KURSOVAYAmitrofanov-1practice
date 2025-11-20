using AutoMapper;
using StudioStatistic.DTO;
using StudioStatistic.Models;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repo;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список всех клиентов
        /// </summary>
        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = _repo.GetAll();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        /// <summary>
        /// Возвращает клиента по ID
        /// </summary>
        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = _repo.GetById(id);
            return client == null ? null : _mapper.Map<ClientDto>(client);
        }

        /// <summary>
        /// Создаёт нового клиента
        /// </summary>
        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            var created = _repo.Create(client);
            return _mapper.Map<ClientDto>(created);
        }

        /// <summary>
        /// Обновляет клиента
        /// </summary>
        public async Task<ClientDto> UpdateAsync(int id, ClientDto dto)
        {
            var client = _repo.GetById(id);
            if (client == null) throw new KeyNotFoundException("Клиент не найден");

            _mapper.Map(dto, client);
            var updated = _repo.Update(client);
            return _mapper.Map<ClientDto>(updated);
        }

        /// <summary>
        /// Удаляет клиента
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return _repo.Delete(id);
        }
    }
}