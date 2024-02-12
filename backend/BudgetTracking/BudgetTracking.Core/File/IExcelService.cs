namespace BudgetTracking.Core.File
{
    public interface IExcelService
    {
        (byte[], string, string) Export<TData>(IEnumerable<TData> exportData, string exportableDataClassName);
    }
}
