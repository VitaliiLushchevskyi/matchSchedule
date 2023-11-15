using AutoFixture;
using AutoMapper;
using matchSchedule.Controllers;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
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
        }


        [Fact]
        public async Task CreateMatch_Should_ReturnError_WhenTeamsAreEqual()
        {
            // Arrange
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var testTournament = _fixture.Create<Tournament>();
            var testTeam = _fixture.Create<Team>();

            var match = new MatchViewModel { AwayTeamId = testTeam.Id, HomeTeamId = testTeam.Id, Tournament = new Tournament { Id = testTournament.Id }, MatchDateTime = DateTime.Today, Referee = "asd" };
            _service.Setup(service => service.GetTournamentById(match.Tournament.Id)).Returns(testTournament);
            _service.Setup(service => service.GetTeamById(match.HomeTeamId)).Returns(testTeam);
            _service.Setup(service => service.GetTeamById(match.AwayTeamId)).Returns(testTeam);

            // Act
            var result = await _controller.Post(match);

            // Assert
            var returnedType = Assert.IsType<Result>(result);
            Assert.True(returnedType.IsFailure);
            Assert.Equal(MatchErrors.SameTeams, returnedType.Error);
        }
    }
}
