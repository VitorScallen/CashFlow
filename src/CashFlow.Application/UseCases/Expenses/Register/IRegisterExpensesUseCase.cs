using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpensesUseCase
    {
        Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request);
    }
}
