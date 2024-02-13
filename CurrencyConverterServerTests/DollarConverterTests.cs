using CurrencyConverterServer;

namespace CurrencyConverterServerTests
{
    /// <summary>
    /// Contains unit tests for <see cref="DollarConverter"/> class.
    /// </summary>
    [TestClass]
   public class DollarConverterTests
   {
        /// <summary>
        /// Gets a test instance of <see cref="DollarConverter"/>.
        /// </summary>
        private static DollarConverter Sut => new();


      /// <summary>Test the conversion of zero values.</summary>
      [TestMethod]
      public void ZeroDollarAndZeroCent()
      {
         Assert.AreEqual("zero dollars", Sut.Convert(0.00), "Invalid conversion of 0.00");
         Assert.AreEqual("zero dollars", Sut.Convert(0.0), "Invalid conversion of 0.0");
         Assert.AreEqual("zero dollars", Sut.Convert(0), "Invalid conversion of 0");
         Assert.AreEqual("zero dollars", Sut.Convert(-0), "Invalid conversion of -0");
      }

      /// <summary>Test the conversion of 0 dollar and X cents.</summary>
      [TestMethod]
      public void ZeroDollar()
      {
         Assert.AreEqual("zero dollars and one cent", Sut.Convert(0.01), "Invalid conversion of 0.01");
         Assert.AreEqual("zero dollars and ten cents", Sut.Convert(0.1), "Invalid conversion of 0.1");
         Assert.AreEqual("zero dollars and ninety nine cents", Sut.Convert(0.99), "Invalid conversion of 0.99");
      }

      /// <summary>Test the conversion of X dollar.</summary>
      [TestMethod]
      public void DollarOnly()
      {
         Assert.AreEqual("one dollar", Sut.Convert(1), "Invalid conversion of 1");
         Assert.AreEqual(
             "nine hundred ninety nine million nine hundred ninety nine thousand nine hundred ninety nine dollars",
             Sut.Convert(999999999),
             "Invalid conversion of 999999999");

      }

      /// <summary>Verifies that an exception is thrown for negative amount of dollars/cents.</summary>
      [TestMethod]
      public void NegativeCurrency()
      {
         Assert.ThrowsException<ConversionException>(() => Sut.Convert(-100), "mustn't be negative");
         Assert.ThrowsException<ConversionException>(() => Sut.Convert(-1.99), "mustn't be negative");
      }

      /// <summary>Verifies that an exception is thrown for a number with more than two decimal points.</summary>
      [TestMethod]
      public void TooManyDecimalPoints()
      {
         Assert.ThrowsException<ConversionException>(() => Sut.Convert(1.998), "must not have more than 2 decimal points");
         Assert.ThrowsException<ConversionException>(() => Sut.Convert(99.001), "must not have more than 2 decimal points");
         Assert.ThrowsException<ConversionException>(() => Sut.Convert(0.0090), "must not have more than 2 decimal points");
      }
   }
}
