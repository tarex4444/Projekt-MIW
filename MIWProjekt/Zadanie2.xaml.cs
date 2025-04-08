using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
namespace MIWProjekt
{
    
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class Zadanie2 : UserControl
    {
        public PlotModel graph { get; set; }
        private const int PopSize = 13;
        private const int TournSize = 3;
        private const int PopIter = 200;
        private const int ParameterCount = 3;
        private const int BitsPerParam = 6;
        private const double MutRate = 0.20;
        private List<(double, double)> initialValues = new List<(double, double)>();
        private double best;
        private double avg;
        private graphPoint bestPoint = new graphPoint();
        private graphPoint avgPoint = new graphPoint();
        private List<graphPoint> bestList = new List<graphPoint>();
        private List<graphPoint> avgList = new List<graphPoint>();
        private static Random rand = new Random();

        private List<TestObject> popul = new List<TestObject>();

        public Zadanie2()
        {
            InitializeComponent();
            initialValues = FillValues("zadanie2.txt");
            Task2();
        }

        private void Task2()
        {
            for (int i = 0; i < PopSize; i++)
            {
                var obj = new TestObject(ParameterCount, BitsPerParam, rand);
                obj.Eval(2);
                popul.Add(obj);
            }
            best = popul.Max(o => o.FitValue);
            avg = popul.Average(o => o.FitValue);
            bestPoint = new graphPoint(best, 0);
            avgPoint = new graphPoint(avg, 0);
            bestList.Add(bestPoint);
            avgList.Add(avgPoint);
            DisplayStats("Początek");

            for (int iter = 1; iter <= PopIter; iter++)
            {
                List<TestObject> newPop = new();

                for (int i = 0; i < PopSize - 1; i++)
                {
                    var selected = ObjectSelection.TournamentSelection(popul, TournSize, rand, 2);
                    selected.Mutate(MutRate, rand);
                    selected.Eval(2);
                    newPop.Add(selected);
                }

                var elite = popul.OrderByDescending(p => p.FitValue).First();
                newPop.Add((TestObject)elite);

                popul = newPop;
                best = popul.Max(o => o.FitValue);
                avg = popul.Average(o => o.FitValue);
                bestPoint = new graphPoint(best, iter);
                avgPoint = new graphPoint(avg, iter);
                bestList.Add(bestPoint);
                avgList.Add(avgPoint);
                DisplayStats($"Iteracja {iter}");
            }
            DisplayGraph();
        }

        private void DisplayStats(string header)
        {
            OutputPanel.Children.Add(new TextBlock
            {
                Text = $"{header}: Najlepszy obiekt testowy: {best:F2}, Średnia obiektów: {avg:F2}, Rozmiar populacji: {popul.Count():F0}",
                FontSize = 14,
                Margin = new Thickness(20, 2, 20, 2)
            });
        }
        private void DisplayGraph()
        {

            graph = new PlotModel { Title = "Poziom Najlepszego obiektu testowego i ich średnia", };

            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -1,
                Maximum = 3
            };
            var bestSeries = new LineSeries
            {
                Title = "Najlepszy obiekt testowy",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green
            };
            var avgSeries = new LineSeries
            {
                Title = "Średnia obiektów testowych",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Blue
            };
            foreach (var point in bestList)
            {
                bestSeries.Points.Add(new DataPoint(point.Y, point.X));
            }
            foreach (var point in avgList)
            {
                avgSeries.Points.Add(new DataPoint(point.Y, point.X));
            }
            graph.Axes.Add(xAxis);
            graph.Series.Add(bestSeries);
            graph.Series.Add(avgSeries);
            DataContext = this;
        }
        private static List<(double, double)> FillValues(string filePath)
        {
            List<(double, double)> result = new List<(double, double)>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines) 
                {
                    if(string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split(' ');

                    if (parts.Length == 2 && double.TryParse(parts[0], out double firstValue) && double.TryParse(parts[1], out double secondValue)) 
                    {
                        result.Add((firstValue, secondValue));
                    }
                }
            } else
            {
                MessageBox.Show("Brakuje pliku z danymi - powinien nazywać się zadanie2.txt", "Błąd! Brak pliku", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }
    }
}
