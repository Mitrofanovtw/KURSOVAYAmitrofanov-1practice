using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;
using StudioStatistic.Services;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _service;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminsController(IAdminService service, IUserRepository userRepository, IMapper mapper)
        {
            _service = service;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех админов (суперадмин)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AdminDto>>> GetAllAdmins()
        {
            var admins = await _userRepository.GetByRoleAsync(UserRole.Admin);
            var adminDtos = _mapper.Map<List<AdminDto>>(admins);
            return Ok(adminDtos);
        }

        /// <summary>
        /// Получить админа по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetById(int id)
        {
            var admin = await _service.GetByIdAsync(id);
            return admin == null ? NotFound() : Ok(admin);
        }

        /// <summary>
        /// Создать нового админа (суперадмин)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AdminDto>> Create([FromBody] CreateAdminDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Изменить роль пользователя (только для админов)
        /// </summary>
        [HttpPut("users/{userId}/role")]
        public async Task<ActionResult<UserDto>> ChangeUserRole(int userId, [FromBody] ChangeRoleDto dto)
        {
            try
            {
                var updatedUser = await _service.ChangeUserRoleAsync(userId, dto.NewRole);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}