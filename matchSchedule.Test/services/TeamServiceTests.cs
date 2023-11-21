using AutoFixture;
using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using matchSchedule.Services.Implements;

namespace matchSchedule.Test.services
{
    public class TeamServiceTests
    {
        private readonly TeamService _teamService;
        private readonly Mock<ITeamRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Fixture _fixture;

        public TeamServiceTests()
        {
            _repositoryMock = new Mock<ITeamRepository>();
            _mapperMock = new Mock<IMapper>();
            _teamService = new TeamService(_repositoryMock.Object, _mapperMock.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task CreateNewTeamAsync_Success()
        {
            // Arrange  
            var newTeamDto = new NewTeamDTO
            {
                Name = "NewTeam",
                Country = "country",
                YearFounded = 1991,
                TeamId = Guid.NewGuid(),
                Logo = "asd",
                PlayerIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }
            };

            _repositoryMock.Setup(repo => repo.DoesTeamExistAsync(newTeamDto.Name)).ReturnsAsync(false);

            _repositoryMock.Setup(repo => repo.AddEntityAsync(It.IsAny<Team>()));
            _repositoryMock.Setup(repo => repo.SaveAllAsync()).ReturnsAsync(true);

            // Act
            var result = await _teamService.CreateNewTeamAsync(newTeamDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.IsType<Team>(result.Value);
        }

        [Fact]
        public async Task CreateNewTeamAsync_TeamAlreadyExists()
        {
            // Arrange
            var existingTeamName = "ExistingTeam";
            var newTeamDto = new NewTeamDTO { Name = existingTeamName };

            // Mocking behavior for repository method
            _repositoryMock.Setup(repo => repo.DoesTeamExistAsync(existingTeamName)).ReturnsAsync(true);

            // Act
            var result = await _teamService.CreateNewTeamAsync(newTeamDto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TeamErrors.AlreadyExist, result.Error);
        }

        [Fact]
        public async Task CreateNewTeamAsync_SaveFailed()
        {
            // Arrange
            var newTeamDto = new NewTeamDTO { Name = "NewTeam", Country = "Some country", YearFounded = 1991 };

            // Mocking behavior for repository methods
            _repositoryMock.Setup(repo => repo.DoesTeamExistAsync(newTeamDto.Name)).ReturnsAsync(false);
            _repositoryMock.Setup(repo => repo.AddEntityAsync(It.IsAny<Team>()));
            _repositoryMock.Setup(repo => repo.SaveAllAsync()).ReturnsAsync(false);

            // Act
            var result = await _teamService.CreateNewTeamAsync(newTeamDto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TeamErrors.BadRequest, result.Error);
        }


    }


}
