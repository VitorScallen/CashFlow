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
        public async Task<IActionResult> Register(
            [FromServices] IRegisterExpensesUseCase registerExpensesUseCase,
            [FromBody] RequestExpenseJson registerExpenseJson)
        {
            var response = await registerExpensesUseCase.Execute(registerExpenseJson);

            return Created(string.Empty, response);
        }
    }
}
