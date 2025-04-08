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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
    }
}