using System.Text.RegularExpressions;

namespace AoC25.Days;

public partial class Day6 : IDay
{
    public int Day => 6;

    [GeneratedRegex(@"(?<number>\d+)|(?<operator>\*|\+)")]
    private static partial Regex ProblemRegex();
    
    public string Part1(string input)
    {
        var problems = ParseInput(input);
        long total = 0;

        foreach (var problem in problems)
        {
            switch (problem.Operator)
            {
                case '+':
                    total += problem.Values.Sum();
                    break;
                case '*':
                    total += problem.Values.Aggregate((x, y) => x * y);
                    break;
            }
        }

        return total.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private class Problem
    {
        public List<long> Values { get; set; } = new();

        public char Operator { get; set; } = ' ';
    }

    private List<Problem> ParseInput(string input)
    {
        var problems = new List<Problem>();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = ProblemRegex().Matches(line);

            if (matches.Count == 0)
            {
                continue;
            }

            for (var index = 0; index < matches.Count; index++)
            {
                var match = matches[index];
                
                if (index > problems.Count - 1)
                {
                    problems.Add(new Problem());
                }
                
                var problem =  problems[index];
                
                if (match.Groups["number"].Success)
                    problem.Values.Add(long.Parse(match.Groups["number"].Value));
                if (match.Groups["operator"].Success)
                    problem.Operator = Convert.ToChar(match.Groups["operator"].Value);
            }
        }

        return problems;
    }
}