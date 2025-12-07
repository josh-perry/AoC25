using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day7 : IDay
{
    public int Day => 7;
    
    public string Part1(string input)
    {
        var (startPosition, splitters, maxY) = ParseInput(input);
        var beamPositions = new HashSet<(long y, long x)> { startPosition };

        var totalSplits = 0;

        for (var y = 0; y < maxY; y++)
        {
            foreach (var beam in beamPositions.Where(beam => beam.y == y).ToList())
            {
                var nextY = beam.y + 1;
                
                if (splitters.Contains((nextY, beam.x)))
                {
                    totalSplits++;
                    
                    beamPositions.Add((nextY, beam.x - 1));
                    beamPositions.Add((nextY, beam.x + 1));
                }
                else
                {
                    beamPositions.Remove(beam);
                    beamPositions.Add((nextY, beam.x));
                }
            }
        }
       
        return totalSplits.ToString();
    }

    public string Part2(string input)
    {
        var (startPosition, splitters, maxY) = ParseInput(input);
        var beamPositions = new HashSet<(long y, long x)> { startPosition };
        
        var totalTimelinesAtPosition = new Dictionary<(long y, long x), long> { { startPosition, 1 } };

        void IncrementTimelinesAtPosition((long y, long x) position, long amount = 1)
        {
            if (totalTimelinesAtPosition.TryAdd(position, amount))
            {
                return;
            }
            
            totalTimelinesAtPosition[position] += amount;
        }

        for (var y = 0; y < maxY; y++)
        {
            foreach (var beam in beamPositions.Where(beam => beam.y == y).ToList())
            {
                var nextY = beam.y + 1;
                
                if (splitters.Contains((nextY, beam.x)))
                {
                    IncrementTimelinesAtPosition((nextY, beam.x - 1), totalTimelinesAtPosition[beam]);
                    beamPositions.Add((nextY, beam.x - 1));

                    IncrementTimelinesAtPosition((nextY, beam.x + 1), totalTimelinesAtPosition[beam]);
                    beamPositions.Add((nextY, beam.x + 1));
                }
                else
                {
                    beamPositions.Remove(beam);
                    beamPositions.Add((nextY, beam.x));
                    IncrementTimelinesAtPosition((nextY, beam.x), totalTimelinesAtPosition[beam]);
                }
            }
        }

        var bottomRowTimelines = totalTimelinesAtPosition.Where(x => x.Key.y == maxY).Sum(x => x.Value);
        return bottomRowTimelines.ToString();
    }

    private ((long, long) startPosition, HashSet<(long y, long x)> splitters, long maxY) ParseInput(string input)
    {
        var stuffRegex = new Regex(@"(?<start>S)|(?<splitter>\^)");
        var startPosition = (0, 0);
        var splitters = new HashSet<(long y, long x)>();
        
        var y = 0;
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = stuffRegex.Matches(line);

            if (matches.Count == 0)
                continue;

            foreach (Match match in matches)
            {
                if (match.Groups["start"].Success)
                {
                    var start = match.Groups["start"];
                    startPosition = (y, start.Index);
                }

                if (match.Groups["splitter"].Success)
                {
                    foreach (Capture splitter in match.Groups["splitter"].Captures)
                    {
                        splitters.Add((y, splitter.Index));
                    }
                }
            }

            y++;
        }

        return (startPosition, splitters, y);
    }
}