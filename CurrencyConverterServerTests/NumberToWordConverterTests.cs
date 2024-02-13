using Moq;
using CurrencyConverterServer;

namespace CurrencyConverterServerTests
{
   /// <summary>
   /// Contains unit tests for <see cref="NumberToWordConverter"/> class.
   /// </summary>
   [TestClass]
   public class NumberToWordConverterTests
   {
        /// <summary>
        /// The mock for <see cref="INumberValidator"/>
        /// </summary>
        private Mock<INumberValidator>? NumberValidatorMock;

        /// <summary>Gets the default sut.</summary>
        private NumberToWordConverter Sut
        {
            get
            {
                if(this.NumberValidatorMock == null)
                {
                    Assert.Fail($"The mock for {typeof(INumberValidator)} mustn't be null.");
                }

                return new NumberToWordConverter(this.NumberValidatorMock.Object);
            }
        }

        /// <summary>Initializes the tests.</summary>
        [TestInitialize]
      public void InitTests()
      {
         this.NumberValidatorMock = new Mock<INumberValidator>();
      }

      /// <summary>Checks conversion of value 0.</summary>
      [TestMethod]
      public void CheckZeroConversion()
      {
         // ARRANGE
         const uint TestValue = 0;

         // ACT
         string converted = Sut.Convert(TestValue);

         // ASSERT
         Assert.AreEqual("zero", converted, "Unexpected conversion of value 0.");
      }

      /// <summary>Checks conversion of one digit number excl. zero.</summary>
      [TestMethod]
      public void CheckOneDigitConversion()
      {
         // ARRANGE
         const uint TestValue1 = 1;
         const uint TestValue2 = 2;
         const uint TestValue3 = 3;
         const uint TestValue4 = 4;
         const uint TestValue5 = 5;
         const uint TestValue6 = 6;
         const uint TestValue7 = 7;
         const uint TestValue8 = 8;
         const uint TestValue9 = 9;

         // ACT & ASSERT
         Assert.AreEqual("one", Sut.Convert(TestValue1), $"Unexpected conversion of value {TestValue1}.");
         Assert.AreEqual("two", Sut.Convert(TestValue2), $"Unexpected conversion of value {TestValue2}.");
         Assert.AreEqual("three", Sut.Convert(TestValue3), $"Unexpected conversion of value {TestValue3}.");
         Assert.AreEqual("four", Sut.Convert(TestValue4), $"Unexpected conversion of value {TestValue4}.");
         Assert.AreEqual("five", Sut.Convert(TestValue5), $"Unexpected conversion of value {TestValue5}.");
         Assert.AreEqual("six", Sut.Convert(TestValue6), $"Unexpected conversion of value {TestValue6}.");
         Assert.AreEqual("seven", Sut.Convert(TestValue7), $"Unexpected conversion of value {TestValue7}.");
         Assert.AreEqual("eight", Sut.Convert(TestValue8), $"Unexpected conversion of value {TestValue8}.");
         Assert.AreEqual("nine", Sut.Convert(TestValue9), $"Unexpected conversion of value {TestValue9}.");
      }

      /// <summary>Checks conversion of special numbers between 11 and 19.</summary>
      [TestMethod]
      public void Check11To19Conversion()
      {
         // ARRANGE
         const uint TestValue11 = 11;
         const uint TestValue12 = 12;
         const uint TestValue13 = 13;
         const uint TestValue14 = 14;
         const uint TestValue15 = 15;
         const uint TestValue16 = 16;
         const uint TestValue17 = 17;
         const uint TestValue18 = 18;
         const uint TestValue19 = 19;

         // ACT & ASSERT
         Assert.AreEqual("eleven", Sut.Convert(TestValue11), $"Unexpected conversion of value {TestValue11}.");
         Assert.AreEqual("twelve", Sut.Convert(TestValue12), $"Unexpected conversion of value {TestValue12}.");
         Assert.AreEqual("thirteen", Sut.Convert(TestValue13), $"Unexpected conversion of value {TestValue13}.");
         Assert.AreEqual("fourteen", Sut.Convert(TestValue14), $"Unexpected conversion of value {TestValue14}.");
         Assert.AreEqual("fifteen", Sut.Convert(TestValue15), $"Unexpected conversion of value {TestValue15}.");
         Assert.AreEqual("sixteen", Sut.Convert(TestValue16), $"Unexpected conversion of value {TestValue16}.");
         Assert.AreEqual("seventeen", Sut.Convert(TestValue17), $"Unexpected conversion of value {TestValue17}.");
         Assert.AreEqual("eighteen", Sut.Convert(TestValue18), $"Unexpected conversion of value {TestValue18}.");
         Assert.AreEqual("nineteen", Sut.Convert(TestValue19), $"Unexpected conversion of value {TestValue19}.");
      }

