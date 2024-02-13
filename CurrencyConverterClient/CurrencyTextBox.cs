using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverterClient
{
    /// <summary>
    /// A custom text box which allows enter a value in range [0;999999999.99]
    /// </summary>
    /// <seealso cref="System.Windows.Controls.TextBox" />
    public class CurrencyTextBox : TextBox
    {
        /// <summary>The regex for a comma separated format X,YY - where the x can be a value between 0 and 999999999. </summary>
        private readonly Regex Format = new(@"^\d{1,9}(\,\d{0,2})?$");

        /// <summary>
        /// Prevents showing entered text if it is inavlid. The new entered character will be ignored.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> that contains the event data.</param>
        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            string newText = GetNewText(e.Text);

            if (!this.IsValidInput(newText))
            {
                e.Handled = true;
            }

            base.OnPreviewTextInput(e);
        }

        /// <summary>
        /// Gets the current text incl. new added character.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The new text.</returns>
        private string GetNewText(string input)
        {
            string currentText = Text;

            StringBuilder sb = new();
            sb.Append(currentText[..SelectionStart]);
            sb.Append(input);
            sb.Append(currentText[(SelectionStart + SelectionLength)..]);

            return sb.ToString();
        }

        /// <summary>Determines whether the entered currency value is valid.</summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if [is valid input]; otherwise, <c>false</c>.</returns>
        private bool IsValidInput(string input)
        {
            return string.IsNullOrEmpty(input) || Format.IsMatch(input);

        }
        
        /// <summary>
        /// Set the entered value to 0 if invalid when focus lost.
        /// </summary>
        /// <param name="e">Provides data about the event.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);

            if (!IsValidInput(Text))
            {
                Text = "0";
            }
        }
    }
}
