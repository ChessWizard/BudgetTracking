using BudgetTracking.Common.Result;
using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Enums;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Dto;
using BudgetTracking.Service.Services.Token.Commands.CreateToken;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Authentication.Commands.UserLogin
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<TokenDto>>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        public UserLoginCommandHandler(UserManager<Core.Entities.User> userManager, IMediator mediator, IUnitofWork unitofWork, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitofWork = unitofWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<Result<TokenDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (!CanUserLogIn(user))
                return Result<TokenDto>.Error("Bu emaile ait bir kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!checkPassword)
                return Result<TokenDto>.Error("Lütfen email veya şifre bilgisini tekrar kontrol ediniz!", (int)HttpStatusCode.BadRequest);

            var token = await _mediator.Send(new CreateTokenCommand { User = user });

            var userRefreshToken = await _userRefreshTokenRepository.GetRefreshTokenAsync(x => x.UserId == user.Id);

            if (userRefreshToken is null)
            {
                UserRefreshToken refreshToken = new()
                {
                    UserId = user.Id,
                    Code = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                };

                await _userRefreshTokenRepository.AddRefreshTokenAsync(refreshToken);
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                _userRefreshTokenRepository.UpdateRefreshTokenAsync(userRefreshToken);
            }

            await _unitofWork.SaveChangesAsync();
            return Result<TokenDto>.Success(token, (int)HttpStatusCode.OK);
        }

        private bool CanUserLogIn(Core.Entities.User user)
         => !(user is null || user.IsDeleted || user.UserState != UserState.Active);
    }
}
