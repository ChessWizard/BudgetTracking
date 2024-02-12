using BudgetTracking.Core.File;
using BudgetTracking.Core.File.Enums;
using BudgetTracking.Service.Services.ExpenseEntity.Queries.GetExpensesByUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Services.File.Commands.ExportFile
{
    public class ExportFileCommandHandler : IRequestHandler<ExportFileCommand, (byte[], string, string)>
    {
        private readonly IFileService _fileService;

        public ExportFileCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<(byte[], string, string)> Handle(ExportFileCommand request, CancellationToken cancellationToken)
        {            
            var result = _fileService.ExportFile(request.ExportFileType, request.ExportableData, request.ExportableDataClassName);
            return result;
        }
    }
}
