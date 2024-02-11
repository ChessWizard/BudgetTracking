using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Unit>>
    {
        private readonly ISecurityContextAccessor _contextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IUnitofWork _unitofWork;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(ISecurityContextAccessor contextAccessor, UserManager<Core.Entities.User> userManager, IUnitofWork unitofWork, IUserRepository userRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _unitofWork = unitofWork;
            _userRepository = userRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.UserId.ToString());

            if (user is null)
                return Result<Unit>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            ConditionalUpdateProcesses(request, user);
            await _userRepository.UpdateUserAsync(user);
            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.NoContent);
        }

        private void ConditionalUpdateProcesses(UpdateUserCommand request, Core.Entities.User user)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Surname))
                user.Surname = request.Surname;
            if (request.BirthDate.HasValue)
                user.BirthDate = request.BirthDate.Value;
            if(request.GenderType.HasValue)
                user.GenderType = request.GenderType.Value;
        }
    }
}
