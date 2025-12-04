namespace AoC25.Days;

public class Day3 : IDay
{
    public int Day => 3;

    private List<List<int>> ParseInput(string input)
    {
        var batteryBanks = new List<List<int>>();

        foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            batteryBanks.Add(line.Select(c => int.Parse(c.ToString())).ToList());
        }

        return batteryBanks;
    }

    public string Part1(string input)
    {
        var batteryBanks = ParseInput(input);
        var totalJoltage = batteryBanks.Sum(x => FindJoltage(x, 2));

        return totalJoltage.ToString();
    }

    public string Part2(string input)
    {
        var batteryBanks = ParseInput(input);
        var totalJoltage = batteryBanks.Sum(x => FindJoltage(x, 12));

        return totalJoltage.ToString();
    }

    private static long FindJoltage(List<int> batteryBank, int batteryCount)
    {
        var biggestJolt = new List<int>(new int[batteryCount]);
        var lastBatteryIndex = -1;

        for (var joltingIndex = 0; joltingIndex < batteryCount; joltingIndex++)
        {
            var maxBatteryIndex = batteryBank.Count - batteryCount + joltingIndex + 1;

            for (var batteryIndex = lastBatteryIndex + 1; batteryIndex < maxBatteryIndex; batteryIndex++)
            {
                var thisBatteryValue  = batteryBank[batteryIndex];
                if (thisBatteryValue <= biggestJolt[joltingIndex]) continue;
                
                biggestJolt[joltingIndex] = thisBatteryValue;
                lastBatteryIndex = batteryIndex;
            }
        }
        
        return long.Parse(string.Concat(biggestJolt.Select(x => x.ToString())));
    }
}