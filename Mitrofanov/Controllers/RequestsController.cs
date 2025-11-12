using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Models.DTO;
using StudioStatistic.Services;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;

        public RequestsController(IRequestService service) => _service = service;

        /// <summary>
        /// Получить все заявки
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetAll()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        /// <summary>
        /// Получить заявку по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDto>> GetById(int id)
        {
            var request = await _service.GetByIdAsync(id);
            return request is null ? NotFound() : Ok(request);
        }

        /// <summary>
        /// Создать новую заявку
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RequestDto>> Create([FromBody] CreateRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, (object?)created);
        }
    }
}