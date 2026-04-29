using CashFlow.Communication.Register;

namespace CashFlow.Application.UseCases.Expenses;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}