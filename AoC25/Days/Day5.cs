using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day5 : IDay
{
    public int Day => 5;
    
    public string Part1(string input)
    {
        var (freshIngredientRanges, spoiledIngedientIds) = ParseInput(input);
        var freshIngredientCount = 0;

        foreach (var spoiledIngredientId in spoiledIngedientIds)
        {
            foreach (var freshIngredientRange in freshIngredientRanges)
            {
                if (spoiledIngredientId >= freshIngredientRange.start && spoiledIngredientId <= freshIngredientRange.end)
                {
                    freshIngredientCount++;
                    break;
                }
            }
        }

        return freshIngredientCount.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private (List<(long start, long end)> freshIngredientRanges, List<long> spoiledIngredientIds) ParseInput(string input)
    {
        var freshIngredientRanges = new List<(long start, long end)>();
        var spoiledIngredientIds = new List<long>();

        var freshIngredientRegex = new Regex(@"(\d+)-(\d+)");

        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrEmpty(line)) continue;
            
            var match = freshIngredientRegex.Match(line);

            if (match.Success)
            {
                freshIngredientRanges.Add((long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value)));
                continue;
            }
            
            spoiledIngredientIds.Add(long.Parse(line));
        }
        
        return (freshIngredientRanges, spoiledIngredientIds);
    }
}