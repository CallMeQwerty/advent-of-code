using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_22 : ISolution
    {
        public async Task Solve()
        {
            await Console.Out.WriteLineAsync("Solving Day 22");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "22.txt");

            string content = File.ReadAllText(filePath);
            Console.WriteLine("File content: ");
            Console.WriteLine(content);
        }        
    }
}
