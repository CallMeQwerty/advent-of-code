using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_03 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 3");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "03.txt");

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

            for (int i = 0; i < lines.Length; i++)
            {
                string currentNumber = "";
                int startIndex = -1;
                int lastIndex = -1;

                // Each char in line
                for (int j = 0; j < lines[i].Length; j++)
                {
                    bool isLastCharacter = j + 1 == lines[i].Length;

                    if (char.IsDigit(lines[i][j]))
                    {
                        // If digit is first
                        if (currentNumber == "")
                        {
                            startIndex = j;
                        }

                        currentNumber += lines[i][j];

                        if (isLastCharacter)
                        {
                            if (!string.IsNullOrEmpty(currentNumber))
                            {
                                lastIndex = j;

                                bool hasSymbolAbove = false;
                                bool hasSymbolBefore = false;
                                bool hasSymbolAfter = false;
                                bool hasSymbolUnder = false;

                                // Check above
                                if (i - 1 != -1)
                                {
                                    int start = startIndex - 1;
                                    int end = j;

                                    // If left wall
                                    if (start == -1)
                                    {
                                        start++;
                                    }

                                    // If right wall
                                    if (end == lines[i].Length)
                                    {
                                        end--;
                                    }

                                    for (int k = start; k <= end; k++)
                                    {
                                        if (lines[i - 1][k] != '.')
                                        {
                                            hasSymbolAbove = true;
                                        }
                                    }
                                }

                                // Check under
                                if (i + 1 != lines.Length)
                                {
                                    int start = startIndex - 1;
                                    int end = j;

                                    // If left wall
                                    if (start == -1)
                                    {
                                        start++;
                                    }

                                    // If right wall
                                    if (end == lines[i].Length)
                                    {
                                        end--;
                                    }

                                    for (int k = start; k <= end; k++)
                                    {
                                        if (lines[i + 1][k] != '.')
                                        {
                                            hasSymbolUnder = true;
                                        }
                                    }
                                }

                                // Check before                        
                                if (startIndex - 1 != -1)
                                {
                                    if (lines[i][startIndex - 1] != '.')
                                    {
                                        hasSymbolBefore = true;
                                    }
                                }

                                // Check after
                                if (lastIndex + 1 != lines[i].Length)
                                {
                                    if (lines[i][lastIndex + 1] != '.')
                                    {
                                        hasSymbolAfter = true;
                                    }
                                }

                                if (hasSymbolAbove || hasSymbolBefore || hasSymbolAfter || hasSymbolUnder)
                                {
                                    if (int.TryParse(currentNumber, out int parsedNumber))
                                    {
                                        result += parsedNumber;

                                    }
                                }

                                currentNumber = "";
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentNumber))
                        {
                            lastIndex = j - 1;

                            bool hasSymbolAbove = false;
                            bool hasSymbolBefore = false;
                            bool hasSymbolAfter = false;
                            bool hasSymbolUnder = false;

                            // Check above
                            if (i - 1 != -1)
                            {
                                int start = startIndex - 1;
                                int end = j;

                                // If left wall
                                if (start == -1)
                                {
                                    start++;
                                }

                                // If right wall
                                if (end == lines[i].Length)
                                {
                                    end--;
                                }

                                for (int k = start; k <= end; k++)
                                {
                                    if (lines[i - 1][k] != '.')
                                    {
                                        hasSymbolAbove = true;
                                    }
                                }
                            }

                            // Check under
                            if (i + 1 != lines.Length)
                            {
                                int start = startIndex - 1;
                                int end = j;

                                // If left wall
                                if (start == -1)
                                {
                                    start++;
                                }

                                // If right wall
                                if (end == lines[i].Length)
                                {
                                    end--;
                                }

                                for (int k = start; k <= end; k++)
                                {
                                    if (lines[i + 1][k] != '.')
                                    {
                                        hasSymbolUnder = true;
                                    }
                                }
                            }

                            // Check before                        
                            if (startIndex - 1 != -1)
                            {
                                if (lines[i][startIndex - 1] != '.')
                                {
                                    hasSymbolBefore = true;
                                }
                            }

                            // Check after
                            if (lastIndex + 1 != lines[i].Length)
                            {
                                if (lines[i][lastIndex + 1] != '.')
                                {
                                    hasSymbolAfter = true;
                                }
                            }

                            if (hasSymbolAbove || hasSymbolBefore || hasSymbolAfter || hasSymbolUnder)
                            {
                                if (int.TryParse(currentNumber, out int parsedNumber))
                                {
                                    result += parsedNumber;

                                }
                            }

                            currentNumber = "";
                        }

                    }
                }
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '*')
                    {
                        int up = (i - 1) == -1 ? i : i - 1;
                        int down = (i + 1) == lines.Length ? i : i + 1;
                        int left = (j - 1) == -1 ? j : j - 1;
                        int right = (j + 1) == lines[i].Length ? j : j + 1;

                        List<string> numbersToMultiply = new List<string>();

                        for (int k = up; k <= down; k++)
                        {
                            for (int l = left; l <= right; l++)
                            {
                                if (char.IsDigit(lines[k][l]))
                                {
                                    string foundNumber = lines[k][l].ToString();

                                    // Search leftside
                                    int posLeft = l - 1;
                                    while (posLeft != -1 && char.IsDigit(lines[k][posLeft]))
                                    {
                                        foundNumber = lines[k][posLeft] + foundNumber;
                                        posLeft--;
                                    }

                                    // Search rightside
                                    int posRight = l + 1;
                                    while (posRight != lines[i].Length && char.IsDigit(lines[k][posRight]))
                                    {
                                        foundNumber += lines[k][posRight];
                                        posRight++;
                                        l++;
                                    }

                                    numbersToMultiply.Add(foundNumber);
                                }
                            }
                        }

                        if (numbersToMultiply.Count > 1)
                        {
                            int multiplyResult = 1;
                            foreach (string number in numbersToMultiply)
                            {
                                multiplyResult = multiplyResult * int.Parse(number);
                            }

                            result += multiplyResult;
                        }
                    }
                }
            }

            return result;
        }
    }
}
