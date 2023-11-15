//using AutoFixture;
//using matchSchedule.Controllers;
//using matchSchedule.Models;
//using matchSchedule.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace matchSchedule.Test
//{
//    public class TeamControllerTests
//    {
//        private readonly Mock<ITeamService> _service;
//        private readonly Mock<ILogger<TeamController>> _logger;
//        private readonly Fixture _fixture;
//        private readonly TeamController _controller;
//        public TeamControllerTests()
//        {
//            _service = new Mock<ITeamService>();
//            _logger = new Mock<ILogger<TeamController>>();
//            _fixture = new Fixture();
//            _controller = new TeamController(_service.Object, _logger.Object, null,null);
//        }


//        [Fact]
//        public async Task Get_ReturnsOkResultWithData()
//        {
//            // Arrange
//            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
//            var teams = _fixture.CreateMany<Team>(10).ToList(); // Generate 2 teams using AutoFixture
//            _service.Setup(service => service.GetAllAsync()).ReturnsAsync(teams);

//            // Act
//            var result = await _controller.Get();

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);
//            var returnedTeams = Assert.IsAssignableFrom<List<Team>>(okResult.Value);
//            Assert.Equal(teams, returnedTeams);
//        }

//        [Fact]
//        public async Task Get_ReturnsOkObjectResult_ForValidId()
//        {
//            // Arrange
//            Guid validId = Guid.NewGuid();
//            _service.Setup(service => service.GetTeamByIdAsync(validId))
//                           .ReturnsAsync(new Team { Id = validId });

//            var controller = new TeamController(_service.Object, null, null,null);

//            // Act
//            var result = await controller.Get(validId);

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);

//        }

//        [Fact]
//        public async Task AddPlayerToTeam_ReturnsOkWithData()
//        {
//            //Arrange
//            var validPlayerId = new Guid();
//            var validTeamId = new Guid();
//            _service.Setup(service => service.AddPlayerAsync(validTeamId, validPlayerId)).ReturnsAsync(new Team { Id = validTeamId, Players = new List<Player> { new Player { PlayerId = validPlayerId } } });

//            var controller = new TeamController(_service.Object, null, null);

//            //Act
//            var result = await controller.Post(validTeamId, validPlayerId);

//            //Assert
//            var okresult = Assert.IsType<OkObjectResult>(result);
//            var returnedData = Assert.IsAssignableFrom<Team>(okresult.Value);
//            Assert.Equal(returnedData.Players.First(i => i.PlayerId == validPlayerId).PlayerId, validPlayerId);

//        }

//        [Fact]
//        public async Task AddPlayerToTeam_ReturnsBadRequest()
//        {
//            //Arrange
//            var invalidId = Guid.Empty;
//            var validPlayerId = Guid.NewGuid();
//            _service.Setup(service => service.AddPlayerAsync(invalidId, validPlayerId)).ReturnsAsync(null as Team);
//            var controller = new TeamController(_service.Object, null, null);

//            //Act
//            var result = await controller.Post(invalidId, validPlayerId);

//            //Assert
//            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
//        }
//    }

//}
