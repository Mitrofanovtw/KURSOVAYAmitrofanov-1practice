using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using StudioStatistic.Controllers;
using StudioStatistic.Services;
using StudioStatistic.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioStatistic.Tests.Controllers
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientService> _mockService;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _mockService = new Mock<IClientService>();
            _controller = new ClientsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfClients()
        {
            var clients = new List<ClientDto>
            {
                new() { Id = 1, FirstName = "Иван", LastName = "Иванов", QuantityOfVisits = 3 },
                new() { Id = 2, FirstName = "Пётр", LastName = "Петров", QuantityOfVisits = 1 }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(clients);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ClientDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal("Иван", returnValue[0].FirstName);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk_WithClient()
        {
            var client = new ClientDto { Id = 1, FirstName = "Мария", LastName = "Шкуратова", QuantityOfVisits = 5 };
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(client);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ClientDto>(okResult.Value);
            Assert.Equal("Мария", returnValue.FirstName);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((ClientDto)null!);

            var result = await _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidData_ReturnsCreatedAtAction()
        {
            var dto = new CreateClientDto
            {
                FirstName = "Иван",
                LastName = "Кузнецов",
                QuantityOfVisits = 0
            };

            var createdClient = new ClientDto
            {
                Id = 10,
                FirstName = "Иван",
                LastName = "Кузнецов",
                QuantityOfVisits = 0
            };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(createdClient);

            var result = await _controller.Create(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetById", createdResult.ActionName);
            Assert.Equal(10, createdResult.RouteValues["id"]);
            var returnValue = Assert.IsType<ClientDto>(createdResult.Value);
            Assert.Equal("Иван", returnValue.FirstName);
        }

        [Fact]
        public async Task Create_InvalidData_ReturnsBadRequest()
        {
            var dto = new CreateClientDto { FirstName = "", LastName = "Кузнецов" };
            _controller.ModelState.AddModelError("FirstName", "Имя клиента обязательно");

            var result = await _controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ServiceThrowsException_Returns500()
        {
            _mockService.Setup(s => s.GetAllAsync())
                .ThrowsAsync(new Exception("Тестовая ошибка БД"));

            var exception = await Assert.ThrowsAsync<Exception>(() => _controller.GetAll());
            Assert.Contains("БД", exception.Message);
        }
    }
}