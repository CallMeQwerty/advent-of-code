using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC23.Solutions
{
    public class Day_05 : ISolution
    {
        public void Solve()
        {
            Console.Out.WriteLine("Solving Day 5");

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "Inputs", "05.txt");

            string[] lines = File.ReadAllLines(filePath);


            // Part 1
            long resultPart1 = SolvePart1(lines);
            Console.Out.WriteLine($"Part 1: {resultPart1}");

            // Part 2
            long resultPart2 = SolvePart2(lines);
            Console.Out.WriteLine($"Part 2: {resultPart2}");
        }

        private long SolvePart1(string[] lines)
        {
            long result = long.MaxValue;            
            
            int rowsCount = lines.Length;

            List<MapValues> seedToSoil = new();
            List<MapValues> soilToFertilizer = new();
            List<MapValues> fertilizerToWater = new();
            List<MapValues> waterToLight = new();
            List<MapValues> lightToTemperature = new();
            List<MapValues> temperatureToHumidity = new();
            List<MapValues> humidityToLocation = new();
            
            // Fill the dictionaries
            for (int i = 1; i < rowsCount ; i++)
            {
                if (lines[i].Contains("seed-to-soil map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        seedToSoil.Add(mapValues);
                        i++;
                    }
                } 
                else if (lines[i].Contains("soil-to-fertilizer map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        soilToFertilizer.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("fertilizer-to-water map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        fertilizerToWater.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("water-to-light map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        waterToLight.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("light-to-temperature map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        lightToTemperature.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("temperature-to-humidity map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        temperatureToHumidity.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("humidity-to-location map:"))
                {
                    i++;
                    while (i != rowsCount)
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        humidityToLocation.Add(mapValues);
                        i++;
                    }
                }
            }

            // Get the seeds
            string numberPattern = @"\d+";
            Regex regex = new Regex(numberPattern);
            MatchCollection matches = regex.Matches(lines[0]);

            List<long> seeds = matches.Cast<Match>().Select(match => long.Parse(match.Value)).ToList();

            // Run seeds trough dictionaries
            foreach (long seed in seeds)
            {
                long seedNumber = seed;

                foreach (var map in seedToSoil)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in soilToFertilizer)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in fertilizerToWater)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in waterToLight)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in lightToTemperature)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in temperatureToHumidity)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                foreach (var map in humidityToLocation)
                {
                    if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                    {
                        seedNumber = map.Destination + (seedNumber - map.Source);
                        break;
                    }
                }

                result = seedNumber < result ? seedNumber : result;
            }

            return result;
        }

        private long SolvePart2(string[] lines)
        {
            long result = long.MaxValue;

            int rowsCount = lines.Length;

            List<MapValues> seedToSoil = new();
            List<MapValues> soilToFertilizer = new();
            List<MapValues> fertilizerToWater = new();
            List<MapValues> waterToLight = new();
            List<MapValues> lightToTemperature = new();
            List<MapValues> temperatureToHumidity = new();
            List<MapValues> humidityToLocation = new();

            // Fill the dictionaries
            for (int i = 1; i < rowsCount; i++)
            {
                if (lines[i].Contains("seed-to-soil map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        seedToSoil.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("soil-to-fertilizer map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        soilToFertilizer.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("fertilizer-to-water map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        fertilizerToWater.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("water-to-light map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        waterToLight.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("light-to-temperature map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        lightToTemperature.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("temperature-to-humidity map:"))
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        temperatureToHumidity.Add(mapValues);
                        i++;
                    }
                }
                else if (lines[i].Contains("humidity-to-location map:"))
                {
                    i++;
                    while (i != rowsCount)
                    {
                        string[] numbers = lines[i].Split(" ");
                        MapValues mapValues = new MapValues()
                        {
                            Destination = long.Parse(numbers[0]),
                            Source = long.Parse(numbers[1]),
                            Range = long.Parse(numbers[2])
                        };
                        humidityToLocation.Add(mapValues);
                        i++;
                    }
                }
            }

            // Get the seeds
            string numberPattern = @"\d+";
            Regex regex = new Regex(numberPattern);
            MatchCollection matches = regex.Matches(lines[0]);

            // Run seeds trough dictionaries
            Parallel.For(0, matches.Count / 2, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, i =>
            {
                int index = i * 2;
                long start = long.Parse(matches[index].Value);
                long range = long.Parse(matches[index + 1].Value);

                for (long j = 0; j <= range - 1; j++)
                {
                    long seedNumber = start + j;

                    foreach (var map in seedToSoil)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in soilToFertilizer)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in fertilizerToWater)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in waterToLight)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in lightToTemperature)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in temperatureToHumidity)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    foreach (var map in humidityToLocation)
                    {
                        if (seedNumber >= map.Source && seedNumber <= map.Source + map.Range - 1)
                        {
                            seedNumber = map.Destination + (seedNumber - map.Source);
                            break;
                        }
                    }

                    result = seedNumber < result ? seedNumber : result;
                }
            });

            return result;
        }

        private class MapValues()
        {
            public long Destination { get; set; }
            public long Source { get; set; }
            public long Range { get; set; }
        }
    }
}
