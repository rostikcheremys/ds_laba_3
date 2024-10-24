using System;
using System.Threading;
using System.Windows;

namespace Program
{
    public partial class MainWindow : Window
    {
        delegate void FormatNumber(double number);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FormatButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(NumberInput.Text, out double number))
            {
                ResultText.Text = "";
                
                FormatNumber format = FormatNumberAsCurrency;
                
                format += FormatNumberWithCommas;
                format += FormatNumberWithTwoPlaces;

                Thread thread = new Thread(() => 
                {
                    format(number);
                });
                
                thread.Start();
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть правильне число.");
            }
        }

        private void FormatNumberAsCurrency(double number)
        {
            Dispatcher.Invoke(() => 
            {
                ResultText.Text += $"A Currency: {number:C}\n";
            });
        }

        private void FormatNumberWithCommas(double number)
        {
            Dispatcher.Invoke(() => 
            {
                ResultText.Text += $"With Commas: {number:N}\n";
            });
        }

        private void FormatNumberWithTwoPlaces(double number)
        {
            Dispatcher.Invoke(() => 
            {
                ResultText.Text += $"With 3 places: {number:0.###}\n";
            });
        }
    }
}