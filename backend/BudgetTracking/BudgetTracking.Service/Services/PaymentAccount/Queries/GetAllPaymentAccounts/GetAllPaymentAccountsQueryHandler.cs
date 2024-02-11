﻿using BudgetTracking.Common.Result;
using BudgetTracking.Core.ContextAccessor;
using BudgetTracking.Core.Repositories;
using BudgetTracking.Data.Extensions.Collection;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.PaymentAccount.Queries.GetAllPaymentAccounts
{
    public class GetAllPaymentAccountsQueryHandler : IRequestHandler<GetAllPaymentAccountsQuery, Result<GetAllPaymentAccountsQueryResult>>
    {
        private readonly ISecurityContextAccessor _contextAccessor;
        private readonly IPaymentAccountRepository _paymentAccountRepository;

        public GetAllPaymentAccountsQueryHandler(ISecurityContextAccessor contextAccessor, IPaymentAccountRepository paymentAccountRepository)
        {
            _contextAccessor = contextAccessor;
            _paymentAccountRepository = paymentAccountRepository;
        }

        public async Task<Result<GetAllPaymentAccountsQueryResult>> Handle(GetAllPaymentAccountsQuery request, CancellationToken cancellationToken)
        {
            var paymentsByUser = await _paymentAccountRepository.GetAllPaymentAccountsByUser(_contextAccessor.UserId)
                .Select(x => new PaymentModel
                {
                    Title = x.Title,
                    Amount = x.Amount,
                    CurrencyCode = x.CurrencyCode,
                    Description = x.Description,
                    PaymentType = x.PaymentType
                })
                .ToListAsync();

            if (paymentsByUser.IsNullOrNotAny())
                return Result<GetAllPaymentAccountsQueryResult>.Error("Ödeme hesabı bulunamadı!", (int)HttpStatusCode.NotFound);

            GetAllPaymentAccountsQueryResult result = new()
            {
                Payments = paymentsByUser,
                TotalAmount = paymentsByUser.Sum(x => x.Amount)
            };

            return Result<GetAllPaymentAccountsQueryResult>.Success(result, (int)HttpStatusCode.OK);
        }
    }
}
