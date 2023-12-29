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
    public class Day_14 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 14");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "14.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int resultPart1 = SolvePart1(lines);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            //int resultPart2 = SolvePart2(lines);
            //Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private int SolvePart1(string[] lines)
        {
            int result = 0;

            // Transpose input
            List<string> newLines = Enumerable.Range(0, lines[0].Length).Select(col => new string(lines.Select(row => row[col]).ToArray())).ToList();

            // Create tilted grid
            List<string> tiltedLines = new();
            string newLine;
            foreach (string line in newLines)
            {
                newLine = "";
                string[] splittedLine = line.Split('#');

                // For each found part
                for (int i = 0; i < splittedLine.Length; i++)
                {
                    int countRounded = splittedLine[i].Count(c => c == 'O');
                    int countSquare = splittedLine[i].Count(c => c == '.');

                    newLine += new string('O', countRounded);
                    newLine += new string('.', countSquare);

                    if (i != splittedLine.Length - 1) newLine += '#';
                }

                tiltedLines.Add(newLine);
            }

            // Calculate load
            foreach (var tiltedLine in tiltedLines)
            {
                for (int i = 0; i < tiltedLine.Length; i++)
                {
                    if (tiltedLine[i] == 'O')
                    {
                        result += tiltedLine.Length - i;
                    }
                }
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;
                       
            return result;
        }             
    }
}
