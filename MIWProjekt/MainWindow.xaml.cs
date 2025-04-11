using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Globalization;
namespace MIWProjekt
{
    public struct graphPoint
    {
        public graphPoint(double x, int y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public int Y { get; set; }

        public override string ToString() => $"{X}, {Y}";
    }
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            GlobalVars.initialValues = FillValues("zadanie2.txt");
            InitializeComponent();           
            TaskViewWindow.Content = new Zadanie1();
        }
        private void ShowTask1_Click(object sender, RoutedEventArgs e)
        {
            TaskViewWindow.Content = new Zadanie1();
        }
        private void ShowTask2_Click(object sender, RoutedEventArgs e)
        {
            TaskViewWindow.Content = new Zadanie2();
        }
        private void ShowTask3_Click(object sender, RoutedEventArgs e)
        {
            TaskViewWindow.Content = new Zadanie3();
        }
        private static List<(double, double)> FillValues(string filePath)
        {
            List<(double, double)> result = new List<(double, double)>();
            double firstValue, secondValue;
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split(' ');

                    if (parts.Length == 2)
                    {
                        firstValue = Double.Parse(parts[0], format);
                        secondValue = Double.Parse(parts[1], format);
                        result.Add((firstValue, secondValue));
                    }
                }
            }
            else
            {
                MessageBox.Show("Brakuje pliku z danymi - powinien nazywać się zadanie2.txt", "Błąd! Brak pliku",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }
    }
}