      /// <summary>Checks conversion of special two digits numbers 20, 30, 40 ... 90.</summary>
      [TestMethod]
      public void CheckMultipleOf10Conversion()
      {
         // ARRANGE
         const uint TestValue20 = 20;
         const uint TestValue30 = 30;
         const uint TestValue40 = 40;
         const uint TestValue50 = 50;
         const uint TestValue60 = 60;
         const uint TestValue70 = 70;
         const uint TestValue80 = 80;
         const uint TestValue90 = 90;

         // ACT & ASSERT
         Assert.AreEqual("twenty", Sut.Convert(TestValue20), $"Unexpected conversion of value {TestValue20}.");
         Assert.AreEqual("thirty", Sut.Convert(TestValue30), $"Unexpected conversion of value {TestValue30}.");
         Assert.AreEqual("fourty", Sut.Convert(TestValue40), $"Unexpected conversion of value {TestValue40}.");
         Assert.AreEqual("fifty", Sut.Convert(TestValue50), $"Unexpected conversion of value {TestValue50}.");
         Assert.AreEqual("sixty", Sut.Convert(TestValue60), $"Unexpected conversion of value {TestValue60}.");
         Assert.AreEqual("seventy", Sut.Convert(TestValue70), $"Unexpected conversion of value {TestValue70}.");
         Assert.AreEqual("eighty", Sut.Convert(TestValue80), $"Unexpected conversion of value {TestValue80}.");
         Assert.AreEqual("ninety", Sut.Convert(TestValue90), $"Unexpected conversion of value {TestValue90}.");
      }

      /// <summary>Checks conversion of two digits numbers from [21;99] excluding step 10 numbers like 20, 30....</summary>
      [TestMethod]
      public void CheckTwoDigitsConversion()
      {
         Assert.AreEqual("twenty one", Sut.Convert(21), "Unexpected conversion of value 21.");
         Assert.AreEqual("ninety nine", Sut.Convert(99), "Unexpected conversion of value 99.");
      }

      /// <summary>Checks conversion of three digits numbers.</summary>
      [TestMethod]
      public void CheckThreeDigitsConversion()
      {
         Assert.AreEqual("one hundred", Sut.Convert(100), "Unexpected conversion of value 100.");
         Assert.AreEqual("two hundred", Sut.Convert(200), "Unexpected conversion of value 200.");
         Assert.AreEqual("three hundred", Sut.Convert(300), "Unexpected conversion of value 300.");
         Assert.AreEqual("four hundred", Sut.Convert(400), "Unexpected conversion of value 400.");
         Assert.AreEqual("five hundred", Sut.Convert(500), "Unexpected conversion of value 500.");
         Assert.AreEqual("six hundred", Sut.Convert(600), "Unexpected conversion of value 600.");
         Assert.AreEqual("seven hundred", Sut.Convert(700), "Unexpected conversion of value 700.");
         Assert.AreEqual("eight hundred", Sut.Convert(800), "Unexpected conversion of value 800.");
         Assert.AreEqual("nine hundred", Sut.Convert(900), "Unexpected conversion of value 900.");

         Assert.AreEqual("two hundred one", Sut.Convert(201), "Unexpected conversion of value 201.");
         Assert.AreEqual("three hundred ninety", Sut.Convert(390), "Unexpected conversion of value 390.");
         Assert.AreEqual("four hundred fifty six", Sut.Convert(456), "Unexpected conversion of value 456.");

         Assert.AreEqual("nine hundred ninety nine", Sut.Convert(999), "Unexpected conversion of value 999.");
      }

