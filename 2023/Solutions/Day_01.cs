using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_01 : ISolution
    {
        public async Task Solve()
        {
            await Console.Out.WriteLineAsync("Solving Day 1");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "01.txt");

            string content = await File.ReadAllTextAsync(filePath);

            Console.WriteLine("File content: ");
            Console.WriteLine(content);
        }        
    }
}
