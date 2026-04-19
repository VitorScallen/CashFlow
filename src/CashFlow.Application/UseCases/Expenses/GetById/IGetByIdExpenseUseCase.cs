using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public interface IGetByIdExpenseUseCase
{
    Task<ResponsesExpensesJson> Execute(long id); 
}