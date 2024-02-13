using CurrencyConverterServer;

namespace CurrencyConverterServerTests
{
   /// <summary>
   /// Contains unit class for <see cref="NumberValidator"/> class.
   /// </summary>
   [TestClass]
   public class NumberValidatorTests
   {
      /// <summary>Checks the validation of too large value fails.</summary>
      [TestMethod]
      public void CheckTooLargeValue()
      {
         // ARRANGE
         ValueRange testRange = new() { Min = 0, Max = 99 };
         uint testValue = testRange.Max + 1;

         // ACT & ASSERT
         INumberValidator sut = new NumberValidator(testRange);
         Assert.AreEqual(ValidationResult.TooLarge, sut.ValidateRange(testValue), "A too large value mustn't pass the validation.");
      }

      /// <summary>Checks the validation of too small value fails.</summary>
      [TestMethod]
      public void CheckTooSmallValue()
      {
         // ARRANGE
         ValueRange testRange = new() { Min = 1, Max = 999999 };
         uint testValue = testRange.Min - 1;

         // ACT & ASSERT
         INumberValidator sut = new NumberValidator(testRange);
         Assert.AreEqual(ValidationResult.TooSmall, sut.ValidateRange(testValue), "A too small value mustn't pass the validation.");
      }

      /// <summary>Checks that minimum allowed number passes the validation.</summary>
      [TestMethod]
      public void CheckMinValue()
      {
         // ARRANGE
         ValueRange testRange = new() { Min = 0, Max = 999999 };
         uint testValue = testRange.Min;

         // ACT & ASSERT
         INumberValidator sut = new NumberValidator(testRange);
         Assert.AreEqual(ValidationResult.Ok, sut.ValidateRange(testValue), "The minimum allowed value shall pass the validation.");
      }

      /// <summary>Checks that maximum allowed number passes the validation.</summary>
      [TestMethod]
      public void CheckMaxValue()
      {
         // ARRANGE
         ValueRange testRange = new() { Min = 0, Max = 999999999 };
         uint testValue = testRange.Max;

         // ACT & ASSERT
         INumberValidator sut = new NumberValidator(testRange);
         Assert.AreEqual(ValidationResult.Ok, sut.ValidateRange(testValue), "The maximum allowed value shall pass the validation.");
      }
   }
}
