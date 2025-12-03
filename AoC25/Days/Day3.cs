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
        var totalJoltage = 0;

        static int FindJoltage(List<int> batteryBank)
        {
            var highestJoltage = -1;
            
            for (var i = 0; i < batteryBank.Count - 1; i++)
            {
                for (var j = i + 1; j < batteryBank.Count; j++)
                {
                    highestJoltage = Math.Max(highestJoltage, int.Parse($"{batteryBank[i]}{batteryBank[j]}"));
                }
            }
            
            return highestJoltage;
        }

        foreach (var batteryBank in batteryBanks)
        {
            totalJoltage += FindJoltage(batteryBank);
        }

        return totalJoltage.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }
}