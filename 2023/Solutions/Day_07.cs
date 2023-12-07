using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_07 : ISolution
    {
        private char[] cardCharacters = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'};
        private char[] cardCharactersJoker = new char[] { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 7");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "07.txt");

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

            List<CamelCard> allHands = new();

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split(' ');

                string hand = splittedLine[0];
                string bid = splittedLine[1];
                Strength handStrength;

                if (HasFiveOfKind(hand))
                {
                    handStrength = Strength.FiveOfKind;
                }
                else if (HasFourOfKind(hand))
                {
                    handStrength = Strength.FourOfKind;
                }
                else if (HasFullHouse(hand))
                {
                    handStrength = Strength.FullHouse;
                }
                else if (HasThreeOfKind(hand))
                {
                    handStrength = Strength.ThreeOfKind;
                }
                else if (HasTwoPair(hand))
                {
                    handStrength = Strength.TwoPair;
                }
                else if (HasOnePair(hand))
                {
                    handStrength = Strength.OnePair;
                }
                else
                {
                    handStrength = Strength.HighCard;
                }
                
                CamelCard currentHard = new CamelCard() {
                    Hand = hand,
                    Bid = int.Parse(bid),
                    Strength = handStrength
                };
                                
                allHands.Add(currentHard);
            }

            allHands = allHands.OrderBy(card => card.Strength).ThenBy(card => card.Hand, new HandComparer(cardCharacters)).ToList();

            for (int i = 0; i < allHands.Count; i++)
            {
                result += allHands[i].Bid * (i+1);
            }

            return result;
        }

        private int SolvePart2(string[] lines)
        {
            int result = 0;

            List<CamelCard> allHands = new();

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split(' ');

                string hand = splittedLine[0];
                string bid = splittedLine[1];
                Strength handStrength;

                if (HasFiveOfKind(hand) || HasFiveOfKindJoker(hand))
                {
                    handStrength = Strength.FiveOfKind;
                }
                else if (HasFourOfKind(hand) || HasFourOfKindJoker(hand))
                {
                    handStrength = Strength.FourOfKind;
                }
                else if (HasFullHouse(hand) || HasFullHouseJoker(hand))
                {
                    handStrength = Strength.FullHouse;
                }
                else if (HasThreeOfKind(hand) || HasThreeOfKindJoker(hand))
                {
                    handStrength = Strength.ThreeOfKind;
                }
                else if (HasTwoPair(hand) || HasTwoPairJoker(hand))
                {
                    handStrength = Strength.TwoPair;
                }
                else if (HasOnePair(hand) || HasOnePairJoker(hand))
                {
                    handStrength = Strength.OnePair;
                }
                else
                {
                    handStrength = Strength.HighCard;
                }

                CamelCard currentHard = new CamelCard()
                {
                    Hand = hand,
                    Bid = int.Parse(bid),
                    Strength = handStrength
                };

                allHands.Add(currentHard);
            }

            allHands = allHands.OrderBy(card => card.Strength).ThenBy(card => card.Hand, new HandComparer(cardCharactersJoker)).ToList();

            for (int i = 0; i < allHands.Count; i++)
            {
                result += allHands[i].Bid * (i + 1);
            }
            
            return result;
        }

        private class CamelCard()
        {
            public string Hand { get; set; }
            public int Bid { get; set; }
            public Strength Strength { get; set; }
        }

        private enum Strength
        {
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfKind = 4,
            FullHouse = 5,
            FourOfKind = 6,
            FiveOfKind = 7
        }

        #region All standart combinations
        private bool HasFiveOfKind(string input)
        {            
            return input.All(c => c == input[0]);
        }

        private bool HasFourOfKind(string input)
        {
            return input.Distinct().Count() == 2 && input.GroupBy(c => c).Any(group => group.Count() == 4);
        }

        private bool HasFullHouse(string input)
        {
            return input.Distinct().Count() == 2 && input.GroupBy(c => c).Any(group => group.Count() == 3) && input.GroupBy(c => c).Any(group => group.Count() == 2);
        }

        private bool HasThreeOfKind(string input)
        {
            return input.GroupBy(c => c).Any(group => group.Count() == 3);
        }

        private bool HasTwoPair(string input)
        {
            var groups = input.GroupBy(c => c);
            return groups.Count(group => group.Count() == 2) == 2 && groups.Count() == 3;
        }

        private bool HasOnePair(string input)
        {
            return input.Distinct().Count() < input.Length;
        }
        #endregion

        #region All joker combinations
        private bool HasFiveOfKindJoker(string input)
        {            
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasFiveOfKind(modifiedInput)) return true;
            }

            return false;   
        }

        private bool HasFourOfKindJoker(string input)
        {
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasFourOfKind(modifiedInput)) return true;
            }

            return false;
        }

        private bool HasFullHouseJoker(string input)
        {
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasFullHouse(modifiedInput)) return true;
            }

            return false;
        }

        private bool HasThreeOfKindJoker(string input)
        {
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasThreeOfKind(modifiedInput)) return true;
            }

            return false;
        }

        private bool HasTwoPairJoker(string input)
        {
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasTwoPair(modifiedInput)) return true;
            }

            return false;
        }

        private bool HasOnePairJoker(string input)
        {
            foreach (char value in cardCharacters)
            {
                string modifiedInput = input.Replace('J', value);
                if (HasOnePair(modifiedInput)) return true;
            }

            return false;
        }
        #endregion

        private class HandComparer : IComparer<string>
        {
            private readonly char[] cardCharacters;

            public HandComparer(char[] cardCharacters)
            {
                this.cardCharacters = cardCharacters;
            }

            public int Compare(string x, string y)
            {
                for (int i = 0; i < Math.Min(x.Length, y.Length); i++)
                {
                    int compareResult = CompareCardCharacters(x[i], y[i]);
                    if (compareResult != 0) return compareResult;
                }
                return 0;
            }

            private int CompareCardCharacters(char a, char b) => Array.IndexOf(cardCharacters, a).CompareTo(Array.IndexOf(cardCharacters, b));
        }
    }
}
