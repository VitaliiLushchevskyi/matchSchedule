using AutoFixture;
using AutoMapper;
using matchSchedule.Controllers;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace matchSchedule.Test.controllers
{
    public class TeamControllerTests
    {
        private readonly Mock<ITeamService> _service;
        private readonly Mock<ILogger<TeamController>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Fixture _fixture;
        private readonly TeamController _controller;

        public TeamControllerTests()
        {
            _fixture = new Fixture();
            _service = new Mock<ITeamService>();
            _logger = new Mock<ILogger<TeamController>>();
            _mapper = new Mock<IMapper>();
            _controller = new TeamController(_logger.Object, _mapper.Object, _service.Object);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllTeams_ShouldReturnOk()
        {
            //Arrange
            var teams = _fixture.CreateMany<Team>().ToList();
            _service.Setup(s => s.GetTeamAsync()).ReturnsAsync(Result.Success(teams));

            //Act
            var result = await _controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultTeams = Assert.IsAssignableFrom<IEnumerable<Team>>(okResult.Value);
            Assert.Equal(teams, resultTeams);
        }


    }
}
