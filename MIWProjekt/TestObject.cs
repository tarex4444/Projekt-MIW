using OxyPlot.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIWProjekt
{

    class TestObject
    {
        public int XCount;
        public int ChromoPerX;
        public int TotalChromo;
        public bool[] chromoSet;
        public double X1 = 0, X2 = 0, X3 = 0, X4 = 0, X5 = 0, X6 = 0, X7 = 0, X8 = 0, X9 = 0, FitValue;
        //nowy osobnik
        public TestObject()
        {
            X1 = 1;
        }
        public TestObject(int ParametersCount, int BitPerParam, Random rand, int TaskNumber)
        {
            this.XCount = ParametersCount;
            this.ChromoPerX = BitPerParam;
            this.TotalChromo = ChromoPerX * XCount;
            this.chromoSet = new bool[TotalChromo];
            for (int i = 0; i < TotalChromo; i++)
            {
                chromoSet[i] = rand.NextDouble() < 0.5;
            }
            this.Eval(TaskNumber);
        }
        //do klonowania
        public TestObject(bool[] chromosomes, int XCount, int ChromoPerX, double X1, double X2, double X3, double X4, double FitValue, int TaskNumber)
        {
            this.XCount = XCount;
            this.ChromoPerX = ChromoPerX;
            this.TotalChromo = ChromoPerX * XCount;
            this.chromoSet = chromosomes;
            this.X1 = X1;
            this.X2 = X2;
            this.X3 = X3;
            this.X4 = X4;
            this.FitValue = FitValue;
            /*if (XCount == 0)
            {
                if(TaskNumber == 2)
                {
                    this.XCount = 3;
                    this.Eval(TaskNumber);
                }
            }
            if (ChromoPerX == 0)
            {
                if(TaskNumber == 2)
                {
                    this.ChromoPerX = chromoSet.Length / this.XCount;
                    this.Eval(TaskNumber);
                }
            }*/
        }
        //do rozmnażania przez miłość - ZAWSZE PO UŻYCIU DAĆ EVAL
        public TestObject( bool[] chromosomes, int TaskNumber) 
        {
            this.chromoSet = chromosomes;
            if (TaskNumber == 1)
            {
                this.XCount = 2;
            } else if (TaskNumber == 2)
            {
                this.XCount = 3;
            } else if (TaskNumber == 3)
            {
                this.XCount = 4;
            }
            this.ChromoPerX = chromosomes.Length / this.XCount;
            this.TotalChromo = chromosomes.Length;
            this.Eval(TaskNumber);
        }


        public void Eval(int TaskNumber)
        {
            int maxInt, x1Chromes, x2Chromes, x3Chromes, x4Chromes, x5Chromes, x6Chromes, x7Chromes, x8Chromes, x9Chromes;
            FitValue = 0;
            switch (TaskNumber)
            {
                case 1:
                    maxInt = (1 << ChromoPerX) - 1; //przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
                    x1Chromes = 0; x2Chromes = 0;
                    for (int i = 0; i < ChromoPerX; i++)
                    {
                        if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                        if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w xXChromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie                           
                    }
                    X1 = 100.0 * x1Chromes / maxInt;
                    X2 = 100.0 * x2Chromes / maxInt;

                    FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);
                    break;
                case 2:
                    maxInt = (1 << ChromoPerX) - 1; 
                    x1Chromes = 0; x2Chromes = 0; x3Chromes = 0;
                    List<double> functionValues = new List<double>(); 
                    for (int i = 0; i < ChromoPerX; i++)
                    {
                        if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                        if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); 
                        if (chromoSet[i + 2*ChromoPerX]) x3Chromes |= (1 << (ChromoPerX - 1 - i));                        
                    }

                    X1 = 3.0 * x1Chromes / maxInt;
                    X2 = 3.0 * x2Chromes / maxInt;
                    X3 = 3.0 * x3Chromes / maxInt;
                    foreach((double, double)value in GlobalVars.initialValues)
                    {
                        functionValues.Add(X1*Math.Sin(X2*value.Item1+X3));
                    }
                    for (int i = 0; i < functionValues.Count() - 1; i++)
                    {
                        FitValue += Math.Pow((functionValues[i] - GlobalVars.initialValues[i].Item2), 2);
                    }
                    break;
                case 3:
                    /*maxInt = (1 << ChromoPerX) - 1; 
                    x1Chromes = 0; x2Chromes = 0;
                    for (int i = 0; i < ChromoPerX; i++)
                    {
                        if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                        if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i));                          
                    }
                    X1 = (20.0 * x1Chromes / maxInt)-10;
                    X2 = (20.0 * x2Chromes / maxInt) - 10;
                    X3 = (20.0 * x3Chromes / maxInt) - 10;
                    X4 = (20.0 * x4Chromes / maxInt) - 10;
                    X5 = (20.0 * x5Chromes / maxInt) - 10;
                    X6 = (20.0 * x6Chromes / maxInt) - 10;
                    X7 = (20.0 * x7Chromes / maxInt) - 10;
                    X8 = (20.0 * x8Chromes / maxInt) - 10;
                    X9 = (20.0 * x9Chromes / maxInt) - 10;

                    FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);*/
                    break;
            }               
        }

        public void Mutate(double mutationRate, Random random)
        {
            for (int i = 0; i < chromoSet.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    chromoSet[i] = !chromoSet[i];
                }
            }
        }
        public TestObject Clone(int TaskNumber)
        {
            return new TestObject(chromoSet, XCount, ChromoPerX, X1, X2, X3, X4, FitValue, TaskNumber);
        }
    }
}

