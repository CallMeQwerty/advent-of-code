using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_04 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 4");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "04.txt");

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

            foreach( string line in lines )
            {
                int cardResult = 2;
                int power = 0;

                int colonIndex = line.IndexOf(':');
                int pipeIndex = line.IndexOf('|');

                string winningNumbersString = line.Substring(colonIndex + 1, pipeIndex - colonIndex - 1).Trim();
                string myNumbersString = line.Substring(pipeIndex + 1).Trim();

                string[] winningNumbers = winningNumbersString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] myNumbers = myNumbersString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string number in winningNumbers)
                {
                    if (myNumbers.Contains(number)) power++;
                }

                if (power > 0)
                {
                    result += (int)Math.Pow(cardResult, power-1);
                }                
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            Dictionary<int, int> cardCount = new();
            for (int i = 0; i < lines.Length; i++)
            {
                cardCount[i] = 1;
            }            

            // For each line
            for (int i = 0; i < lines.Length; i++)
            {
                int j = 1;
                while (j <= cardCount[i])
                {
                    int colonIndex = lines[i].IndexOf(':');
                    int pipeIndex = lines[i].IndexOf('|');

                    string winningNumbersString = lines[i].Substring(colonIndex + 1, pipeIndex - colonIndex - 1).Trim();
                    string myNumbersString = lines[i].Substring(pipeIndex + 1).Trim();

                    string[] winningNumbers = winningNumbersString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] myNumbers = myNumbersString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    int temp = 1;
                    foreach (string number in winningNumbers)
                    {
                        if (myNumbers.Contains(number))
                        {
                            if ((i + temp) < lines.Length)
                            {
                                cardCount[i + temp]++;
                                temp++;
                            }                            
                        }
                    }

                    j++;
                }                
            }
            
            foreach (var item in cardCount)
            {                
                result += item.Value;
            }
            
            return result;
        }
    }
}
