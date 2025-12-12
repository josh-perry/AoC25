using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day12 : IDay
{
    public int Day => 12;
    
    public string Part1(string input)
    {
        var (shapes, regions) = ParseInput(input);
        var shapeSizes = new Dictionary<int, int>();

        var totalRegionsWithEnoughSpace = 0L;

        for (var index = 0; index < shapes.Count; index++)
        {
            var shape = shapes[index];
            shapeSizes[index] = shape.Count(x => x);
        }

        foreach (var region in regions)
        {
            var totalArea = region.X * region.Y;
            var totalPresentArea = 0;

            foreach (var (key, value) in region.PresentCount)
            {
                totalPresentArea += shapeSizes[key] * value;
            }

            if (totalPresentArea > totalArea)
            {
                continue;
            }
            
            var totalNumberOfPresents = region.PresentCount.Sum(x => x.Value);
            var gridSpacesRequired = (region.X / 3) * (region.Y / 3);
            
            if (gridSpacesRequired < totalNumberOfPresents)
            {
                continue;
            }
            
            totalRegionsWithEnoughSpace++;
        }

        return totalRegionsWithEnoughSpace.ToString();
    }

    public string Part2(string input)
    {
        return "No part 2!";
    }

    private (List<List<bool>> shapes, List<Region> regions) ParseInput(string input)
    {
        var shapes = new List<List<bool>>();
        var regions = new List<Region>();
        
        var shapeRegex = new Regex(@"#|\.");
        var regionRegex = new Regex(@"(?<x>\d+)x(?<y>\d+):|(?<!^)(?<count>\d+)");

        var currentShape = new List<bool>();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                shapes.Add(currentShape);
                currentShape = new();
            }
            
            var shapeMatches = shapeRegex.Matches(line);

            if (shapeMatches.Count > 0)
            {
                foreach (Match match in shapeMatches)
                {
                    currentShape.Add(match.Value == "#");
                }
            }
            else
            {
                var regionMatches = regionRegex.Matches(line);

                if (regionMatches.Count > 0)
                {
                    var region = new Region();
                    var presentIndex = 0;

                    foreach (Match regionMatch in regionMatches)
                    {
                        region.X = regionMatch.Groups["x"].Success
                            ? int.Parse(regionMatch.Groups["x"].Value)
                            : region.X;

                        region.Y = regionMatch.Groups["y"].Success
                            ? int.Parse(regionMatch.Groups["y"].Value)
                            : region.Y;

                        if (regionMatch.Groups["count"].Success)
                        {
                            region.PresentCount[presentIndex] = int.Parse(regionMatch.Groups["count"].Value);
                            presentIndex++;
                        }
                    }

                    regions.Add(region);
                }
            }
        }

        return (shapes, regions);
    }

    private record Region
    {
        public int X { get; set; }
        
        public int Y { get; set; }

        public Dictionary<int, int> PresentCount { get; set; } = new();
    }
}