using AutoFixture;
using AutoMapper;
using matchSchedule.Controllers;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace matchSchedule.Test
{
    public class TeamControllerTests
    {
        private readonly Mock<ITeamService> _service;
        private readonly Mock<ILogger<TeamController>> _logger;
        private readonly Fixture _fixture;
        private readonly TeamController _controller;
        public TeamControllerTests()
        {
            _service = new Mock<ITeamService>();
            _logger = new Mock<ILogger<TeamController>>();
            _fixture = new Fixture();
            _controller = new TeamController(_service.Object, _logger.Object, null);
        }


        [Fact]
        public async Task Get_ReturnsOkResultWithData()
        {
            // Arrange
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var teams = _fixture.CreateMany<Team>(10).ToList(); // Generate 2 teams using AutoFixture
            _service.Setup(service => service.GetAllAsync()).ReturnsAsync(teams);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTeams = Assert.IsAssignableFrom<List<Team>>(okResult.Value);
            Assert.Equal(teams, returnedTeams);
        }

        [Fact]
        public async Task Get_ReturnsOkObjectResult_ForValidId()
        {
            // Arrange
            Guid validId = Guid.NewGuid();
            var mockTeamService = new Mock<ITeamService>();
            mockTeamService.Setup(service => service.GetTeamByIdAsync(validId))
                           .ReturnsAsync(new Team { Id = validId});

            var controller = new TeamController(mockTeamService.Object, null, null);

            // Act
            var result = await controller.Get(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
           
        }
    }
}
