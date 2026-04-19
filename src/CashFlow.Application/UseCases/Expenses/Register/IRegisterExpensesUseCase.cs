using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpensesUseCase
    {
<<<<<<< HEAD
        Task<ResponsesRegisterExpenseJson> Execute(RequestRegisterExpenseJson request);
=======
        Task<ResponsesExpensesJson> Execute(RequestExpenseJson request);
>>>>>>> e74bd3660e971cf9960841b86672c81d49622655
    }
}
