using BudgetTracking.Data.Interfaces.Markup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Dto
{
    public class ExportTransactionDto : IExportableData
    {
        // DisplayName değerleri Reflextion ile çekilerek excel üzerindeki sütun adları haline getirilecek
        [DisplayName("Kategori")]
        public string Category { get; set; }

        [DisplayName("Kayıt Zamanı")]
        public DateOnly CreatedDate { get; set; }

        [DisplayName("Para Birimi")]
        public string Currency { get; set; }

        [DisplayName("Harcama Tipi")]
        public string Expense { get; set; }

        [DisplayName("Fiyat")]
        public decimal Price { get; set; }
    }
}
