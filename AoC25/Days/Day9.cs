using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day9 : IDay
{
    public int Day => 9;
    
    public string Part1(string input)
    {
        var tiles = ParseInput(input);
        long biggestArea = 0;

        for (var index = 0; index < tiles.Count; index++)
        {
            var tileA = tiles[index];
            
            for (var index2 = index + 1; index2 < tiles.Count; index2++)
            {
                var tileB = tiles[index2];
                
                var width = Math.Abs(tileA.x - tileB.x) + 1;
                var height = Math.Abs(tileA.y - tileB.y) + 1;
                
                biggestArea = Math.Max(biggestArea, width * height);
            }
        }

        return biggestArea.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private List<(long x, long y)> ParseInput(string input)
    {
        var tiles = new List<(long x, long y)>();
        var regex = new Regex(@"(?<x>\d+),(?<y>\d+)");
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var match = regex.Match(line);
            tiles.Add((long.Parse(match.Groups["x"].Value), long.Parse(match.Groups["y"].Value)));
        }

        return tiles;
    }
}