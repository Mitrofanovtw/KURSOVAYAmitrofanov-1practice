using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Services;
using StudioStatistic.Models.DTO;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить все услуги студии
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
        {
            var services = await _service.GetAllAsync();
            return Ok(services);
        }

        /// <summary>
        /// Получить услугу по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            var service = await _service.GetByIdAsync(id);
            return service == null ? NotFound() : Ok(service);
        }

        /// <summary>
        /// Создать новую услугу (админ)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Обновить услугу
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Удалить услугу
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}