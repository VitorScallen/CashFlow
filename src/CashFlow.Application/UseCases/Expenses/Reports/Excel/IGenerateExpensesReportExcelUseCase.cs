namespace CashFlow.Application.UseCases.Reports.Excel;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> ExecuteAsync(DateOnly month);
}