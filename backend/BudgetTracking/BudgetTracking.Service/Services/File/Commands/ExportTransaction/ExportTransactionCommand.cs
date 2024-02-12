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
        // ProcessStartDate == null && ProcessEndDate == null && PaymentAccountId == null -> tüm hesaplara ait tüm kayıtlar getirilir
        // ProcessStartDate == null && ProcessEndDate == null && PaymentAccountId.HasValue -> ilgili hesaba ait tüm kayıtlar getirilir

        // ProcessStartDate == null && ProcessEndDate.HasValue -> En baştan belirtilen son tarihe kadar
        public DateOnly? ProcessStartDate { get; set; }

        // ProcessStartDate.HasValue && ProcessEndDate == null -> Belirtilen başlangıçtan en son tarihe kadar
        public DateOnly? ProcessEndDate { get; set; }

        public Guid? PaymentAccountId { get; set; }

        public ExportFileType ExportFileType { get; set; }
    }
}
