using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC23.Solutions
{
    public class Day_08 : ISolution
    {
        private string directions = "LRLRLLRRLLRRLRRLRRRLLRLRLRLRLRRLRRRLRLRRLRLLRRLLRLRRLRLRRLLRRRLRLRLRRRLRLLRRRLLLLLLRRRLRRLLLRRLRLRRLRRLRLRRLRRLLRRLRRRLRRRLLRLRLLLRRLLLRRLLRRLRLLRRRLRRRLRRRLRLRRLRRLLLRRRLRRLLRRLRRRLRLRLRRLRRLRRRLRRRLRLLLLRRRLRLRRRLRRRLLRLRRLRRLLRLLLRRLRLRRLRRRLRRRLRRRLLRRRLRLLRRRLRRRLRRRLRRRLRRLRRRLLRRLLRLRLRRRLRRRLRLRRRR";

        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 8");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "08.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int resultPart1 = SolvePart1(lines);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            long resultPart2 = SolvePart2(lines);
            Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private int SolvePart1(string[] lines)
        {
            int result = 0;

            string currentPoint = lines.First(line => line.StartsWith("AAA"));

            bool found = false;
            int i = 0;
            while (!found)
            {
                Match match = Regex.Match(currentPoint, @"\((\w+),\s*(\w+)\)");

                // Find the desired line
                if (directions[i] == 'L')
                {
                    currentPoint = lines.First(line => line.StartsWith(match.Groups[1].Value));
                } 
                else if (directions[i] == 'R')
                {                    
                    currentPoint = lines.First(line => line.StartsWith(match.Groups[2].Value));
                }

                i++;
                result++;

                // Check if end is found
                if (currentPoint.StartsWith("ZZZ"))
                {
                    found = true;
                }

                // Repeat the directions
                if (i == directions.Length)
                {
                    i = 0;
                }
            }

            return result;
        }

        private long SolvePart2(string[] lines)
        {
            long result = 0;

            List<string> currentPoints = lines.Where(line => line[2] == 'A').ToList();
            int[] steps = new int[currentPoints.Count];

            // For each starting line
            Parallel.For(0, currentPoints.Count, j =>
            {
                bool found = false;
                int i = 0;
                int counter = 0;
                while (!found)
                {
                    Match match = Regex.Match(currentPoints[j], @"\((\w+),\s*(\w+)\)");

                    // Find the desired line
                    if (directions[i] == 'L')
                    {
                        currentPoints[j] = lines.First(line => line.StartsWith(match.Groups[1].Value));
                    }
                    else if (directions[i] == 'R')
                    {
                        currentPoints[j] = lines.First(line => line.StartsWith(match.Groups[2].Value));
                    }

                    i++;
                    counter++;

                    // Check if end is found
                    if (currentPoints[j][2] == 'Z')
                    {
                        found = true;
                        steps[j] = counter;
                    }

                    // Repeat the directions
                    if (i == directions.Length)
                    {
                        i = 0;
                    }
                }
            });

            result = CalculateLCM(steps);

            return result;
        }

        #region Calculate LCM
        static long CalculateLCM(int[] values)
        {
            long lcm = values[0];

            for (int i = 1; i < values.Length; i++)
            {
                lcm = CalculateLCM(lcm, values[i]);
            }

            return lcm;
        }

        static long CalculateLCM(long a, long b)
        {
            return (a * b) / CalculateGCF(a, b);
        }

        static long CalculateGCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
        #endregion
    }
}
