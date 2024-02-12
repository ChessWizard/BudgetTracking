using BudgetTracking.Core.Enums;
using BudgetTracking.Core.File.Enums;
using BudgetTracking.Data.Interfaces.Markup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.File.Commands.ExportFile
{
    public class ExportFileCommand : IRequest<(byte[], string, string)>
    {
        public ExportFileType ExportFileType { get; set; }

        public IEnumerable<IExportableData> ExportableData { get; set; }

        public string ExportableDataClassName { get; set; }
    }
}
