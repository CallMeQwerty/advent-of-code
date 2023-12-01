using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_01 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 1");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "01.txt");

            string[] lines = File.ReadAllLines(filePath);

            // Part 1
            int result = 0;
            
            foreach (var line in lines)
            {
                string firstNumber = "";
                string lastNumber = "";

                bool isFirst = true;

                foreach (var letter in line)
                {
                    if (int.TryParse(letter.ToString(), out int number))
                    {
                        firstNumber = isFirst ? letter.ToString() : firstNumber;
                        lastNumber = letter.ToString();

                        isFirst = false;
                    }
                }

                result += int.Parse(firstNumber + lastNumber);
            }

            Console.Out.WriteLine($"Part 1: {result}");
            
            // Part 2
            result = 0;
            List<string> spelledNumbers = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            foreach (var line in lines)
            {
                // Find spelled numbers and their index              
                int firstSpelledNumberIndex = int.MaxValue;
                int lastSpelledNumberIndex = int.MinValue;

                string firstSpelledNumber = "";
                string lastSpelledNumber = "";

                for (int i = 0; i < spelledNumbers.Count; i++)
                {
                    int firstIndex = line.IndexOf(spelledNumbers[i]);
                    int lastIndex = line.LastIndexOf(spelledNumbers[i]);

                    if (firstIndex != -1 && lastIndex != -1)
                    {
                        if (firstIndex < firstSpelledNumberIndex)
                        {
                            firstSpelledNumberIndex = firstIndex;
                            firstSpelledNumber = (i + 1).ToString();
                        }

                        if (lastIndex > lastSpelledNumberIndex)
                        {
                            lastSpelledNumberIndex = lastIndex;
                            lastSpelledNumber = (i + 1).ToString();
                        }
                    }
                }

                // Find number digits and their index
                int firstNumberIndex = int.MaxValue;
                int lastNumberIndex = int.MinValue;

                string firstNumber = "";
                string lastNumber = "";                

                bool isFirst = true;

                for (int i = 0; i < line.Length; i++)
                {
                    if (int.TryParse(line[i].ToString(), out int number))
                    {
                        if (isFirst)
                        {
                            firstNumber = line[i].ToString();
                            firstNumberIndex = i;
                            isFirst = false;
                        }
                        
                        lastNumber = line[i].ToString();
                        lastNumberIndex = i;
                        
                    }
                }

                string realFirstNumber = firstSpelledNumberIndex < firstNumberIndex ? firstSpelledNumber : firstNumber;
                string realLastNumber = lastSpelledNumberIndex > lastNumberIndex ? lastSpelledNumber : lastNumber;
            
                result += int.Parse(realFirstNumber + realLastNumber);
            }

            Console.Out.WriteLine($"Part 2: {result}");
        }        
    }
}
