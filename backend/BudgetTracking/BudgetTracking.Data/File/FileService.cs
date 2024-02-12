using BudgetTracking.Core.File;
using BudgetTracking.Core.File.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.File
{
    public class FileService : IFileService
    {
        private readonly IExcelService _excelService;

        public FileService(IExcelService excelService)
        {
            _excelService = excelService;
        }

        public (byte[], string, string) ExportFile<TData>(ExportFileType fileType, IEnumerable<TData> exportData, string exportableDataClassName)
        {
            switch(fileType)
            {
                case ExportFileType.Excel:
                    return _excelService.Export(exportData, exportableDataClassName);
                default:
                    return (null, "", "");
            }
        }
    }
}
