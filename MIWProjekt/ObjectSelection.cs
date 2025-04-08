using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIWProjekt
{
    static class ObjectSelection
    {
        public static TestObject TournamentSelection(List<TestObject> generation, int tournamentSize, Random rand, int TaskNumber)
        {
            var tournament = new List<TestObject>();
            
            for (int i = 0; i < tournamentSize; i++)
            {
                int index = rand.Next(generation.Count);
                tournament.Add(generation[index]);
            }
            return tournament.OrderByDescending(obj => obj.FitValue).First().Clone();
        }
        public static List<TestObject> Crossbreed(TestObject dad, TestObject mum, Random rand, int TaskNumber)
        {
            bool[] fenotype = new bool[dad.TotalChromo];
            List<TestObject> children = new List<TestObject>();
            int cutoffPoint = rand.Next(fenotype.Count() - 2);
            for(int i = 0; i < fenotype.Length; i++)
            {
                if(i < cutoffPoint)
                {
                    fenotype[i] = dad.chromoSet[i];
                } else
                {
                    fenotype[i] = mum.chromoSet[i];
                }
            }
            TestObject child = new TestObject(fenotype);
            child.Eval(TaskNumber);
            children.Add(child);
            for (int i = 0; i < fenotype.Length; i++)
            {
                if (i < cutoffPoint)
                {
                    fenotype[i] = mum.chromoSet[i];
                }
                else
                {
                    fenotype[i] = dad.chromoSet[i];
                }
            }
            child.chromoSet = fenotype;
            child.Eval(TaskNumber);
            children.Add(child);

            return children;
        }
    }
}
