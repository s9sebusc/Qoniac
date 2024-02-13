
namespace CurrencyConverterServer
{
   /// <summary>
   /// This class contains tools to determine the <see cref="ValueType"/> of a number.
   /// </summary>
   public class ValueQualifier
   {
      /// <summary>Qualifies the specified number.</summary>
      /// <param name="number">The number.</param>
      /// <returns>
      /// The <see cref="ValueType"/> of the number.
      /// For number > 999999999 a value type 'Unknown' will be returned.
      /// </returns>
      public static ValueType Qualify(uint number)
      {
         if (number == 0)
         {
            return ValueType.Zero;
         }

         if (number <= 9)
         {
            return ValueType.OneDigit;
         }

         if (number<= 99)
         {
            return DetermineTypeOfTwoDigitsNumber(number);
         }

         if (number <= 999)
         {
            return ValueType.MultipleOfHundred;
         }

         if (number<= 999999)
         {
            return ValueType.MultipleOfThousand;
         }

         if (number <= 999999999)
         {
            return ValueType.MultipleOfMillion;
         }

         return ValueType.Unknown;
      }

      /// <summary>Determines a special type of two digits number.</summary>
      /// <returns>The value type.</returns>
      private static ValueType DetermineTypeOfTwoDigitsNumber(uint number)
      {
         if (number >= 10 && number <= 19) // 10...19
         {
            return ValueType.TwoDigitSpecial;
         }
         else if (number >= 20 && number <= 90 && number % 10 == 0) // 20, 30, 40...90
         {
            return ValueType.TwoDigitSpecial;
         }
         else
         {
            return ValueType.TwoDigits;
         }
      }
   }
}
