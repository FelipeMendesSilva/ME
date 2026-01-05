using ME.SuperHero.Application.Handlers.Command;
using ME.SuperHero.Domain.Interfaces;
using Moq;
using System.Reflection.Metadata;

namespace ME.SuperHeroTests
{
    public class BaseTest
    {
        protected readonly Mock<IHeroisRepository> _heroisRepoMock;
        protected readonly Mock<IHeroisSuperpoderesRepository> _heroisSupRepoMock;
        protected readonly CreateHeroiCommandHandler _createHandler;
        protected readonly Mock<IUow> _uowMock;

        public BaseTest()
        {
            _heroisRepoMock = new Mock<IHeroisRepository>();
            _heroisSupRepoMock = new Mock<IHeroisSuperpoderesRepository>();
            _uowMock = new Mock<IUow>();
            _createHandler = new CreateHeroiCommandHandler(
                _heroisRepoMock.Object,
                _heroisSupRepoMock.Object,
                _uowMock.Object
            );
        }
    }
}