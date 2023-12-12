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
    public class Day_11 : ISolution
    {
        private List<(int posX, int posY)> visitedPositions = new();

        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 11");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "11.txt");

            List<string> lines1 = File.ReadAllLines(filePath).ToList();
            List<string> lines2 = File.ReadAllLines(filePath).ToList();

            // Part 1
            int resultPart1 = SolvePart1(lines1);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            long resultPart2 = SolvePart2(lines2);
            Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private int SolvePart1(List<string> grid)
        {
            int result = 0;

            // Expand the grid
            int length = grid.Count;
            for (int i = 0; i < length; i++)
            {
                // Check if the row has any galaxy
                if (!grid[i].Contains('#'))
                {
                    grid.Insert(i, new string('.', grid[0].Length));
                    i++;
                    length = grid.Count;
                }
            }

            length = grid[0].Length;
            for (int i = 0; i < length; i++)
            {
                // Check if the collumn has any galaxy
                if (!grid.Any(row => row[i] == '#')) 
                {
                    grid = grid.Select(row => row.Insert(i, '.'.ToString())).ToList();
                    i++;
                    length = grid[0].Length;
                }
            }

            // Find in all the galaxy positions
            List<(int posX, int posY)> foundGalaxies = new();            
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == '#') foundGalaxies.Add((i, j));
                }
            }

            // Calculate distances
            for (int i = 0; i < foundGalaxies.Count; i++)
            {
                (int posX, int posY) current = foundGalaxies[i];

                // Add all positions to remaining
                for (int j = i + 1; j < foundGalaxies.Count; j++)
                {
                    int valueToAdd = Math.Abs(current.posX - foundGalaxies[j].posX) + Math.Abs(current.posY - foundGalaxies[j].posY);
                    result += valueToAdd;
                }
            }

            return result;
        }

        private long SolvePart2(List<string> grid)
        {
            long result = 0;

            // Find in all the galaxy positions
            List<(int posX, int posY)> foundGalaxies = new();
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == '#') foundGalaxies.Add((i, j));
                }
            }

            // Calculate distances
            for (int i = 0; i < foundGalaxies.Count; i++)
            {
                (int posX, int posY) current = foundGalaxies[i];

                // Add all positions to remaining
                for (int j = i + 1; j < foundGalaxies.Count; j++)
                {
                    int empty = 0;

                    int maxX = Math.Max(current.posX, foundGalaxies[j].posX);
                    int minX = Math.Min(current.posX, foundGalaxies[j].posX);

                    int maxY = Math.Max(current.posY, foundGalaxies[j].posY);
                    int minY = Math.Min(current.posY, foundGalaxies[j].posY);

                    for (int k = minX; k < maxX; k++)
                    {                        
                        if (!grid[k].Contains('#'))
                        {
                            empty++;
                        }
                    }

                    for (int k = minY; k < maxY; k++)
                    {
                        if (!grid.Any(row => row[k] == '#'))
                        {
                            empty++;
                        }
                    }

                    long valueToAdd = (Math.Abs(current.posX - foundGalaxies[j].posX) + Math.Abs(current.posY - foundGalaxies[j].posY) - empty) + ((empty) * 1000000);
                    result += valueToAdd;
                }
            }

            return result;
        }                
    }
}
