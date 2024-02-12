using BudgetTracking.Core.File.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Core.File
{
    public interface IFileService
    {
        (byte[],string,  string) ExportFile<TData>(ExportFileType fileType, IEnumerable<TData> exportData, string exportableDataClassName);
    }
}
