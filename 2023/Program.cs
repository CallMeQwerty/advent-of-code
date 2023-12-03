using AoC23;
using AoC23.Solutions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to my AoC23 solution. Enter 0 to Quit\n");
        int selectedDay;

        do
        {
            Console.Write("I want to solve Day: ");            
                        
            if (!int.TryParse(Console.ReadLine(), out selectedDay) || selectedDay <= 0 || selectedDay >= 25) { break; }
            
            SolveService solveService = new SolveService(selectedDay);
            solveService.Solve();

            Console.WriteLine();
            
        } while (selectedDay != 0);       
    }
}