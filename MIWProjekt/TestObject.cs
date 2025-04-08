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
        public double X1 = 0, X2 = 0, X3 = 0, X4 = 0, FitValue;
        //nowy osobnik
        public TestObject(int ParametersCount, int BitPerParam, Random rand)
        {
            this.XCount = ParametersCount;
            this.ChromoPerX = BitPerParam;
            this.TotalChromo = ChromoPerX * XCount;
            this.chromoSet = new bool[TotalChromo];
            for (int i = 0; i < TotalChromo; i++)
            {
                chromoSet[i] = rand.NextDouble() < 0.5;
            }
        }
        //do klonowania
        public TestObject(bool[] chromosomes, int XCount, int ChromoPerX, double X1, double X2, double X3, double X4, double FitValue)
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
        }
        //do rozmnażania przez miłość - ZAWSZE PO UŻYĆ EVAL
        public TestObject( bool[] chromosomes) 
        {
            this.chromoSet = chromosomes;
        }


        public void Eval(int TaskNumber)
        {
            int maxInt, x1Chromes, x2Chromes, x3Chromes, x4Chromes;
            switch (TaskNumber)
            {
                case 1:
                        maxInt = (1 << ChromoPerX) - 1; //przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
                        x1Chromes = 0; x2Chromes = 0;
                        for (int i = 0; i < ChromoPerX; i++)
                        {
                            if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                            if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w x2Chromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie

                            X1 = 100.0 * x1Chromes / maxInt;
                            X2 = 100.0 * x2Chromes / maxInt;

                            FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);
                        }
                    break;
                case 2:
                    maxInt = (1 << ChromoPerX) - 1; //przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
                    x1Chromes = 0; x2Chromes = 0; x3Chromes = 0;
                    for (int i = 0; i < ChromoPerX; i++)
                    {
                        if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                        if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w xXChromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie
                        if (chromoSet[i + 2*ChromoPerX]) x3Chromes |= (1 << (ChromoPerX - 1 - i));

                        X1 = 3.0 * x1Chromes / maxInt;
                        X2 = 3.0 * x2Chromes / maxInt;
                        X3 = 3.0 * x3Chromes / maxInt;

                        FitValue = 0;
                    }
                    break;
                case 3:
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
        public TestObject Clone()
        {
            return new TestObject(chromoSet, XCount, ChromoPerX, X1, X2, X3, X4, FitValue);
        }
        
    }
}

