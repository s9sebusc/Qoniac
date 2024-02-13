using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CurrencyConverterServer
{
    /// <summary>
    /// The server providing currency conversion via TCP.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            TcpListener tcpListener = CreateTcpListener();

            try
            {
                tcpListener.Start();
                HandleClientConnection(tcpListener);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured on server: {0}", e.Message);
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        /// <summary>
        /// Handles the communication with a client.
        /// </summary>
        /// <param name="tcpListener">The TCP listener.</param>
        /// <exception cref="System.ArgumentNullException">The TCP listener mustn't be null.</exception>
        internal static void HandleClientConnection(TcpListener tcpListener)
        {
            if (tcpListener == null)
            {
                throw new ArgumentNullException(nameof(tcpListener), "The TCP listener mustn't be null.");
            }

            while (true)
            {
                Console.Write("Waiting for a connection...");
                TcpClient client = tcpListener.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                NetworkStream clientStream = client.GetStream();

                string clientData = ReceiveData(clientStream);
                string convertedCurrency = ConvertCurrency(new DollarConverter(), clientData);
                SendData(clientStream, convertedCurrency);

                client.Close();
                Console.WriteLine("Data successfully transfered. Client disconnected.");
            }
        }

        /// <summary>
        /// Receives the data from client.
        /// </summary>
        /// <param name="clientStream">The client stream.</param>
        /// <return>The data received from client. The data is converted ASCII.</returns>
        internal static string ReceiveData(NetworkStream clientStream)
        {
            if (clientStream == null)
            {
                throw new ArgumentNullException(nameof(clientStream), "The client network stream mustn't be null.");
            }

            byte[] buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
            string clientData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Data received from client: [{clientData}].");
            return clientData;
        }

        /// <summary>
        /// Sends the given data to client.
        /// </summary>
        /// <param name="clientStream">The client stream.</param>
        /// <param name="data">The data for client.</param>
        internal static void SendData(NetworkStream clientStream, string data)
        {
            if (clientStream == null)
            {
                throw new ArgumentNullException(nameof(clientStream), "The client network stream mustn't be null.");
            }

            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine($"Warning: the data to be sent to client is null or empty.");
                return;
            }

            byte[] response = Encoding.ASCII.GetBytes(data);
            clientStream.Write(response, 0, response.Length);
            Console.WriteLine($"Sent converted data to client: [{data}].");
        }

        /// <summary>
        /// Creates a TCP listener for localhost.
        /// </summary>
        /// <returns></returns>
        internal static TcpListener CreateTcpListener()
        {
            const int Port = 8080;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            return new TcpListener(localAddr, Port);
        }

        /// <summary>
        /// Converts the specific currency entered as text by user to its word representation.
        /// </summary>
        /// <param name="currencyConverter">The specific currency converter.</param>
        /// <param name="clientData">The user input of currency amount.</param>
        /// <returns>The converted currency in words.</returns>
        static string ConvertCurrency(ICurrencyConverter currencyConverter, string clientData)
        {
            if(currencyConverter == null)
            {
                throw new ArgumentNullException(nameof(currencyConverter), "The currency converter instance is null.");
            }
            string convertedCurrency;

            if (double.TryParse(clientData, out double currencyAmount))
            {
                convertedCurrency = currencyConverter.Convert(currencyAmount);
            }
            else
            {
                convertedCurrency = string.Empty;
                Console.WriteLine($"The conversion of '{clientData}' string to double value wasn't successful.");
            }

            return convertedCurrency;
        }
    }
}

