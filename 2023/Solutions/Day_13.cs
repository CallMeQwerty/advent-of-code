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
    public class Day_13 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 13");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "13.txt");

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

            List<string> pattern = new();
            foreach (string line in lines)
            {
                // If full pattern found
                if (string.IsNullOrEmpty(line)) 
                {         
                    // Check vertical
                    int vertical = CheckVertical(pattern);
                    if (vertical != 0) 
                    {
                        result += vertical;
                    }

                    // Check horizontal
                    int horizontal = CheckHorizontal(pattern);
                    if (horizontal != 0)
                    {
                        result += horizontal;
                    }

                    pattern.Clear();
                }
                else
                {
                    pattern.Add(line);
                }                
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            List<string> pattern = new();
            foreach (string line in lines)
            {
                // If full pattern found
                if (string.IsNullOrEmpty(line))
                {
                    // Check vertical
                    int vertical = CheckVertical(pattern);
                    if (vertical != 0)
                    {
                        // Find smudge for horizontal
                        bool foundAnotherReflection = false;
                        for (int i = 0; i < pattern.Count; i++)
                        {
                            for (int j = 0; j < pattern[0].Length; j++)
                            {             
                                if (pattern[i][j] == '.')
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, "#");
                                }
                                else
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, ".");
                                }
                                
                                int smudgedHorizontal = CheckHorizontal(pattern);
                                if (smudgedHorizontal != 0)
                                {
                                    result += smudgedHorizontal;
                                    foundAnotherReflection = true;
                                    break;
                                }
                                
                                int newSmudgedVertical = CheckVerticalUnique(pattern, vertical);
                                if (newSmudgedVertical != 0)
                                {
                                    result += newSmudgedVertical;
                                    foundAnotherReflection = true;
                                    break;
                                }

                                if (pattern[i][j] == '.')
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, "#");
                                }
                                else
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, ".");
                                }
                            }

                            if (foundAnotherReflection) break;
                        }
                    } 
                    else 
                    {
                        int horizontal = CheckHorizontal(pattern);

                        // Find smudge for vertical
                        bool foundAnotherReflection = false;
                        for (int i = 0; i < pattern.Count; i++)
                        {
                            for (int j = 0; j < pattern[0].Length; j++)
                            {
                                if (pattern[i][j] == '.')
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, "#");
                                }
                                else
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, ".");
                                }

                                int smudgedVertical = CheckVertical(pattern);
                                if (smudgedVertical != 0)
                                {
                                    result += smudgedVertical;
                                    foundAnotherReflection = true;
                                    break;
                                }

                                int newSmudgedHorizontal = CheckHorizontalUnique(pattern, horizontal);
                                if (newSmudgedHorizontal != 0)
                                {
                                    result += newSmudgedHorizontal;
                                    foundAnotherReflection = true;
                                    break;
                                }

                                if (pattern[i][j] == '.')
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, "#");
                                }
                                else
                                {
                                    pattern[i] = pattern[i].Remove(j, 1).Insert(j, ".");
                                }
                            }

                            if (foundAnotherReflection) break;
                        }
                    }

                    pattern.Clear();
                }
                else
                {
                    pattern.Add(line);
                }
            }

            return result;
        }     
        
        private int CheckVertical(List<string> pattern)
        {
            for (int i = 0; i <= pattern[0].Length - 2; i++)
            {
                if (pattern[0][i] == pattern[0][i + 1])
                {
                    int countToSide = Math.Min(i + 1, pattern[0].Length - i - 1);
                    bool correct = true;

                    for (int j = 0; j < countToSide; j++)
                    {
                        for (int k = 0; k < pattern.Count; k++)
                        {
                            if (pattern[k][i - j] != pattern[k][i + 1 + j])
                            {
                                correct = false;
                                break;
                            }
                        }

                        if (!correct) break;
                    }

                    if (correct) return i + 1;
                }
            }

            return 0;
        }

        private int CheckHorizontal(List<string> pattern)
        {
            for (int i = 0; i <= pattern.Count - 2; i++)
            {
                if (pattern[i] == pattern[i + 1])
                {
                    int countToEdge = Math.Min(i + 1, pattern.Count - i - 1);
                    bool correct = true;

                    for (int j = 0; j < countToEdge; j++)
                    {
                        if (pattern[i - j] != pattern[i + 1 + j])
                        {
                            correct = false;
                            break;
                        }
                    }

                    if (correct) return (i + 1) * 100;
                }
            }

            return 0;
        }

        private int CheckVerticalUnique(List<string> pattern, int foundValue)
        {
            for (int i = 0; i <= pattern[0].Length - 2; i++)
            {
                if (pattern[0][i] == pattern[0][i + 1])
                {
                    int countToSide = Math.Min(i + 1, pattern[0].Length - i - 1);
                    bool correct = true;

                    for (int j = 0; j < countToSide; j++)
                    {
                        for (int k = 0; k < pattern.Count; k++)
                        {
                            if (pattern[k][i - j] != pattern[k][i + 1 + j])
                            {
                                correct = false;
                                break;
                            }
                        }

                        if (!correct) break;
                    }

                    int result = i + 1;
                    if (correct && result != foundValue) return result;
                }
            }

            return 0;
        }

        private int CheckHorizontalUnique(List<string> pattern, int foundValue)
        {
            for (int i = 0; i <= pattern.Count - 2; i++)
            {
                if (pattern[i] == pattern[i + 1])
                {
                    int countToEdge = Math.Min(i + 1, pattern.Count - i - 1);
                    bool correct = true;

                    for (int j = 0; j < countToEdge; j++)
                    {
                        if (pattern[i - j] != pattern[i + 1 + j])
                        {
                            correct = false;
                            break;
                        }
                    }

                    int result = (i + 1) * 100;
                    if (correct && result != foundValue) return result;
                }
            }

            return 0;
        }
    }
}
