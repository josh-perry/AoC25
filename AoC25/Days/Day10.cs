using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day10 : IDay
{
    public int Day => 10;
    
    public string Part1(string input)
    {
        var machines = ParseInput(input);
        var totalButtonPresses = machines.Sum(FindFewestPresses);

        return totalButtonPresses.ToString();
    }

    public string Part2(string input)
    {
        // This star is stolen valour, I could not have done it without this:
        // https://www.reddit.com/r/adventofcode/comments/1pk87hl/2025_day_10_part_2_bifurcate_your_way_to_victory/
        var machines = ParseInput(input);
        var totalButtonPresses = 0;

        foreach (var machine in machines)
        {
            var coeffs = machine.Buttons.Select(button => 
                Enumerable.Range(0, machine.TargetLights.Length)
                    .Select(i => button.LightIndexes.Contains(i) ? 1 : 0)
                    .ToArray()
            ).ToArray();

            var result = SolveSingle(coeffs, machine.Joltages);
            totalButtonPresses += result;
        }

        return totalButtonPresses.ToString();
    }

    private Dictionary<string, Dictionary<int[], int>> GetPatterns(int[][] coeffs)
    {
        var buttonCount = coeffs.Length;
        var variableCount = coeffs[0].Length;
        
        var result = new Dictionary<string, Dictionary<int[], int>>();

        for (var numBitsSet = 0; numBitsSet <= variableCount; numBitsSet++)
        {
            foreach (var bitIndices in CombinationsOfSize(variableCount, numBitsSet))
            {
                var parityPattern = new int[variableCount];
                
                foreach (var bitIdx in bitIndices)
                {
                    parityPattern[bitIdx] = 1;
                }
                
                var key = string.Join(",", parityPattern);
                result[key] = new Dictionary<int[], int>(new ArrayComparer());
            }
        }

        for (var numPressed = 0; numPressed <= buttonCount; numPressed++)
        {
            foreach (var buttons in CombinationsOfSize(buttonCount, numPressed))
            {
                var pattern = new int[variableCount];
                
                foreach (var buttonIdx in buttons)
                {
                    for (var i = 0; i < variableCount; i++)
                    {
                        pattern[i] += coeffs[buttonIdx][i];
                    }
                }

                var parityPattern = pattern.Select(x => x % 2).ToArray();
                var key = string.Join(",", parityPattern);
                
                if (!result[key].ContainsKey(pattern))
                {
                    result[key][pattern] = numPressed;
                }
            }
        }

        return result;
    }

    private int SolveSingle(int[][] coeffs, int[] goal)
    {
        var patternCosts = GetPatterns(coeffs);
        var memo = new Dictionary<string, int>();

        int SolveSingleAux(int[] currentGoal)
        {
            if (currentGoal.All(x => x == 0)) return 0;

            var key = string.Join(",", currentGoal);
            if (memo.TryGetValue(key, out var cachedAnswer))
                return cachedAnswer;

            var answer = 1000000;
            var parityPattern = currentGoal.Select(x => x % 2).ToArray();
            var parityKey = string.Join(",", parityPattern);

            foreach (var (pattern, patternCost) in patternCosts[parityKey])
            {
                if (!pattern.SequenceEqual(currentGoal) && !Enumerable.Range(0, pattern.Length).All(i => pattern[i] <= currentGoal[i]))
                    continue;
                
                var newGoal = Enumerable.Range(0, pattern.Length)
                    .Select(i => (currentGoal[i] - pattern[i]) / 2)
                    .ToArray();

                answer = Math.Min(answer, patternCost + 2 * SolveSingleAux(newGoal));
            }

            memo[key] = answer;
            return answer;
        }

        return SolveSingleAux(goal);
    }

    private IEnumerable<List<int>> CombinationsOfSize(int n, int k)
    {
        if (k == 0)
        {
            yield return [];
        }
        else if (k == n)
        {
            yield return Enumerable.Range(0, n).ToList();
        }
        else
        {
            foreach (var combo in CombinationsOfSize(n - 1, k - 1))
            {
                yield return combo.Prepend(n - 1).ToList();
            }
            
            foreach (var combo in CombinationsOfSize(n - 1, k))
            {
                yield return combo;
            }
        }
    }

    private class ArrayComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y) => x.SequenceEqual(y);
        public int GetHashCode(int[] obj) => string.Join(",", obj).GetHashCode();
    }

    private int FindFewestPresses(Machine machine)
    {
        var fewestPresses = int.MaxValue;

        var queue = new Queue<HashSet<int>>();

        for (var buttonIndex = 0; buttonIndex < machine.Buttons.Length; buttonIndex++)
        {
            queue.Enqueue([buttonIndex]);
        }

        while (queue.Count > 0)
        {
            var buttonsToPress = queue.Dequeue();

            if (buttonsToPress.Count >= fewestPresses)
            {
                continue;
            }

            // Reset lights
            for (var lightIndex = 0; lightIndex < machine.TargetLights.Length; lightIndex++)
            {
                machine.Lights[lightIndex] = false;
            }

            foreach (var buttonIndex in buttonsToPress)
            {
                var button = machine.Buttons[buttonIndex];

                foreach (var lightIndex in button.LightIndexes)
                {
                    machine.Lights[lightIndex] = !machine.Lights[lightIndex];
                }
            }
            
            if (machine.IsConfiguredProperly())
            {
                fewestPresses = Math.Min(fewestPresses, buttonsToPress.Count);
            }
            else
            {
                for (var buttonIndex = 0; buttonIndex < machine.Buttons.Length; buttonIndex++)
                {
                    if (buttonsToPress.Contains(buttonIndex))
                    {
                        continue;
                    }
                    
                    var nextButtons = new HashSet<int>(buttonsToPress) { buttonIndex };
                    queue.Enqueue(nextButtons);
                }
            }
        }

        return fewestPresses;
    }

    private List<Machine> ParseInput(string input)
    {
        var machines = new List<Machine>();

        var regex = new Regex(@"\[(?<lights>.*)\]|\((?<buttons>(.+?))\)|\{(?<joltage>.*)\}");

        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = regex.Matches(line);

            var lightsOn = new List<bool>();
            var buttons = new List<Button>();
            var joltages = new List<int>();

            foreach (Match match in matches)
            {
                if (match.Groups["lights"].Success)
                {
                    var lights = match.Groups["lights"].Value;
                    lightsOn = lights.Select(x => x == '#').ToList();
                    continue;
                }

                if (match.Groups["buttons"].Success)
                {
                    var buttonsString = match.Groups["buttons"].Value;
                    buttons.Add(new Button(buttonsString.Split(",").Select(x => int.Parse(x)).ToArray()));
                }

                if (match.Groups["joltage"].Success)
                {
                    var joltagesString = match.Groups["joltage"].Value;
                    joltages.AddRange(joltagesString.Split(",").Select(x => int.Parse(x)));
                }
            }

            var machine = new Machine(lightsOn.ToArray(), buttons.ToArray(), joltages.ToArray());
            machines.Add(machine);
        }

        return machines;
    }

    private record Machine(bool[] TargetLights, Button[] Buttons, int[] Joltages)
    {
        public bool[] Lights = TargetLights.Select(_ => false).ToArray();
        
        public bool IsConfiguredProperly() => !TargetLights.Where((t, lightIndex) => t != Lights[lightIndex]).Any();
    };

    private record Button(int[] LightIndexes);
}