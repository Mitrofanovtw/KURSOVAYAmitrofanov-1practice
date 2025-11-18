using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Models.DTO;
using StudioStatistic.Services;

namespace StudioStatistic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EngineersController : ControllerBase
    {
        private readonly IEngineerService _service;

        public EngineersController(IEngineerService service) => _service = service;

        /// <summary>
        /// Получить всех звукорежиссёров
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EngineerDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Получить звукорежиссёра по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EngineerDto>> GetById(int id)
        {
            var engineer = await _service.GetByIdAsync(id);
            return engineer is null ? NotFound() : Ok(engineer);
        }

        /// <summary>
        /// Нанять нового звукорежиссёра
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EngineerDto>> Create([FromBody] CreateEngineerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Уволить звукорежиссёра
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}