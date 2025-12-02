using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day2 : IDay
{
    public int Day => 2;

    private List<(long left, long right)> ParseInput(string input)
    {
        var regex = new Regex(@"(?<left>\d+)-(?<right>\d+)");

        return input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select<string, (long left, long right)>(x =>
        {
            var match = regex.Match(x);
            return new()
            {
                left = long.Parse(match.Groups["left"].Value),
                right = long.Parse(match.Groups["right"].Value)
            };
        }).ToList();
    }
    
    public string Part1(string input)
    {
        var ranges =  ParseInput(input);
        long total = 0;

        foreach (var range in ranges)
        {
            for (var i = range.left; i <= range.right; i++)
            {
                var idString = i.ToString();
                
                var leftHalf = idString[..(idString.Length / 2)];
                var rightHalf = idString[(idString.Length / 2)..];

                if (leftHalf == rightHalf)
                {
                    total += i;
                }
            }
        }

        return total.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }
}