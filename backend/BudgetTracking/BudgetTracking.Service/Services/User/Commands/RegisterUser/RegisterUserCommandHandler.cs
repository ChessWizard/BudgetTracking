using BudgetTracking.Common.Result;
using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Enums;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.User.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Unit>>
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterUserCommand> _validator;

        public RegisterUserCommandHandler(IUnitofWork unitofWork, UserManager<Core.Entities.User> userManager, IUserRepository userRepository, IValidator<RegisterUserCommand> validator)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<Result<Unit>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validations = _validator.Validate(request);
            if (!validations.IsValid)
                return Result<Unit>.Error(new ErrorResult(validations.Errors.Select(x => x.ErrorMessage).ToList(), true), (int)HttpStatusCode.BadRequest);

            var emailByUser = await _userManager.FindByEmailAsync(request.Email);

            if (emailByUser is not null)
                return Result<Unit>.Error("Bu email ile kayıtlı bir kullanıcı bulunmaktadır!", (int)HttpStatusCode.BadRequest);

            // Başta validasyonlardan geçmiş olarak gelen request verileri User nesnesi olarak oluşturulur
            // veritabanına bu User nesnesi kaydedilince register(kullanıcı kayıt işlemi) gerçekleştirilmiş olur
            Core.Entities.User user = new()
            {
                Email = request.Email,// kullanıcının girmiş olduğu email adresi
                NormalizedEmail = _userManager.NormalizeEmail(request.Email),// Identity kütüphanesi "eşsiz email kontrolünü" bu alan sayesinde yapar
                SecurityStamp = Guid.NewGuid().ToString(),
                UserState = UserState.Active,
            };

            // şifre alanı ise Identity kütüphanesi user nesnesini istediği için şifre oluşturma metodunda dışarıda yazılır
            // Identity kütüphanesinden gelen hashleme metoduyla veritabanına şifreyi kaydedeceğiz
            user.PasswordHash = _userManager.PasswordHasher
                .HashPassword(user, request.Password);

            // Artık tamamıyla user nesnemizi oluşturduk
            // veritabanına user'ı ekleyebiliriz
            await _userRepository.AddUserAsync(user);

            //var role = await SetUserRolesAsync(user);

            //await _context.UserRoles
            //        .AddAsync(role);

            // veritabanına eklenme işlemi yansıtılsın
            await _unitofWork.SaveChangesAsync();

            // tüm işlemler tamamlanınca mesaj ve status code dönsün
            return Result<Unit>.Success("Hesap açma işlemi başarılı!", (int)HttpStatusCode.Created);
        }
    }
}
