using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using OxyPlot;
using OxyPlot.Series;
namespace MIWProjekt
{
    public struct graphPoint
    {
        public graphPoint(double x, int y)
        {
            this.x = y;
            this.y = y;
        }
        public double x { get; }
        public int y { get; }

        public override string ToString() => $"{x}, {y}";
    }
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class Zadanie1 : UserControl
    {
        public PlotModel graph { get; set; }
        private const int PopSize = 29;
        private const int TournSize = 3;
        private const int PopIter = 50;
        private const double MutRate = 0.20;

        private List<graphPoint> bestList = new List<graphPoint>();
        private List<graphPoint> avgList = new List<graphPoint>();
        private static Random rand = new Random();

        private List<TestObject> popul = new List<TestObject>();

        public Zadanie1()
        {
            InitializeComponent();
            Task1();
        }

        private void Task1()
        {
            graphPoint bestPoint = new graphPoint();
            graphPoint avgPoint = new graphPoint();
            for (int i = 0; i < PopSize ; i++)
            {
                var obj = new TestObject();
                obj.Eval();
                popul.Add(obj);
            }
            bestPoint = new graphPoint(popul.Max(o => o.FitValue), 0);
            avgPoint = new graphPoint(popul.Average(o => o.FitValue), 0);
            bestList.Add(bestPoint);
            avgList.Add(avgPoint);
            DisplayStats("Początek");

            for (int iter = 1; iter <= PopIter; iter++)
            {
                List<TestObject> newPop = new();

                for (int i = 0; i < PopSize - 1; i++)
                {
                    var selected = ObjectSelection.TournamentSelection(popul, TournSize);
                    selected.Mutate(MutRate, rand);
                    selected.Eval();
                    newPop.Add(selected);
                }

                var elite = popul.OrderByDescending(p => p.FitValue).First();
                newPop.Add((TestObject)elite);

                popul = newPop;
                bestPoint = new graphPoint(popul.Max(o => o.FitValue), iter);
                avgPoint = new graphPoint(popul.Average(o => o.FitValue), iter);
                bestList.Add(bestPoint);
                avgList.Add(avgPoint);
                DisplayStats($"Iteracja {iter}");
            }
            DisplayGraph();
        }

        private void DisplayStats(string header)
        {
            var best = popul.Max(o => o.FitValue);
            var avg = popul.Average(o =>  o.FitValue);
            OutputPanel.Children.Add(new TextBlock
            {
                Text = $"{header}: Najlepszy obiekt testowy: {best:F2}, Średnia obiektów: {avg:F2}, Rozmiar populacji: {popul.Count():F0}",
                FontSize = 14,
                Margin = new Thickness(20, 2, 20, 2)
            });
        }
        private void DisplayGraph()
        {
            
            graph = new PlotModel { Title = "Poziom Najlepszego obiektu testowego i ich średnia" };


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
                bestSeries.Points.Add(new DataPoint(point.x, point.y));
            }
            foreach(var point in avgList)
            {
                avgSeries.Points.Add(new DataPoint(point.x, point.y));
            }
            
            graph.Series.Add(bestSeries);
            graph.Series.Add(avgSeries);
            DataContext = this;
        }
    }
}
