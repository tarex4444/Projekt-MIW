using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIWProjekt
{

    class TestObject
    {
        private static Random rand = new Random();

        public const int ChromoPerX = 8;
        public const int TotalChromo = ChromoPerX * 2;
        public bool[] chromoSet = new bool[TotalChromo];
        public double X1, X2, FitValue;

        public TestObject()
        {
            for (int i = 0; i < TotalChromo; i++)
            {
                chromoSet[i] = rand.NextDouble() < 0.5;
            }
        }

        public TestObject(bool[] chromosomes)
        {
            this.chromoSet = chromosomes;
        }

        public void Eval()
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
            return new TestObject(chromoSet);
        }
    }
}

