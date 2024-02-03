using BudgetTracking.Common.Result;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.Authentication.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Result<Unit>>
    {
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IUnitofWork _unitofWork;

        public RevokeRefreshTokenCommandHandler(IUserRefreshTokenRepository userRefreshTokenRepository, IUnitofWork unitofWork)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _unitofWork = unitofWork;
        }

        public async Task<Result<Unit>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _userRefreshTokenRepository.GetRefreshTokenAsync(x => x.Code == request.RefreshToken);

            if (refreshToken is null)
                return Result<Unit>.Error("Silinecek hesap bulunamadı!", (int)HttpStatusCode.NotFound);

            await _userRefreshTokenRepository.RemoveRefreshTokenAsync(refreshToken);
            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success("Hesaptan çıkış yapıldı!", (int)HttpStatusCode.OK);
        }
    }
}
