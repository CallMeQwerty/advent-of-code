using AoC23;
using AoC23.Solutions;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("I want to solve Day: ");
        int selectedDay = int.Parse(Console.ReadLine());

        SolveService solveService = new SolveService(selectedDay);
        await solveService.Solve();
    }
}