using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Register;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestExpenseJson registerExpenseJson)
        {
            var useCase = new RegisterExpensesUseCase();

            var response = useCase.Execute(registerExpenseJson);
            return Created(string.Empty, response);
        }
    }
}
