using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIWProjekt
{

    class TestObject
    {
        static Random rand = new Random();
        public List<bool> chromoSet = new List<bool>();
        public double FitValue;

        public TestObject(int MaxBits)
        {
            for (int i = 0; i < MaxBits; i++)
            {
                
            }
        }

        public TestObject(List<bool> chromosomes)
        {
            this.chromoSet = chromosomes;
        }

        public TestObject Clone()
        {
            return new TestObject(chromoSet);
        }
        static TestObject TournamentSelection(List<TestObject> generation, int tournamentSize)
        {
            var tournament = new List<TestObject>();
            for (int i = 0; i < tournamentSize; i++)
            {
                tournament.Add(generation[rand.Next(generation.Count)]);
            }
            return tournament.OrderByDescending(obj => obj.FitValue).First().Clone();
        }
    }
}

