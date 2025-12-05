using System.Text.RegularExpressions;

namespace AoC25.Days;

public partial class Day5 : IDay
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
        var (freshIngredientRanges, _) = ParseInput(input);
        freshIngredientRanges = freshIngredientRanges.OrderBy(x => x.start).ToList();

        while (MergeRanges(ref freshIngredientRanges)) { }

        var freshIngredientCount = freshIngredientRanges.Sum(range => range.end - range.start + 1);
        return freshIngredientCount.ToString();
    }

    private bool MergeRanges(ref List<(long start, long end)> freshIngredientRanges)
    {
        var mergedRanges = false;
        
        for (var rangeIndex = 0; rangeIndex < freshIngredientRanges.Count; rangeIndex++)
        {
            var rangeA = freshIngredientRanges[rangeIndex];
            
            for (var rangeIndex2 = rangeIndex + 1; rangeIndex2 < freshIngredientRanges.Count; rangeIndex2++)
            {
                var rangeB = freshIngredientRanges[rangeIndex2];

                if ((rangeB.start >= rangeA.start && rangeB.start <= rangeA.end) ||
                    (rangeB.end >= rangeA.start && rangeB.end <= rangeA.end))
                {
                    var minStart = Math.Min(rangeA.start, rangeB.start);
                    var maxEnd = Math.Max(rangeA.end, rangeB.end);

                    freshIngredientRanges[rangeIndex] = (minStart, maxEnd);
                    freshIngredientRanges.RemoveAt(rangeIndex2);
                    mergedRanges = true;
                }
            }
        }

        return mergedRanges;
    }

    [GeneratedRegex(@"(\d+)-(\d+)")]
    private static partial Regex FreshIngredientRegex();

    private (List<(long start, long end)> freshIngredientRanges, List<long> spoiledIngredientIds) ParseInput(string input)
    {
        var freshIngredientRanges = new List<(long start, long end)>();
        var spoiledIngredientIds = new List<long>();

        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrEmpty(line)) continue;

            var match = FreshIngredientRegex().Match(line);

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