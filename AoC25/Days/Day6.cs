using System.Text.RegularExpressions;

namespace AoC25.Days;

public partial class Day6 : IDay
{
    public int Day => 6;

    [GeneratedRegex(@"(?<number>\s*\d+\s*) ?|(?<operator>\*|\+)")]
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
        var problems = ParseInputPart2(input);

        foreach (var problem in problems)
        {
            var longestStringValue = problem.StringValues.Max(x => x.Length);
            problem.Values = new();

            for (var index = 0; index < longestStringValue; index++)
            {
                var s = "";
                
                foreach (var value in problem.StringValues)
                {
                    if (index >= value.Length)
                    {
                        continue;
                    }
                    
                    s += value[index];
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    continue;
                }

                problem.Values.Add(long.Parse(s));
            }
            
            problem.Values.Reverse();
        }

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

    private class Problem
    {
        public List<long> Values { get; set; } = new();
        
        public List<string> StringValues { get; set; } = new();

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
    
    private List<Problem> ParseInputPart2(string input)
    {
        var problems = new List<Problem>();
        var operatorRegex = new Regex(@"\+|\*");
        var columnStartIndexes = new Dictionary<int, int>();
        var operators = new Dictionary<int, char>();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = operatorRegex.Matches(line);
            var column = 0;

            foreach (Match match in matches)
            {
                columnStartIndexes[column] = match.Index;
                operators[column] = match.Value[0];
                column++;
            }
        }
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            if (operatorRegex.IsMatch(line))
            {
                continue;
            }

            for (var columnIndex = 0; columnIndex < columnStartIndexes.Count; columnIndex++)
            {
                var columnLength = columnIndex == columnStartIndexes.Count - 1
                    ? line.Length -  columnStartIndexes[columnIndex]
                    : columnStartIndexes[columnIndex + 1] - columnStartIndexes[columnIndex] - 1;
                
                var columnString = line.Substring(columnStartIndexes[columnIndex], columnLength);
                
                var r = new Regex($"(?<number>.{{{columnLength}}})");

                var match = r.Match(columnString);

                if (columnIndex > problems.Count - 1)
                {
                    problems.Add(new Problem());
                    problems[columnIndex].Operator = operators[columnIndex];
                }

                var problem =  problems[columnIndex];
                var n = match.Groups["number"].Value;
                problem.StringValues.Add(n);
            }
        }

        return problems;
    }
}