      /// <summary>Checks conversion of numbers in range [1000;999999].</summary>
      [TestMethod]
      public void CheckThousandConversion()
      {
         Assert.AreEqual("one thousand", Sut.Convert(1000), "Unexpected conversion of value 1000.");
         Assert.AreEqual("ten thousand", Sut.Convert(10000), "Unexpected conversion of value 10000.");
         Assert.AreEqual("one hundred thousand", Sut.Convert(100000), "Unexpected conversion of value 100000.");

         Assert.AreEqual("nine thousand nine hundred ninety nine", Sut.Convert(9999), "Unexpected conversion of value 9 999.");
         Assert.AreEqual("ninety nine thousand nine hundred ninety nine", Sut.Convert(99999), "Unexpected conversion of value 99 999.");
         Assert.AreEqual("nine hundred ninety nine thousand nine hundred ninety nine", Sut.Convert(999999), "Unexpected conversion of value 999 999.");

         Assert.AreEqual("one thousand one", Sut.Convert(1001), "Unexpected conversion of value 1001.");
         Assert.AreEqual("one thousand eleven", Sut.Convert(1011), "Unexpected conversion of value 1011.");
         Assert.AreEqual("one thousand one hundred eleven", Sut.Convert(1111), "Unexpected conversion of value 1111.");

         Assert.AreEqual("thirty three thousand ninety nine", Sut.Convert(33099), "Unexpected conversion of value 33 099.");
      }

      /// <summary>Checks conversion of numbers in range [1000000;999999999].</summary>
      [TestMethod]
      public void CheckMillionConversion()
      {
         Assert.AreEqual("one million", Sut.Convert(1000000), "Unexpected conversion of value 1 000 000.");
         Assert.AreEqual("ten million", Sut.Convert(10000000), "Unexpected conversion of value 10 000 000.");
         Assert.AreEqual("one hundred million", Sut.Convert(100000000), "Unexpected conversion of value 100 000 000.");

         Assert.AreEqual("nine million nine hundred ninety nine thousand nine hundred ninety nine", Sut.Convert(9999999), "Unexpected conversion of value 9 999 999.");
         Assert.AreEqual("ninety nine million nine hundred ninety nine thousand nine hundred ninety nine", Sut.Convert(99999999), "Unexpected conversion of value 99 999 999.");
         Assert.AreEqual("nine hundred ninety nine million nine hundred ninety nine thousand nine hundred ninety nine", Sut.Convert(999999999), "Unexpected conversion of value 999 999 999.");

         Assert.AreEqual("one million one", Sut.Convert(1000001), "Unexpected conversion of value 1 000 001.");
         Assert.AreEqual("one million eleven", Sut.Convert(1000011), "Unexpected conversion of value 1 000 011.");
         Assert.AreEqual("one million one hundred eleven", Sut.Convert(1000111), "Unexpected conversion of value 1 000 111.");
         Assert.AreEqual("one million one thousand one hundred eleven", Sut.Convert(1001111), "Unexpected conversion of value 1 001 111.");
         Assert.AreEqual("one million eleven thousand one hundred eleven", Sut.Convert(1011111), "Unexpected conversion of value 1 0011 111.");
         Assert.AreEqual("one million one hundred eleven thousand one hundred eleven", Sut.Convert(1111111), "Unexpected conversion of value 1 111 111.");

         Assert.AreEqual("two million ninety nine", Sut.Convert(2000099), "Unexpected conversion of value 2 000 099.");
      }

      /// <summary>Verifies that an exception is thrown if the value is out range (too large).</summary>
      [TestMethod]
      public void CheckTooLargeValue()
      {
         // ARRANGE
         const uint Max = 999999999;
         const uint TestValue = Max + 1;

         // ASSERT
         Assert.ThrowsException<ArgumentOutOfRangeException>(() => Sut.Convert(TestValue), "too large");
      }
   }
}
