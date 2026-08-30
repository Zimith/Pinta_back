namespace Pinta.Domain.Exceptions;

public class InvalidCredentialsException : BusinessException
{
    public InvalidCredentialsException(string message)
        : base(message)
    {
    }
}
