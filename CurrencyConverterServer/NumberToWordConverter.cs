
namespace CurrencyConverterServer
{
   /// <summary>
   /// Class for conversion of a given unsigned integer number to the corresponding representation in words.
   /// E.g. "99" => "Ninety nine", "1234" => "One thousand two hundred thirty four"
   /// </summary>
   public class NumberToWordConverter
   {
      /// <summary>The number validator.</summary>
      private readonly INumberValidator numberValidator;

      /// <summary>Initializes a new instance of the <see cref="NumberToWordConverter" /> class.</summary>
      /// <param name="numberValidator">Used to validate the number to convert.</param>
      public NumberToWordConverter(INumberValidator numberValidator)
      {
         this.numberValidator = numberValidator;
      }

      /// <summary>Converts the given number to the corresponding representation in words.</summary>
      /// <param name="number">The number.</param>
      /// <returns></returns>
      public string Convert(uint number)
      {
         ValidationResult validationResult = this.numberValidator.ValidateRange(number);

         if (ValidationResult.Ok != validationResult)
         {
            throw new ConversionException(
               $"The validation of the number failed. Validation result: {validationResult}");
         }


         ValueType valueType = ValueQualifier.Qualify(number);

         return valueType switch
         {
             ValueType.Zero => "zero",
             ValueType.OneDigit => SpecialOneDigitsNumber[number],
             ValueType.TwoDigitSpecial => SpecialTwoDigitsNumber[number],
             ValueType.TwoDigits => Convert2DigitsNumber(number),
             ValueType.MultipleOfHundred => Convert3DigitsNumber(number),
             ValueType.MultipleOfThousand => Convert4To6DigitsNumber(number),
             ValueType.MultipleOfMillion => Convert7To9DigitsNumber(number),
                _ => throw new ArgumentOutOfRangeException(
                                  $"Can't determine the value type because the value is too large {number}"),
         };
      }

      /// <summary>A dictionary contains special group of one digit number with their word representation (zero isn't supported).</summary>
      private static readonly Dictionary<uint, string> SpecialOneDigitsNumber = new()
      {
         { 1, "one" },
         { 2, "two" },
         { 3, "three" },
         { 4, "four" },
         { 5, "five" },
         { 6, "six" },
         { 7, "seven"},
         { 8, "eight"},
         { 9, "nine"},
      };

      /// <summary>A dictionary contains special group of two digits number with their str</summary>
      private static readonly Dictionary<uint, string> SpecialTwoDigitsNumber = new()
      {
         { 10, "ten" },
         { 11, "eleven" },
         { 12, "twelve" },
         { 13, "thirteen" },
         { 14, "fourteen" },
         { 15, "fifteen" },
         { 16, "sixteen" },
         { 17, "seventeen"},
         { 18, "eighteen"},
         { 19, "nineteen"},
         { 20, "twenty"},
         { 30, "thirty"},
         { 40, "fourty"},
         { 50, "fifty"},
         { 60, "sixty"},
         { 70, "seventy"},
         { 80, "eighty"},
         { 90, "ninety"},
      };

      /// <summary>Convert a two digit number to word representation.</summary>
      /// <param name="number">The number with two digits. Multiple of 10 numbers or a number smaller than 21 are not allowed.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException">
      /// number - Only a number in range [21;99] allowed.
      /// or
      /// number - Multiple of 10 numbers are not allowed.
      /// </exception>
      private static string Convert2DigitsNumber(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 21, Max = 99 }, number);

         if (number % 10 == 0) // These numbers are handled in other method and must not occurs here
         {
            throw new ArgumentOutOfRangeException(nameof(number), number, "Multiple of 10 numbers are not allowed.");
         }

         uint secondDigit = number % 10;
         uint firstDigit = number - secondDigit;

