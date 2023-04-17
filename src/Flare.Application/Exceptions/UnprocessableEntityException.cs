namespace Flare.Application.Exceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message): base(message) {}
}