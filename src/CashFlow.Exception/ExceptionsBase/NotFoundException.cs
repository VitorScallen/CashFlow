using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message)
    {
        _errors = [message];
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}