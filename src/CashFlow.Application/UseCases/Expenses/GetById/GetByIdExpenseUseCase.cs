using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetByIdExpenseUseCase : IGetByIdExpenseUseCase
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public GetByIdExpenseUseCase(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expenseRepository.GetByIdAsync(id);

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}