using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace AoC23.Solutions
{
    public class Day_12 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 12");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "12.txt");

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

            Parallel.ForEach(lines, line =>
            {
                string[] lineSplit = line.Split(' ');                

                // Split input
                string conditions = lineSplit[0];
                int[] groups = lineSplit[1].Split(',').Select(int.Parse).ToArray();
                
                string[] parts = conditions.Split('?');
                char[] possibleChars = { '.', '#' };
                int totalCombinations = (int)Math.Pow(possibleChars.Length, parts.Length - 1);

                // For each possible combination
                for (int i = 0; i < totalCombinations; i++)
                {
                    string combination = parts[0];
                    int temp = i;

                    // Fill the values
                    for (int j = 1; j < parts.Length; j++)
                    {
                        int index = temp % possibleChars.Length;
                        combination += possibleChars[index] + parts[j];
                        temp /= possibleChars.Length;
                    }

                    // Check if combination has correct number of groups
                    string[] combinationSplitted = combination.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (combinationSplitted.Length == groups.Length)
                    {
                        bool allCountsCorrect = true;

                        // For each correct size combinations
                        for (int j = 0; j < combinationSplitted.Length; j++)
                        {
                            // Check if combination size responds with group number
                            if (combinationSplitted[j].Length != groups[j])
                            {
                                allCountsCorrect = false;
                                break;
                            }
                        }

                        if (allCountsCorrect)
                        {
                            Interlocked.Increment(ref result);
                        }
                    }
                }
            });

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            // Working but incredibly slow - do not use :)
            /*
            foreach (string line in lines)
            {
                string[] lineSplit = line.Split(' ');

                // Split input
                string conditions = lineSplit[0];
                List<int> groups = lineSplit[1].Split(',').Select(int.Parse).ToList();

                // Unfold the inputs
                string unfoldedConditions = conditions;
                List<int> unfoldedGroups = new List<int>(groups);

                for (int i = 0; i < 4; i++)
                {
                    unfoldedConditions += $"?{conditions}";
                    unfoldedGroups.AddRange(groups);
                }

                string[] parts = unfoldedConditions.Split('?');
                char[] possibleChars = { '.', '#' };
                long totalCombinations = (long)Math.Pow(possibleChars.Length, parts.Length - 1);

                // For each possible combination
                for (long i = 0; i < totalCombinations; i++)
                {
                    string combination = parts[0];
                    long temp = i;

                    // Fill the values
                    for (int j = 1; j < parts.Length; j++)
                    {
                        long index = temp % possibleChars.Length;
                        combination += possibleChars[index] + parts[j];
                        temp /= possibleChars.Length;
                    }

                    // Check if combination has correct number of groups
                    string[] combinationSplitted = combination.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (combinationSplitted.Length == unfoldedGroups.Count)
                    {
                        bool allCountsCorrect = true;

                        // For each correct size combination
                        for (int j = 0; j < combinationSplitted.Length; j++)
                        {
                            // Check if combination size corresponds with group number
                            if (combinationSplitted[j].Length != unfoldedGroups[j])
                            {
                                allCountsCorrect = false;
                                break;
                            }
                        }

                        if (allCountsCorrect)
                        {
                            result++;
                        }
                    }
                }
            }
            */

            return result;
        }                
    }
}
