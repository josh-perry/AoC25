using System.Reflection;
using AoC25;

var days = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
    .Select(t => (IDay)Activator.CreateInstance(t)!)
    .OrderBy(x => x.Day)
    .ToList();

if (args.Contains("--just-last"))
{
    days = days[^1..];
}
    
foreach (var day in days)
{
    Console.WriteLine($"Running day {day.Day}...");

    var input = File.ReadAllText("Inputs/day" + day.Day + ".txt");
    
    Console.WriteLine("Part 1");
    Console.WriteLine(day.Part1(input));
    
    Console.WriteLine("Part 2");
    Console.WriteLine(day.Part2(input));
    
    Console.WriteLine();
}