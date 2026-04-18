using AutoMapper;
using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpensesUseCase : IRegisterExpensesUseCase
    {
        private readonly IExpenseRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterExpensesUseCase(
            IExpenseRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponsesRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Expense>(request);

            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ResponsesRegisterExpenseJson>(entity);

        }

        private void Validate(RequestRegisterExpenseJson request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
