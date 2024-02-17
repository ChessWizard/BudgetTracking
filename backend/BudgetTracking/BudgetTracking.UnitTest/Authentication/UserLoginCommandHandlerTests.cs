using BudgetTracking.Core.Enums;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Dto;
using BudgetTracking.Data.Extensions.Collection;
using BudgetTracking.Service.Services.Authentication.Commands.UserLogin;
using BudgetTracking.Service.Services.Token.Commands.CreateToken;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.UnitTest.Authentication
{
    public class UserLoginCommandHandlerTests
    {
        private readonly Mock<UserManager<Core.Entities.User>> _userManagerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IUnitofWork> _unitOfWorkMock;
        private readonly Mock<IUserRefreshTokenRepository> _userRefreshTokenRepositoryMock;
        private readonly UserLoginCommandHandler _handler;

        public UserLoginCommandHandlerTests()
        {
            // UserManager mock setup
            _userManagerMock = new Mock<UserManager<Core.Entities.User>>(
                Mock.Of<IUserStore<Core.Entities.User>>(), null, null, null, null, null, null, null, null);

            _mediatorMock = new Mock<IMediator>();
            _unitOfWorkMock = new Mock<IUnitofWork>();
            _userRefreshTokenRepositoryMock = new Mock<IUserRefreshTokenRepository>();

            _handler = new UserLoginCommandHandler(_userManagerMock.Object, _mediatorMock.Object, _unitOfWorkMock.Object, _userRefreshTokenRepositoryMock.Object);
        }

        [InlineData(true, UserState.Active)]
        [InlineData(false, UserState.Passive)]
        [InlineData(true, UserState.Passive)]
        [Theory]
        public async Task Handle_GivenInValidUser_ReturnsFailure(bool isDeleted, UserState userState)
        {
            // Arrange
            var user = new Core.Entities.User { Id = Guid.NewGuid(), Email = "test@example.com", IsDeleted = isDeleted, UserState = userState };
            var command = new UserLoginCommand { Email = "test@example.com", Password = "Password123" };
            var tokenDto = new TokenDto { AccessToken = "access_token", AccessTokenExpiration = DateTime.Now.AddMinutes(600), RefreshToken = "refresh_token", RefreshTokenExpiration = DateTime.Now.AddMinutes(1200) };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<Core.Entities.User>(), It.IsAny<string>())).ReturnsAsync(true);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(tokenDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Data is null);
            Assert.True(!result.ErrorDto.Errors.IsNullOrNotAny());
            Assert.Equal(404, result.HttpStatusCode);
        }
    }
}
