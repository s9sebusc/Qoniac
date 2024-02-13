
namespace CurrencyConverterServer
{
   /// <summary>
   /// Validates the given number using following check:
   /// - Range check
   /// </summary>
   /// <seealso cref="DollarConverter.INumberValidator" />
   public class NumberValidator : INumberValidator
   {
      /// <summary>The value range with min und max boundaries.</summary>
      private readonly ValueRange range;

      /// <summary>Initializes a new instance of the <see cref="NumberValidator"/> class.</summary>
      public NumberValidator(ValueRange range)
      {
         this.range = range ?? throw new ArgumentNullException(nameof(range), "The value range mustn't be null.");
      }

      /// <summary>Validates that the number is in the allowed range.</summary>
      /// <param name="number">The number.</param>
      /// <returns>The result of value range validation.</returns>
      public ValidationResult ValidateRange(uint number)
      {
         if (number > this.range.Max)
         {
            return ValidationResult.TooLarge;
         }

         if (number < this.range.Min)
         {
            return ValidationResult.TooSmall;
         }

         return ValidationResult.Ok;
      }
   }
}