         return $"{SpecialTwoDigitsNumber[firstDigit]} {SpecialOneDigitsNumber[secondDigit]}";
      }

      /// <summary>Convert a three digits number to word representation.</summary>
      /// <param name="number">The number with three digits.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">
      /// Thrown when the input number is not in the supported range or has an unsupported number of digits.
      /// </exception>
      private static string Convert3DigitsNumber(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 100, Max = 999 }, number);

         string firstDigit = ConvertHundreds(number);

         uint restOfNumber = number % 100;
         ValueType valueType = ValueQualifier.Qualify(restOfNumber);
         string restDigits = ConvertRestDigits(restOfNumber, valueType);

         return $"{firstDigit} {restDigits}".TrimEnd();
      }

      /// <summary>Convert number with 4...6 digits to its word representation.</summary>
      /// <param name="number">The number with 4...6 digits.</param>
      /// <returns>The word representation of the number</returns>
      /// <exception cref="ArgumentOutOfRangeException">
      /// Thrown when the input number is not in the supported range or has an unsupported number of digits.
      /// </exception>
      private static string Convert4To6DigitsNumber(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 1000, Max = 999999 }, number);

         string thousands = ConvertsThousands(number);

         uint restOfNumber = number % 1000;
         ValueType valueType = ValueQualifier.Qualify(restOfNumber);
         string restDigits = ConvertRestDigits(restOfNumber, valueType);

         return $"{thousands} {restDigits}".TrimEnd();
      }

      /// <summary>Convert number with 7...9 digits to its word representation.</summary>
      /// <param name="number">The number with 7...9 digits.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">
      /// Only numbers in range [1000000;999999999] are supported.
      /// or
      /// The number has unsupported number of digits.
      /// </exception>
      private static string Convert7To9DigitsNumber(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 1000000, Max = 999999999 }, number);

         string millions = ConvertsMillions(number);

         uint restOfNumber = number % 1000000;
         ValueType valueType = ValueQualifier.Qualify(restOfNumber);
         string restDigits = ConvertRestDigits(restOfNumber, valueType);

         return $"{millions} {restDigits}".TrimEnd();
      }

      /// <summary>Convert the first digit of the given 3-digit number to word representation (e.g. 123 => "one hundred", 405 => "four hundred").</summary>
      /// <param name="number">The number.</param>
      /// <returns>The hundred part of the given number number as word representation</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">number - Only numbers with three digits are supported.</exception>
      private static string ConvertHundreds(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 100, Max = 999 }, number);

         uint hundreds = (number - number % 100) / 100;
         string result = SpecialOneDigitsNumber[hundreds] + " hundred";

         return result;
      }

      /// <summary>Convert the thousand digits of the number to word representation (e.g. 100001: "one thousand").</summary>
      /// <param name="number">The number. Shall be in the range [1000;999999]</param>
      /// <returns>The thousand part of the given number number as word representation</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">Only numbers in range [1000;999999] are supported.</exception>
      private static string ConvertsThousands(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 1000, Max = 999999 }, number);

         uint thousand = (number - number % 1000) / 1000;
         string thousandInWords = Convert1To999(thousand);

         return $"{thousandInWords} thousand";
      }

      /// <summary>Convert the million digits of the number to word representation (e.g. 123000000: "one hundred twenty three million").</summary>
      /// <param name="number">The number. Shall be in the range [1000000;999999000]</param>
      /// <returns>The thousand part of the given number number as word representation</returns>
      /// <exception cref="System.ArgumentOutOfRangeException">Only numbers in range [1000000;999999999] are supported.</exception>
      private static string ConvertsMillions(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 1000000, Max = 999999999 }, number);

         uint millions = (number - number % 1000000) / 1000000;
         string millionInWords = Convert1To999(millions);

         return $"{millionInWords} million";
      }

        /// <summary>
        /// Convert1s the number 1...999 to its word representation.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The value isn't in the range [1;999].</exception>
        private static string Convert1To999(uint number)
      {
         CheckWhetherInRange(new ValueRange { Min = 1, Max = 999 }, number);

         ValueType valueType = ValueQualifier.Qualify(number);

         return valueType switch
         {
             ValueType.OneDigit => SpecialOneDigitsNumber[number],
             ValueType.TwoDigitSpecial => SpecialTwoDigitsNumber[number],
             ValueType.TwoDigits => Convert2DigitsNumber(number),
             ValueType.MultipleOfHundred => Convert3DigitsNumber(number),
             _ => throw new ArgumentOutOfRangeException($"Unsupported value. Allowed is value in range [1;999], current value type is {valueType} and the value is {number}."),
         };
        }

      private static string ConvertRestDigits(uint restOfNumber, ValueType valueType)
      {
         return valueType switch
         {
             ValueType.Zero => string.Empty,
             ValueType.OneDigit or ValueType.TwoDigitSpecial or ValueType.TwoDigits or ValueType.MultipleOfHundred => Convert1To999(restOfNumber),
             ValueType.MultipleOfThousand => Convert4To6DigitsNumber(restOfNumber),
             _ => throw new ArgumentOutOfRangeException($"The number has an unsupported number of digits: {restOfNumber}"),
         };
      }

      /// <summary>Checks whether the number is in expected range.</summary>
      /// <param name="range">The value range for the number.</param>
      /// <param name="number">The number.</param>
      /// <exception cref="System.ArgumentNullException">The value range is null.</exception>
      /// <exception cref="System.ArgumentOutOfRangeException">
      /// Only numbers in range [{range.Min};{range.Max}] are supported.</exception>
      private static void CheckWhetherInRange(ValueRange range, uint number)
      {
         if (range == null)
         {
            throw new ArgumentNullException(nameof(range), "The value range is null.");
         }

         if (number < range.Min || number > range.Max)
         {
            throw new ArgumentOutOfRangeException(nameof(number), number, $"Only numbers in range [{range.Min};{range.Max}] are supported.");
         }
      }
   }
}
