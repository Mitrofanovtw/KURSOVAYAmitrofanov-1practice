using Microsoft.AspNetCore.Mvc;
using StudioStatistic.DTO;
using StudioStatistic.Services;

namespace StudioStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientsController(IClientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            var clients = await _service.GetAllAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetById(int id)
        {
            var client = await _service.GetByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create(ClientDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}