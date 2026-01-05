using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ME.SuperHero.Application.Handlers.Command;
using ME.SuperHero.Application.Requests;
using ME.SuperHero.Domain.Entities;
using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Domain.Result;
using ME.SuperHeroTests;
using Moq;
using Xunit;

namespace ME.SuperHero.Tests.Handlers
{
    public class CreateHeroiCommandHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenHeroiAlreadyExists()
        {
            // Arrange
            var request = new RequestCreateHeroi
            {
                Nome = "Clark Kent",
                NomeHeroi = "Superman",
                Altura = 1.9,
                Peso = 95,
                DataNascimento = null,
                Superpoderes = new List<int>() { 1, 2, 3 }
            };

            _heroisRepoMock
                .Setup(r => r.ExistsHeroiByNameAsync(request.NomeHeroi, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _createHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess());
            Assert.Equal("NomeHeroi already exists", result.ErrorMessage);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenHeroiIsCreated()
        {
            // Arrange
            var request = new RequestCreateHeroi
            {
                Nome = "Clark Kent",
                NomeHeroi = "Superman",
                Altura = 1.9,
                Peso = 95,
                DataNascimento = null,
                Superpoderes = new List<int>() { 1, 2, 3 }
            };

            _heroisRepoMock
                .Setup(r => r.ExistsHeroiByNameAsync(request.NomeHeroi, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _heroisRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Herois>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _uowMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _createHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess());
            Assert.Equal("Created", result.Data);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            _heroisSupRepoMock.Verify(r => r.AddPowerAsync(It.IsAny<HeroisSuperpoderes>(), It.IsAny<CancellationToken>()), Times.Exactly(request.Superpoderes.Count));
            _uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCreateAsyncFails()
        {
            // Arrange
            var request = new RequestCreateHeroi
            {
                Nome = "Clark Kent",
                NomeHeroi = "Superman",
                Altura = 1.9,
                Peso = 95,
                DataNascimento = null,
                Superpoderes = new List<int>() { 1, 2, 3 }
            };

            _heroisRepoMock
                .Setup(r => r.ExistsHeroiByNameAsync(request.NomeHeroi, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _heroisRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<Herois>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _createHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess());
            Assert.Equal("Creating not completed", result.ErrorMessage);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
