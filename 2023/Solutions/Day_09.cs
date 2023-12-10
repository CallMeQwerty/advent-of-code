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
    public class Day_09 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 9");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "09.txt");

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

            // Each history
            foreach (string line in lines)
            {
                // Fill in differences
                List<List<int>> historyValues = new();
                List<int> startingValues = line.Split(' ').Select(str => int.Parse(str)).ToList();
                historyValues.Add(startingValues);

                List<int> differences = startingValues;

                do
                {
                    List<int> newDifferences = new();

                    for (int i = 0; i < differences.Count - 1; i++)
                    {
                        newDifferences.Add(differences[i + 1] - differences[i]);
                    }

                    differences = newDifferences;
                    historyValues.Add(differences);

                } while (!differences.All(number => number == 0));

                // Calculate next value
                int newValue = 0;
                for (int i = historyValues.Count - 1; i >= 0; i--)
                {
                    historyValues[i].Add(newValue);

                    if ((i - 1) >= 0)
                    {
                        newValue = historyValues[i].Last() + historyValues[i - 1].Last();
                    }                    
                }

                result += newValue;                
            }
            
            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            // Each history
            foreach (string line in lines)
            {
                // Fill in differences
                List<List<int>> historyValues = new();
                List<int> startingValues = line.Split(' ').Select(str => int.Parse(str)).ToList();
                historyValues.Add(startingValues);

                List<int> differences = startingValues;

                do
                {
                    List<int> newDifferences = new();

                    for (int i = 0; i < differences.Count - 1; i++)
                    {
                        newDifferences.Add(differences[i + 1] - differences[i]);
                    }

                    differences = newDifferences;
                    historyValues.Add(differences);

                } while (!differences.All(number => number == 0));

                // Flip the values so beginning is now the end
                foreach (var calculatedDifferences in historyValues)
                {
                    calculatedDifferences.Reverse();
                }

                // Calculate next value
                int newValue = 0;
                for (int i = historyValues.Count - 1; i >= 0; i--)
                {
                    historyValues[i].Add(newValue);

                    if ((i - 1) >= 0)
                    {
                        newValue = historyValues[i - 1].Last() - historyValues[i].Last();
                    }
                }

                result += newValue;
            }

            return result;
        }                
    }
}
