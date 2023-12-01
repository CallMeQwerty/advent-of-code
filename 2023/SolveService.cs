using AoC23.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23
{
    internal class SolveService
    {
        private ISolution currentDaySolution;

        public SolveService(int number)
        {
            currentDaySolution = CreateInstance(number);
        }

        private ISolution CreateInstance(int dayNumber)
        {
            string className = $"Day_{dayNumber:00}";
            Type type = Type.GetType($"AoC23.Solutions.{className}");
            return (ISolution)Activator.CreateInstance(type);
        }

        public void Solve()
        {
            currentDaySolution.Solve();
        }
    }
}
