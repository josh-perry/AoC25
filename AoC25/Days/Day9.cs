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
        long biggestArea = 0;
        
        var redTiles = ParseInput(input);
        var tileLines = new List<(long x, long y, long x2, long y2)>();

        for (var index = 0; index < redTiles.Count - 1; index++)
        {
            var tileA = redTiles[index];
            var tileB = redTiles[index + 1];
            
            tileLines.Add((tileA.x, tileA.y, tileB.x, tileB.y));
        }
        
        tileLines.Add((redTiles[^1].x, redTiles[^1].y, redTiles[0].x, redTiles[0].y));

        for (var index = 0; index < redTiles.Count; index++)
        {
            var tileA = redTiles[index];
            
            for (var index2 = index + 1; index2 < redTiles.Count; index2++)
            {
                var tileB = redTiles[index2];
                
                var width = Math.Abs(tileA.x - tileB.x) + 1;
                var height = Math.Abs(tileA.y - tileB.y) + 1;
                
                if (width * height <= biggestArea)
                {
                    continue;
                }
                
                var minX = Math.Min(tileA.x, tileB.x);
                var minY = Math.Min(tileA.y, tileB.y);
                var maxX = Math.Max(tileA.x, tileB.x);
                var maxY = Math.Max(tileA.y, tileB.y);

                var valid = true;

                foreach (var line in tileLines)
                {
                    if (!DoesLineIntersectRectangle(line, minX, minY, maxX, maxY))
                    {
                        continue;
                    }
                    
                    valid = false;
                    break;
                }

                if (valid)
                {
                    biggestArea = Math.Max(biggestArea, width * height);
                }
            }
        }

        return biggestArea.ToString();
    }

    private static bool DoesLineIntersectRectangle((long x, long y, long x2, long y2) line, long minX, long minY, long maxX, long maxY)
    {
        var lineMinX = Math.Min(line.x, line.x2);
        var lineMaxX = Math.Max(line.x, line.x2);
        var lineMinY = Math.Min(line.y, line.y2);
        var lineMaxY = Math.Max(line.y, line.y2);

        if (line.y == line.y2 && (line.y == minY || line.y == maxY))
            return false;
            
        if (line.x == line.x2 && (line.x == minX || line.x == maxX))
            return false;

        return lineMinX < maxX && lineMaxX > minX &&
               lineMinY < maxY && lineMaxY > minY;
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