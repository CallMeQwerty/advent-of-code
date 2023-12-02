using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public enum RGB
    {
        red,
        green,
        blue
    }

    public class Day_02 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 2");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "02.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int redLimit = 12;
            int greenLimit = 13;
            int blueLimit = 14;
            
            int result = 0;

            foreach (var line in lines)
            {
                string[] game = line.Split(": ");
                string[] gameSplitted = game[0].Split(" ");
                string[] cubePicks = game[1].Split("; ");

                int gameId = int.Parse(gameSplitted[1]);
                bool isPossible = true;

                // For each set of cubes
                foreach (var cubePick in cubePicks)
                {
                    string[] cubes = cubePick.Split(", ");

                    // For each color in set
                    foreach (var cube in cubes)
                    {
                        string[] cubeInfo = cube.Split(" ");

                        int number = int.Parse(cubeInfo[0]);
                        RGB color = Enum.Parse<RGB>(cubeInfo[1]);

                        switch (color)
                        {
                            case RGB.red:
                                if (number > redLimit) isPossible = false;
                                break;
                            case RGB.green:
                                if (number > greenLimit) isPossible = false;
                                break;
                            case RGB.blue:
                                if (number > blueLimit) isPossible = false;
                                break;
                        }
                    }
                }

                if (isPossible) result += gameId;  
            }

            Console.Out.WriteLine($"Part 1: {result}");

            // Part 2            
            result = 0;

            foreach (var line in lines)
            {
                string[] game = line.Split(": ");
                string[] cubePicks = game[1].Split("; ");

                int gameId = int.Parse(game[0].Split(" ")[1]);

                int redCount = 0;
                int greenCount = 0;
                int blueCount = 0;

                // For each set of cubes
                foreach (var cubePick in cubePicks)
                {
                    string[] cubes = cubePick.Split(", ");

                    // For each color in set
                    foreach (var cube in cubes)
                    {
                        string[] cubeInfo = cube.Split(" ");

                        int number = int.Parse(cubeInfo[0]);
                        RGB color = Enum.Parse<RGB>(cubeInfo[1]);

                        switch (color)
                        {
                            case RGB.red:
                                redCount = number > redCount ? number : redCount;
                                break;
                            case RGB.green:
                                greenCount = number > greenCount ? number : greenCount;
                                break;
                            case RGB.blue:
                                blueCount = number > blueCount ? number : blueCount;
                                break;
                        }
                    }
                }

                result += redCount * greenCount * blueCount;
            }

            Console.Out.WriteLine($"Part 2: {result}");
        }        
    }
}
