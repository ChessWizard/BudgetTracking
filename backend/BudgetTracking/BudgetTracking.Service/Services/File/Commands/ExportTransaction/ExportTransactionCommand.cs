using BudgetTracking.Core.File.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.File.Commands.ExportTransaction
{
    public class ExportTransactionCommand : IRequest<(byte[], string, string)>
    {
        public DateOnly? ProcessStartDate { get; set; }

        public DateOnly? ProcessEndDate { get; set; }

        public Guid? PaymentAccountId { get; set; }

        public ExportFileType ExportFileType { get; set; }
    }
}
