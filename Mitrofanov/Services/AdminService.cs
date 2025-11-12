using AutoMapper;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех админов
        /// </summary>
        public async Task<IEnumerable<AdminDto>> GetAllAsync()
        {
            var admins = _repo.GetAll();
            return _mapper.Map<IEnumerable<AdminDto>>(admins);
        }

        /// <summary>
        /// Получить админа по ID
        /// </summary>
        public async Task<AdminDto?> GetByIdAsync(int id)
        {
            var admin = _repo.GetById(id);
            return admin == null ? null : _mapper.Map<AdminDto>(admin);
        }

        /// <summary>
        /// Создать нового админа
        /// </summary>
        public async Task<AdminDto> CreateAsync(CreateAdminDto dto)
        {
            var admin = _mapper.Map<Admin>(dto);
            var created = _repo.Create(admin);
            return _mapper.Map<AdminDto>(created);
        }
    }
}