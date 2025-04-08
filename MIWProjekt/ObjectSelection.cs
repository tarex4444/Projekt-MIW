using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIWProjekt
{
    static class ObjectSelection
    {
        public static TestObject TournamentSelection(List<TestObject> generation, int tournamentSize, Random rand)
        {
            var tournament = new List<TestObject>();
            
            for (int i = 0; i < tournamentSize; i++)
            {
                int index = rand.Next(generation.Count);
                tournament.Add(generation[index]);
            }
            return tournament.OrderByDescending(obj => obj.FitValue).First().Clone();
        }
    }
}
