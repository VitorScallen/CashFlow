using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpensesUseCase
    {
        public ResponsesRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            return new ResponsesRegisterExpenseJson()
            {
                Title = request.Title
            };

        }
    }
}
