using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIWProjekt
{
    static class ObjectSelection
    {
        public static TestObject TournamentSelection(List<TestObject> generation, int tournamentSize, Random rand, int TaskNumber)
        {
            var tournament = new List<TestObject>();
            var selected = new TestObject();

            for (int i = 0; i < tournamentSize; i++)
            {
                int index = rand.Next(generation.Count);
                tournament.Add(generation[index]);
            }
            if (TaskNumber == 1)
            {
                selected = tournament.OrderByDescending(obj => obj.FitValue).First().Clone(TaskNumber);
                selected.Eval(TaskNumber);
            }
            else if (TaskNumber == 2) 
            {
                selected = tournament.OrderByDescending(obj => obj.FitValue).Last().Clone(TaskNumber);
                selected.Eval(TaskNumber);               
            }
            return selected;
        }
        public static List<TestObject> Crossbreed(TestObject dad, TestObject mum, Random rand, int TaskNumber)
        {
            dad.Eval(TaskNumber);
            mum.Eval(TaskNumber);
            bool[] fenotype1 = new bool[dad.TotalChromo];
            bool[] fenotype2 = new bool[dad.TotalChromo];
            List<TestObject> children = new List<TestObject>();
            var foo = dad.TotalChromo - 2;
            int cutoffPoint = rand.Next(foo);
            for(int i = 0; i < fenotype1.Length; i++)
            {
                if(i < cutoffPoint)
                {
                    fenotype1[i] = dad.chromoSet[i];
                } else
                {
                    fenotype1[i] = mum.chromoSet[i];
                }
            }
            
            for (int i = 0; i < fenotype2.Length; i++)
            {
                if (i < cutoffPoint)
                {
                    fenotype2[i] = mum.chromoSet[i];
                }
                else
                {
                    fenotype2[i] = dad.chromoSet[i];
                }
            }
            TestObject child1 = new TestObject(fenotype1, dad.XCount, dad.ChromoPerX, 0, 0, 0, 0, 0, TaskNumber);
            child1.Eval(TaskNumber);
            TestObject child2 = new TestObject(fenotype2, dad.XCount, dad.ChromoPerX, 0, 0, 0, 0, 0, TaskNumber);
            child2.Eval(TaskNumber);
            children.Add(child1);
            children.Add(child2);

            return children;
        }
    }
}
