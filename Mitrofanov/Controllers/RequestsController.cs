using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioStatistic.Models;
using StudioStatistic.Models.DTO;
using StudioStatistic.Repositories;
using StudioStatistic.Services;
using System.Security.Claims;

namespace StudioStatistic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;
        private readonly APIDBContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService service, IUserRepository userRepository, APIDBContext context, IRequestService requestService)
        {
            _service = service;
            _userRepository = userRepository;
            _context = context;
            _requestService = requestService;
        }

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

        [HttpGet("my")]
        [Authorize]
        public async Task<ActionResult<List<RequestDto>>> GetMyRequests()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.Role != UserRole.Client)
                return Forbid();

            var requests = await _service.GetByClientIdAsync(user.Id);
            return Ok(requests);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Engineer,Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
                return NotFound();

            if (!Enum.TryParse<RequestStatus>(dto.Status, true, out var status))
                return BadRequest("Неверный статус");

            request.Status = status;
            await _context.SaveChangesAsync();

            return Ok();
        }

        public class UpdateStatusDto
        {
            public string Status { get; set; } = "New";
        }
    }
}