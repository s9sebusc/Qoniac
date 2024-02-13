
namespace CurrencyConverterServer
{
    /// <summary>
    /// 
    /// </summary>
    public class DollarConverter : ICurrencyConverter
   {
        /// <summary>
        /// Converts the given amount of specific currency to its word representation.
        /// </summary>
        /// <param name="currencyAmount">The amount of currency to convert (e.g.33.45)</param>
        /// <returns>Currency amount in words</returns>
        public string Convert(double currencyAmount)
      {
         VerifyDecimalPoints(currencyAmount);

         NumberToWordConverter dollarConverter = new(new NumberValidator(new ValueRange { Min = 0, Max = 999999999 }));
         uint dollars = GetIntegerPart(currencyAmount);
         string dollarsInWords = dollarConverter.Convert(dollars);

         NumberToWordConverter centConverter = new(new NumberValidator(new ValueRange { Min = 0, Max = 99 }));
         uint cents = GetDecimalPart(currencyAmount);
         string centsInWords = centConverter.Convert(cents);

         string dollarCurrency = $"{dollarsInWords} {(dollarsInWords == "one" ? "dollar" : "dollars")}";
         string centCurrency = centsInWords == "zero" ? string.Empty : $" and {centsInWords} {(centsInWords == "one" ? "cent" : "cents")}";
         string totalCurrencyInWords = $"{dollarCurrency}{centCurrency}";

         return totalCurrencyInWords;
      }

      /// <summary>Verifies that the number has max 2 decimal points.</summary>
      /// <param name="number">The number.</param>
      /// <exception cref="ConversionException">The number must not have more than 2 decimal points</exception>
      private static void VerifyDecimalPoints(double number)
      {
         string numberAsString = number.ToString();
         int decimalIndex = numberAsString.IndexOfAny(new[] { ',', '.' });

         if (decimalIndex != -1 && numberAsString.Length - decimalIndex > 3)
         {
            throw new ConversionException("The number must not have more than 2 decimal points");
         }
      }

        /// <summary>Extract the number of dollars (number before comma) from the given double value.</summary>
        /// <param name="number">The number. Must be positive.</param>
        /// <returns>The number of dollars.</returns>
        /// <exception cref="CurrencyConverterServer.ConversionException">The given number of dollars mustn't be negative.</exception>
        private static uint GetIntegerPart(double number)
      {
         if (number < 0)
         {
            throw new ConversionException("The given number mustn't be negative");
         }

         return (uint)Math.Floor(number);
      }

        /// <summary>Extract the number of cents (decimal part) from the given double value.</summary>
        /// <param name="number">The number. Shall be positive.</param>
        /// <returns>The number of dollars.</returns>
        /// <exception cref="CurrencyConverterServer.ConversionException">The given number of dollars mustn't be negative.</exception>
        private static uint GetDecimalPart(double number)
      {
         if (number < 0)
         {
            throw new ConversionException("The given number mustn't be negative");
         }

         uint cents = (uint)(number % 1 * 100);

         return cents;
      }
   }
}
