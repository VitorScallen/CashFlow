#region Uso do padrão AAA (Arrange, Act, Assert) para estruturar os testes unitários:
/*
* Arrange serve para configurar o ambiente de teste, como criar instâncias de objetos necessários.
* Act é onde a ação a ser testada é executada, como chamar um método ou função.
* Assert é onde você verifica se o resultado da ação corresponde ao esperado, usando asserções para validar os resultados. 
*/
#endregion
using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses
{
    public class ExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            //Arrange
            var validator = new ExpenseValidator();
            var request = new RequestRegisterExpenseJsonBuilder().Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("         ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            //Arrange
            var validator = new ExpenseValidator();
            var request = new RequestRegisterExpenseJsonBuilder().Build();
            request.Title = title;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            //Arrange
            var validator = new ExpenseValidator();
            var request = new RequestRegisterExpenseJsonBuilder().Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            //Arrange
            var validator = new ExpenseValidator();
            var request = new RequestRegisterExpenseJsonBuilder().Build();
            request.PaymentType = (PaymentTypeEnum)700;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {
            //Arrange
            var validator = new ExpenseValidator();
            var request = new RequestRegisterExpenseJsonBuilder().Build();
            request.Amount = amount;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
        }
    }
}
