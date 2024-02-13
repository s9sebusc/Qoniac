
namespace CurrencyConverterServer
{
   /// <summary>
   /// Defines the the type of values
   /// </summary>
   public enum ValueType
   {
      Unknown,

      // 0
      Zero,

      // Number with one digit but greater zero [1;9]
      OneDigit,

      // Special number with 2 digits from following ranges:
      // - [10;19]
      // - [20;90] with step 10 (e.g. 20, 30, 40...)
      TwoDigitSpecial,

      // Number with 2 digits [21;99] excluding multiple of ten numbers (21, 34, 44)
      TwoDigits,

      // Number with 3 digits [100;999]
      MultipleOfHundred,

      // Number with 4...6 digits [1000;999999]
      MultipleOfThousand,

      // Number with 7...9 digits [1000000;999999999]
      MultipleOfMillion
   }
}
