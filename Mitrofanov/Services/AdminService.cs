using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;

namespace StudioStatistic.Services
{
    public class AdminService : IAdminService
    {
        private readonly IClientRepository _clientRepo;
        private readonly IAdminRepository _repo;
        private readonly IEngineersRepository _engineerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AdminService(
            IAdminRepository repo,
            IUserRepository userRepo,
            IClientRepository clientRepo,
            IAdminRepository adminRepo,
            IEngineersRepository engineerRepo,
            IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _clientRepo = clientRepo;
            _engineerRepo = engineerRepo;
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
        public async Task UpdateRoleAsync(int userId, UserRole newRole)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("Пользователь не найден");

            var oldRole = user.Role;

            if (oldRole == UserRole.Client && newRole != UserRole.Client)
            {
                var client = await _clientRepo.GetByIdAsync(userId);
                if (client != null)
                {
                    if (newRole == UserRole.Admin)
                    {
                        var admin = new Admin
                        {
                            Id = userId,
                            Name = client.FirstName,
                            Email = "",
                        };
                        await _repo.CreateAsync(admin);
                    }
                    else if (newRole == UserRole.Engineer)
                    {
                        var engineer = new Engineers
                        {
                            Id = userId,
                            FirstName = client.FirstName,
                            LastName = client.LastName,
                            Adress = null,
                            WorkExp = "",
                            AboutHimself = null,
                            Requests = null
                        };
                        await _engineerRepo.CreateAsync(engineer);
                    }

                    await _clientRepo.DeleteAsync(userId);
                }
            }
            else if (oldRole != UserRole.Client && newRole == UserRole.Client)
            {
                Client client = null;

                if (oldRole == UserRole.Admin)
                {
                    var admin = await _repo.GetByIdAsync(userId);
                    if (admin != null)
                    {
                        client = new Client
                        {
                            Id = userId,
                            FirstName = admin.Name ?? "",
                            LastName = "",
                            QuantityOfVisits = 0,
                            Requests = new List<Request>()
                        };
                        await _repo.DeleteAsync(userId);
                    }
                }
                else if (oldRole == UserRole.Engineer)
                {
                    var engineer = await _engineerRepo.GetByIdAsync(userId);
                    if (engineer != null)
                    {
                        client = new Client
                        {
                            Id = userId,
                            FirstName = engineer.FirstName,
                            LastName = engineer.LastName,
                            QuantityOfVisits = 0,
                            Requests = engineer.Requests
                        };
                        await _engineerRepo.DeleteAsync(userId);
                    }
                }

                if (client != null)
                {
                    await _clientRepo.CreateAsync(client);
                }
            }

            user.Role = newRole;
            await _userRepo.UpdateAsync(user);
        }

        public async Task<UserDto> ChangeUserRoleAsync(int userId, UserRole newRole)
        {
            await UpdateRoleAsync(userId, newRole);
            var updatedUser = await _userRepo.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(updatedUser);
        }
    }
}