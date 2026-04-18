using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsesRegisterExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterExpensesUseCase registerExpensesUseCase,
            [FromBody] RequestRegisterExpenseJson registerExpenseJson)
        {
            var response = await registerExpensesUseCase.Execute(registerExpenseJson);

            return Created(string.Empty, response);
        }
    }
}
