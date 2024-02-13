using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CurrencyConverterClient
{
    /// <summary>
    /// The view model for the currency conversion <see cref="View"/>.
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class ViewModel : BaseViewModel, IDisposable
    {
        private string enteredCurrencyAmount = default!;
        private string convertedCurrency = default!;
        private readonly TcpClient? tcpClient;
        private readonly NetworkStream? networkStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary> 
        public ViewModel()
        {
            this.ConvertCommand = new RelayCommand(
                async () => await ExecuteConvertAsync(),
                _ => !string.IsNullOrEmpty(this.EnteredCurrencyAmount));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ViewModel"/> class.
        /// </summary>
        ~ViewModel()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the command for converting currency.
        /// </summary>
        public ICommand ConvertCommand { get; private set; }

        /// <summary>
        /// Gets or sets the entered currency amount.
        /// </summary>
        public string EnteredCurrencyAmount
        {
            get => this.enteredCurrencyAmount;
            set
            {
                if (this.enteredCurrencyAmount != value)
                {
                    this.enteredCurrencyAmount = value;
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the converted currency.
        /// </summary>
        public string ConvertedCurrency
        {
            get => this.convertedCurrency;
            set
            {
                if(this.convertedCurrency != value)
                {
                    this.convertedCurrency = value;
                }
                
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sends the data to be converted to server via TCP and get the converted currency from server.
        /// </summary>
        private async Task ExecuteConvertAsync()
        {
            try
            {
                this.ConvertedCurrency = "Connecting to server...";

                await Task.Delay(100); // Allow the UI to refresh before starting the connection process

                using TcpClient tcpClient = new("localhost", 8080);
                using NetworkStream stream = tcpClient.GetStream();

                // Send data for conversion to server
                byte[] buffer = Encoding.UTF8.GetBytes(this.EnteredCurrencyAmount);
                await stream.WriteAsync(buffer);

                // Get converted result from server
                buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                this.ConvertedCurrency = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Conversion failed. Check whether the converter server is available. Exception: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                this.ConvertedCurrency = string.Empty; // Clean up result field
            }
        }

        #region IDisposable Implementation

        /// <summary>
        /// The disposed flag
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    tcpClient?.Dispose();
                    networkStream?.Dispose();
                }

                // Dispose unmanaged resources
                disposed = true;
            }
        }
        #endregion
    }
}
