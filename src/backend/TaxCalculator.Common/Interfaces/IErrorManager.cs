using TaxCalculator.Common.Models;

namespace TaxCalculator.Common.Interfaces
{
    public interface IErrorManager<T> where T : Enum
    {
        ApiException BuildException(T errorEnum, string? addtionalMessage = null);
        ApiException BuildFormattedException(T errorEnum, string? addtionalMessage = null, params object?[] args);
    }
}
