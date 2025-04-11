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
            
            Task2();
        }

        private void Task2()
        {
            for (int i = 0; i < PopSize; i++)
            {
                var obj = new TestObject(ParameterCount, BitsPerParam, rand, 2);
                obj.Eval(2);
                popul.Add(obj);
            }
            var childrenOfTheNewGen = new List<TestObject>();
            best = popul.Min(o => o.FitValue);
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
                    newPop.Add(ObjectSelection.TournamentSelection(popul, TournSize, rand, 2));
                }
                childrenOfTheNewGen.AddRange(ObjectSelection.Crossbreed(newPop[0], newPop[1], rand, 2));
                childrenOfTheNewGen.AddRange(ObjectSelection.Crossbreed(newPop[2], newPop[3], rand, 2));
                childrenOfTheNewGen.AddRange(ObjectSelection.Crossbreed(newPop[8], newPop[9], rand, 2));
                childrenOfTheNewGen.AddRange(ObjectSelection.Crossbreed(newPop[newPop.Count-1], newPop[newPop.Count - 2], rand, 2));
                foreach(var child in childrenOfTheNewGen)
                {
                    newPop.Add(child);
                }
                childrenOfTheNewGen.Clear();
                for (int i = 4; i < PopSize - 1; i++)
                {
                    newPop[i].Mutate(MutRate, rand);
                }
                var elite = popul.OrderByDescending(p => p.FitValue).Last();
                newPop.Add((TestObject)elite);               
                best = newPop.Min(o => o.FitValue);
                avg = newPop.Average(o => o.FitValue);
                bestPoint = new graphPoint(best, iter);
                avgPoint = new graphPoint(avg, iter);
                bestList.Add(bestPoint);
                avgList.Add(avgPoint);
                DisplayStats($"Iteracja {iter}");
                popul = newPop;
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
        
    }
}
