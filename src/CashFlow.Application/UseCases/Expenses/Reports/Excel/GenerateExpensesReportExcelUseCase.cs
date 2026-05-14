using System.Runtime.InteropServices;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;

    public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository)
    {
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
    }
    
    public async Task<byte[]> ExecuteAsync(DateOnly month)
    {
        var expenses = await _expensesReadOnlyRepository.FilterByMonthAsync(month);
        if (expenses.Count == 0)
        {
            return [];
        }
        
        var workBook = new XLWorkbook();
        workBook.Author = "Vitor";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Times New Roman";

        var worksheet = workBook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);
       
        using var stream = new MemoryStream();
        workBook.SaveAs(stream);
        return stream.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;
        
        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#987ACF");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

    }
}