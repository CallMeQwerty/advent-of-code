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
    public class Day_10 : ISolution
    {
        private List<(int posX, int posY)> visitedPositions = new();

        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 10");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "10.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int resultPart1 = SolvePart1(lines);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            int resultPart2 = SolvePart2(lines);
            Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private int SolvePart1(string[] grid)
        {
            int result = 0;

            // Find location of S
            (int startX, int startY) sPosition = (-1, -1);

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 'S')
                    {
                        sPosition = (j, i);
                    }
                }
            }

            // Find both connecting paths
            List<(int posX, int posY)> startingPaths = FindPathsFromStart(sPosition, grid);
            
            (int startX, int startY) pathOnePrev = sPosition;
            (int startX, int startY) pathTwoPrev = sPosition;

            (int startX, int startY) pathOne = startingPaths[0];
            (int startX, int startY) pathTwo = startingPaths[1];

            // Store all visited positions            
            visitedPositions.Add(pathOnePrev);
            visitedPositions.Add(pathTwoPrev);

            bool found = false;
            int counter = 1;

            // Iterate till the maximum distance is found
            while (!found)
            {
                (int startX, int startY) newPathOne = FindNextPosition(pathOnePrev, pathOne, grid);
                (int startX, int startY) newPathTwo = FindNextPosition(pathTwoPrev, pathTwo, grid);

                pathOnePrev = pathOne;
                pathTwoPrev = pathTwo;

                pathOne = newPathOne;
                pathTwo = newPathTwo;

                visitedPositions.Add(pathOnePrev);
                visitedPositions.Add(pathTwoPrev);

                if (visitedPositions.Contains(pathOne) && visitedPositions.Contains(pathTwo))
                {
                    found = true;
                    result = counter;
                }
                else
                {
                    counter++;
                }
            }
            
            return result;
        }

        // Using the list of visited postions from Part 1
        private int SolvePart2(string[] grid)
        {
            int result = 0;
                        
            return result;
        }

        private List<(int posX, int posY)> FindPathsFromStart((int posX, int posY) currentPosition, string[] grid)
        {
            List<(int posX, int posY)> foundPositions = new();

            // Check UP
            if (currentPosition.posY - 1 >= 0)
            {
                int x = currentPosition.posX;
                int y = currentPosition.posY - 1;

                if (grid[y][x] == '|' || grid[y][x] == '7' || grid[y][x] == 'F')
                {
                    foundPositions.Add((x, y));
                }
            }

            // Check DOWN
            if (currentPosition.posY + 1 < grid.Length)
            {
                int x = currentPosition.posX;
                int y = currentPosition.posY + 1;

                if (grid[y][x] == '|' || grid[y][x] == 'L' || grid[y][x] == 'J')
                {
                    foundPositions.Add((x, y));
                }
            }

            // Check LEFT
            if (currentPosition.posX - 1 >= 0)
            {
                int x = currentPosition.posX - 1;
                int y = currentPosition.posY;

                if (grid[y][x] == '-' || grid[y][x] == 'L' || grid[y][x] == 'F')
                {
                    foundPositions.Add((x, y));
                }
            }

            // Check RIGHT
            if (currentPosition.posX + 1 < grid[0].Length)
            {
                int x = currentPosition.posX + 1;
                int y = currentPosition.posY;

                if (grid[y][x] == '-' || grid[y][x] == 'J' || grid[y][x] == '7')
                {
                    foundPositions.Add((x, y));
                }
            }

            return foundPositions;
        }

        private (int posX, int posY) FindNextPosition((int posX, int posY) prevPosition, (int posX, int posY) currentPosition, string[] grid)
        {
            char currentChar = grid[currentPosition.posY][currentPosition.posX];

            switch (currentChar)
            {
                case '|':
                    if (prevPosition.posY == currentPosition.posY - 1) return (currentPosition.posX, currentPosition.posY + 1);
                    if (prevPosition.posY == currentPosition.posY + 1) return (currentPosition.posX, currentPosition.posY - 1);
                    break;
                case '-':
                    if (prevPosition.posX == currentPosition.posX - 1) return (currentPosition.posX + 1, currentPosition.posY);
                    if (prevPosition.posX == currentPosition.posX + 1) return (currentPosition.posX - 1, currentPosition.posY);
                    break;
                case 'L':
                    if (prevPosition.posY == currentPosition.posY - 1) return (currentPosition.posX + 1, currentPosition.posY);
                    if (prevPosition.posX == currentPosition.posX + 1) return (currentPosition.posX, currentPosition.posY - 1);
                    break;
                case 'J':
                    if (prevPosition.posY == currentPosition.posY - 1) return (currentPosition.posX - 1, currentPosition.posY);
                    if (prevPosition.posX == currentPosition.posX - 1) return (currentPosition.posX, currentPosition.posY - 1);
                    break;
                case '7':
                    if (prevPosition.posY == currentPosition.posY + 1) return (currentPosition.posX - 1, currentPosition.posY);
                    if (prevPosition.posX == currentPosition.posX - 1) return (currentPosition.posX, currentPosition.posY + 1);
                    break;
                case 'F':
                    if (prevPosition.posY == currentPosition.posY + 1) return (currentPosition.posX + 1, currentPosition.posY);
                    if (prevPosition.posX == currentPosition.posX + 1) return (currentPosition.posX, currentPosition.posY + 1);
                    break;
            }

            throw new Exception();
        }
    }
}
