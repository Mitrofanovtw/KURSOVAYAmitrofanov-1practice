using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Services;
using StudioStatistic.Models.DTO;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminsController(IAdminService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить всех админов (суперадмин)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAll()
        {
            var admins = await _service.GetAllAsync();
            return Ok(admins);
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