
namespace CurrencyConverterServer
{
   /// <summary>
   /// The number to word converter for special two digit numbers:
   /// - 10...19
   /// - Multiple of 10 numbers in range [20;90]
   /// </summary>
   public class TwoDigitConverter
   {
        /// <summary>
        /// Converts a number from range [11;19] to its word representation
        /// </summary>
        /// <param name="numberWithTwoDigits">The number with two digits.</param>
        /// <returns>The word represenation of number</returns>
        public static string Convert11To19(string numberWithTwoDigits)
        {
            return numberWithTwoDigits switch
            {
                "10" => "ten",
                "11" => "eleven",
                "12" => "twelve",
                "13" => "thirteen",
                "14" => "fourteen",
                "15" => "fifteen",
                "16" => "sixteen",
                "17" => "seventeen",
                "18" => "eighteen",
                "19" => "nineteen",
                _ => throw new ArgumentOutOfRangeException("Invalid number: " + numberWithTwoDigits),
            };
      }

        /// <summary>
        /// Converts a number from range [20;90] in 10-steps to its word representation
        /// </summary>
        /// <param name="numberWithTwoDigits">The number with two digits.</param>
        /// <returns>The word represenation of number</returns>
        public static string Convert20To99(string numberWithTwoDigits)
        {
            return numberWithTwoDigits switch
            {
                "20" => "twenty",
                "30" => "thirty",
                "40" => "fourty",
                "50" => "fifty",
                "60" => "sixty",
                "70" => "seventy",
                "80" => "eighty",
                "90" => "ninety",
                _ => throw new ArgumentOutOfRangeException("Invalid number" + numberWithTwoDigits),
            };
        }
   }
}