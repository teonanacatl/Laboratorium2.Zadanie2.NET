using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Laboratorium2.Zadanie2.NET;

public partial class MainWindow
{
    private double _result;
    private string _operation = "";

    public MainWindow()
    {
        var start = DateTime.Now;
        InitializeComponent();
        var end = DateTime.Now;
        var duration = end - start;
        
        if (duration.TotalSeconds > 0.05)
        {
            EventLog.WriteEntry("Application", "Initialization took too long: " + duration.TotalSeconds + " seconds", EventLogEntryType.Warning);
        }
    }

    private void NumberButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        ResultTextBox.Text += button.Content;
    }

    private void OperationButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (button.Content != null)
            {
                _operation = button.Content.ToString() ?? throw new InvalidOperationException();
            }
            else
            {
                _operation = string.Empty;
                Console.WriteLine("Błąd: Content przycisku jest null.");
            }

            if (double.TryParse(ResultTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                _result = result;
                ResultTextBox.Clear();
            }
            else
            {
                Console.WriteLine($"Błąd: Nie można przekształcić '{ResultTextBox.Text}' na typ double.");
            }
        }
        else
        {
            Console.WriteLine("Błąd: Sender nie jest przyciskiem.");
        }
    }
    
    private void EqualsButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(ResultTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double secondOperand))
        {
            _result = CalculateResult(secondOperand);
            ResultTextBox.Text = _result.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            MessageBox.Show($"Cannot parse '{ResultTextBox.Text}' as a double");
        }
    }

    private double CalculateResult(double secondOperand)
    {
        switch (_operation)
        {
            case "+":
                return _result + secondOperand;
            case "-":
                return _result - secondOperand;
            case "*":
                return _result * secondOperand;
            case "/":
                return Divide(secondOperand);
            default:
                throw new InvalidOperationException($"Invalid operation '{_operation}'");
        }
    }

    private double Divide(double secondOperand)
    {
        if (secondOperand != 0)
        {
            return _result / secondOperand;
        }

        MessageBox.Show("Cannot divide by zero");
        return _result;
    }
}