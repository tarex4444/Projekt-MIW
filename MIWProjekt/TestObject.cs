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

        public void EvalTask1()
        {
            int maxInt = (1 << ChromoPerX) - 1; //Przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
            int x1Chromes = 0, x2Chromes = 0;
            for (int i = 0; i < ChromoPerX; i++)
            {
                if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w x2Chromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie

                X1 = 100.0 * x1Chromes / maxInt;
                X2 = 100.0 * x2Chromes / maxInt;

                FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);
            }
        }
        public void EvalTask2()
        {
            int maxInt = (1 << ChromoPerX) - 1; //Przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
            int x1Chromes = 0, x2Chromes = 0;
            for (int i = 0; i < ChromoPerX; i++)
            {
                if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w x2Chromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie

                X1 = 100.0 * x1Chromes / maxInt;
                X2 = 100.0 * x2Chromes / maxInt;

                FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);
            }
        }
        public void EvalTask3()
        {
            int maxInt = (1 << ChromoPerX) - 1; //Przesunięcie bitowe w lewo o 1 aby podac max mozliwych zapisanych liczb
            int x1Chromes = 0, x2Chromes = 0;
            for (int i = 0; i < ChromoPerX; i++)
            {
                if (chromoSet[i]) x1Chromes |= (1 << (ChromoPerX - 1 - i));
                if (chromoSet[i + ChromoPerX]) x2Chromes |= (1 << (ChromoPerX - 1 - i)); //czary bitowe - ustawianie bitów w x2Chromes tak żeby odpowiadały liczbie całkowitej ułożonej z bitów w chromosecie               
            }
            X1 = 100.0 * x1Chromes / maxInt;
            X2 = 100.0 * x2Chromes / maxInt;

            FitValue = Math.Sin(X1 * 0.05) + Math.Sin(X2 * 0.05) + 0.4 * Math.Sin(X1 * 0.15) * Math.Sin(X2 * 0.15);
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

