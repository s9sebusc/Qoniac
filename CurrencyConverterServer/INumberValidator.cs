
namespace CurrencyConverterServer
{
   /// <summary>
   /// Interface for validation of a number.
   /// </summary>
   public interface INumberValidator
   {
      /// <summary>Validates whether the number is in the allowed range.</summary>
      /// <param name="number">The number.</param>
      /// <returns>The result of value range validation.</returns>
      ValidationResult ValidateRange(uint number);
   }
}
