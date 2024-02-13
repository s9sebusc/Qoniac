
namespace CurrencyConverterServer
{
    /// <summary>
    /// Interface for conversions of a currency to it's word representation.
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Converts the given amount of specific currency to its word representation.
        /// </summary>
        /// <param name="currencyAmout">The currency amount.</param>
        /// <returns>Currency amount in words</returns>
        string Convert(double currencyAmount);
    }
}
