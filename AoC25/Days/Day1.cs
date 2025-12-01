using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day1 : IDay
{
    public int Day => 1;

    private List<int> ParseInput(string input)
    {
        var instructions = new List<int>();

        var regex = new Regex(@"(?<direction>L|R)(?<amount>\d+)");

        foreach (var line in input.Split(Environment.NewLine))
        {
            var match = regex.Match(line);
            
            var direction = match.Groups["direction"].Value;
            var amount = int.Parse(match.Groups["amount"].Value);

            if (direction == "L")
            {
                amount *= -1;
            }
            
            instructions.Add(amount);
        }
        
        return instructions;
    }

    public string Part1(string input)
    {
        const int maximumValue = 99 + 1;
        
        var currentValue = 50;
        
        var instructions = ParseInput(input);
        var totalZeroes = 0;
        
        foreach (var instruction in instructions)
        {
            currentValue = ((currentValue + instruction) % maximumValue + maximumValue) % maximumValue;

            if (currentValue == 0)
            {
                totalZeroes++;
            }
        }

        return totalZeroes.ToString();
    }

    public string Part2(string input)
    {
        return input;
    }
}