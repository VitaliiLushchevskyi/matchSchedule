using AutoFixture;
using AutoMapper;
using matchSchedule.Controllers;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace matchSchedule.Test
{
    public class MatchControllerTests
    {
        private readonly Mock<IMatchService> _service;
        private readonly Mock<ILogger<MatchController>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Fixture _fixture;
        private readonly MatchController _controller;

        public MatchControllerTests()
        {
            _fixture = new Fixture();
            _service = new Mock<IMatchService>();
            _logger = new Mock<ILogger<MatchController>>();
            _mapper = new Mock<IMapper>();
            _controller = new MatchController(_service.Object, _logger.Object, _mapper.Object);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        [Fact]
        public async Task CreateMatch_Should_ReturnError_WhenTeamsAreEqual()
        {
            // Arrange
            var testTournament = _fixture.Create<Tournament>();
            var testTeam = _fixture.Create<Team>();

            var modelDTO = new NewMatchDTO { AwayTeamId = testTeam.Id, HomeTeamId = testTeam.Id, Tournament = new Tournament { Id = testTournament.Id }, MatchDateTime = DateTime.Today, Referee = "asd" };
            _service.Setup(service => service.AddMatchAsync(modelDTO));

            // Act
            var result = await _controller.Post(modelDTO);

            // Assert
            var returnedType = Assert.IsType<Result>(result);
            Assert.True(returnedType.IsFailure);
            Assert.Equal(MatchErrors.SameTeams, returnedType.Error);
        }

        [Fact]
        public async Task CreateMatch_Should_Return_Success()
        {
            // Arrange
            var testTournament = _fixture.Create<Tournament>();
            var testTeam = _fixture.Create<Team>();

            var modelDTO = new NewMatchDTO { AwayTeamId = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), Tournament = new Tournament { Id = Guid.NewGuid() }, MatchDateTime = DateTime.Today, Referee = "test" };
            var fakeMatch = _fixture.Create<Models.Match>();
            _service.Setup(service => service.AddMatchAsync(modelDTO)).ReturnsAsync(fakeMatch);

            // Act
            var result = await _controller.Post(modelDTO);

            // Assert
            var returnedType = Assert.IsType<Result>(result);
            Assert.True(returnedType.IsSuccess);

        }


        [Fact]
        public async Task CreateMatch_Should_ReturnError_WhenTournamentIsNull()
        {
            // Arrange
            var testTournament = _fixture.Create<Tournament>();
            var testTeam = _fixture.Create<Team>();

            var modelDTO = new NewMatchDTO { AwayTeamId = Guid.NewGuid(), HomeTeamId = Guid.NewGuid(), Tournament = null, MatchDateTime = DateTime.Today, Referee = "asd" };
            _service.Setup(service => service.AddMatchAsync(modelDTO));

            // Act
            var result = await _controller.Post(modelDTO);

            // Assert
            var returnedType = Assert.IsType<Result>(result);
            Assert.True(returnedType.IsFailure);
            Assert.Equal(MatchErrors.NotFoundTournament, returnedType.Error);
        }
    }
}
