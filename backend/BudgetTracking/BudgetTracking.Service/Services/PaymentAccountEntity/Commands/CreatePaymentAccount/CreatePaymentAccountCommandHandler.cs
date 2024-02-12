using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Entities;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Core.UnitofWork;
using BudgetTracking.Data.Extensions.Collection;
using BudgetTracking.Service.Services.PaymentAccountEntity.Queries.GetAllPaymentAccounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.PaymentAccountEntity.Commands.CreatePaymentAccount
{
    public class CreatePaymentAccountCommandHandler : IRequestHandler<CreatePaymentAccountCommand, Result<Unit>>
    {
        private readonly ISecurityContextAccessor _contextAccessor;
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IPaymentAccountRepository _paymentAccountRepository;
        private readonly IUnitofWork _unitofWork;
        private readonly IMediator _mediator;

        public CreatePaymentAccountCommandHandler(ISecurityContextAccessor contextAccessor,
            UserManager<Core.Entities.User> userManager,
            IPaymentAccountRepository paymentAccountRepository,
            IUnitofWork unitofWork,
            IMediator mediator)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _paymentAccountRepository = paymentAccountRepository;
            _unitofWork = unitofWork;
            _mediator = mediator;
        }

        public async Task<Result<Unit>> Handle(CreatePaymentAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_contextAccessor.UserId.ToString());

            if (user is null)
                return Result<Unit>.Error("Kullanıcı bulunamadı!", (int)HttpStatusCode.NotFound);

            var paymentAccountsByUser = await _mediator.Send(new GetAllPaymentAccountsQuery());

            // eğer kullanıcının önceden yarattığı hesaplar varsa
            // yeni kaydedilecek olan hesabın isminin bunlardan bir tanesi olmaması gerektiği kontrol edilir
            // hesap ismi unique olmalıdır
            var payments = paymentAccountsByUser.Data?.Payments;
            if (!payments.IsNullOrNotAny())
            {
                var paymenTitles = payments.Select(x => x.Title)
                    .ToList();
                if (paymenTitles.Contains(request.Title))
                {
                    return Result<Unit>.Error("Girilen hesap ismi var olan hesaplardan birisi olamaz!", (int)HttpStatusCode.BadRequest);
                }
            }

            PaymentAccount paymentAccount = new()
            {
                CurrencyCode = request.CurrencyCode,
                Amount = request.InitialAmount,
                Description = request.Description,
                PaymentType = request.PaymentType,
                User = user,
                Title = request.Title,
            };

            await _paymentAccountRepository.AddPaymentAccountAsync(paymentAccount);
            await _unitofWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value, (int)HttpStatusCode.Created);
        }
    }
}
