using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public AdminService(IAdminRepository repo, IUserRepository userRepo, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех админов
        /// </summary>
        public async Task<IEnumerable<AdminDto>> GetAllAsync()
        {
            var admins = await Task.Run(() => _repo.GetAll());
            return _mapper.Map<IEnumerable<AdminDto>>(admins);
        }

        /// <summary>
        /// Получить админа по ID
        /// </summary>
        public async Task<AdminDto?> GetByIdAsync(int id)
        {
            var admin = await Task.Run(() => _repo.GetById(id));
            return admin == null ? null : _mapper.Map<AdminDto>(admin);
        }

        /// <summary>
        /// Создать нового админа
        /// </summary>
        public async Task<AdminDto> CreateAsync(CreateAdminDto dto)
        {
            var existingAdmin = _repo.GetAll().FirstOrDefault(a => a.Email == dto.Email);
            if (existingAdmin != null)
                throw new InvalidOperationException("Администратор с таким email уже существует");

            var admin = _mapper.Map<Admin>(dto);
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var created = await Task.Run(() => _repo.Create(admin));
            return _mapper.Map<AdminDto>(created);
        }

        /// <summary>
        /// Обновление роли
        /// </summary>
        public async Task<UserDto> ChangeUserRoleAsync(int userId, UserRole newRole)
        {
            var user = await Task.Run(() => _userRepo.GetById(userId));
            if (user == null)
                throw new KeyNotFoundException("Пользователь не найден");

            user.Role = newRole;
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}