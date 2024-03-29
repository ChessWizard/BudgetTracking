﻿using BudgetTracking.Core.File;
using BudgetTracking.Data.Dto;
using BudgetTracking.Data.Extensions.Reflection;
using ClosedXML.Excel;
using System.Data;
using System.Reflection;

namespace BudgetTracking.Data.File
{
    public class ExcelService : IExcelService
    {
        /// <summary>
        /// Generic Excel Export Metodu
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="exportData"></param>
        public (byte[],string, string) Export<T>(IEnumerable<T> exportData, string exportableDataClassName)
        {
            var type = Type.GetType(exportableDataClassName);
            DataTable table = new DataTable(type.Name);

            PropertyInfo[] properties = type.GetProperties();
            foreach (var prop in properties)
            {
                 table.Columns.Add(ClassReflectionExtensions.GetDisplayName(prop.Name, type), Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in exportData)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                table.Rows.Add(values);
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(table);
            using MemoryStream stream = new();
            workbook.SaveAs(stream);
            return (stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }
    }
}
