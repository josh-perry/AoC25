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
        throw new NotImplementedException();
    }

    private int FindFewestPresses(Machine machine)
    {
        var fewestPresses = int.MaxValue;

        var queue = new Queue<List<int>>();

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
                    
                    var nextButtons = new List<int>(buttonsToPress) { buttonIndex };
                    queue.Enqueue(nextButtons);
                }
            }
        }

        return fewestPresses;
    }

    private List<Machine> ParseInput(string input)
    {
        var machines = new List<Machine>();

        var regex = new Regex(@"\[(?<lights>.*)\]|\((?<buttons>(.+?))\)|(?<joltage>\{.*\})");

        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = regex.Matches(line);

            var lightsOn = new List<bool>();
            var buttons = new List<Button>();

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
            }

            var machine = new Machine(lightsOn.ToArray(), buttons.ToArray());
            machines.Add(machine);
        }

        return machines;
    }

    private record Machine(bool[] TargetLights, Button[] Buttons)
    {
        public bool[] Lights = TargetLights.Select(_ => false).ToArray();
        
        public bool IsConfiguredProperly() => !TargetLights.Where((t, lightIndex) => t != Lights[lightIndex]).Any();
    };

    private record Button(int[] LightIndexes);
}