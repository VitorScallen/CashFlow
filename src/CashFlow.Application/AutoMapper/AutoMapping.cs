<<<<<<< HEAD
﻿using AutoMapper;
=======
using AutoMapper;
>>>>>>> e74bd3660e971cf9960841b86672c81d49622655
using CashFlow.Communication.Register;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

<<<<<<< HEAD
namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestRegisterExpenseJson, Expense>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponsesRegisterExpenseJson>();
        }
    }
}
=======
namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponsesExpensesJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
    }
}
>>>>>>> e74bd3660e971cf9960841b86672c81d49622655
