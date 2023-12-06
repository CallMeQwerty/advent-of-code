using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_06 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 6");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "06.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int resultPart1 = SolvePart1(lines);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            int resultPart2 = SolvePart2(lines);
            Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private int SolvePart1(string[] lines)
        {
            int result = 0;

            Regex regex = new Regex(@"\d+");                        
            int[] times = regex.Matches(lines[0]).Cast<Match>().Select(match => int.Parse(match.Value)).ToArray();
            int[] distances = regex.Matches(lines[1]).Cast<Match>().Select(match => int.Parse(match.Value)).ToArray();

            // For each race
            for (int i = 0; i < times.Length; i++)
            {
                int successCount = 0;

                // 1 ms till record time - 1
                for (int j = 1; j < times[i]; j++)
                {
                    if ((times[i] - j) * j > distances[i]) successCount++;
                }

                result = result == 0 ? successCount : result * successCount;
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            Regex regex = new Regex(@"\d+");
            long time = long.Parse(string.Concat(regex.Matches(lines[0]).Cast<Match>().Select(match => match.Value)));
            long distance = long.Parse(string.Concat(regex.Matches(lines[1]).Cast<Match>().Select(match => match.Value)));

            // 1 ms till record time - 1
            for (int j = 1; j < time; j++)
            {
                if ((time - j) * j > distance) result++;
            }

            return result;
        }
    }
}
