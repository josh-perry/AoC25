using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day8 : IDay
{
    public int Day => 8;
    
    public string Part1(string input)
    {
        var junctionBoxes = ParseInput(input);
        var circuits = new List<Circuit>();
        
        var pairs = new List<(JunctionBox a, JunctionBox b, double distance)>();
        for (var i = 0; i < junctionBoxes.Count; i++)
        {
            for (var j = i + 1; j < junctionBoxes.Count; j++)
            {
                var distance = GetDistance(junctionBoxes[i], junctionBoxes[j]);
                pairs.Add((junctionBoxes[i], junctionBoxes[j], distance));
            }
        }

        var connected = 0;
        foreach (var _ in ConnectClosestCircuits(circuits, pairs))
        {
            connected++;

            if (connected == 1000) break;
        }

        var biggest = circuits.OrderByDescending(x => x.JunctionBoxes.Count).Take(3);
        var total = biggest.Aggregate(1, (current, circuit) => current * circuit.JunctionBoxes.Count);
        return total.ToString();
    }
    
    public string Part2(string input)
    {
        var junctionBoxes = ParseInput(input);
        var circuits = new List<Circuit>();
        
        var pairs = new List<(JunctionBox a, JunctionBox b, double distance)>();
        for (var i = 0; i < junctionBoxes.Count; i++)
        {
            for (var j = i + 1; j < junctionBoxes.Count; j++)
            {
                var distance = GetDistance(junctionBoxes[i], junctionBoxes[j]);
                pairs.Add((junctionBoxes[i], junctionBoxes[j], distance));
            }
        }

        foreach (var (a, b) in ConnectClosestCircuits(circuits, pairs))
        {
            if (a.Circuit!.JunctionBoxes.Count == junctionBoxes.Count)
            {
                return ((long)a.X * b.X).ToString();
            }
        }

        return "0";
    }

    private IEnumerable<(JunctionBox a, JunctionBox b)> ConnectClosestCircuits(
        List<Circuit> circuits,
        List<(JunctionBox a, JunctionBox b, double distance)> pairs)
    {
        foreach (var (a, b, _) in pairs.OrderBy(p => p.distance))
        {
            if (a.Circuit is not null && b.Circuit is not null)
            {
                if (a.Circuit != b.Circuit)
                {
                    var oldCircuit = b.Circuit;
                    circuits.Remove(oldCircuit);

                    foreach (var box in oldCircuit.JunctionBoxes)
                    {
                        box.Circuit = a.Circuit;
                        a.Circuit.JunctionBoxes.Add(box);
                    }
                }
            }
            else if (a.Circuit is not null)
            {
                b.Circuit = a.Circuit;
                a.Circuit.JunctionBoxes.Add(b);
            }
            else if (b.Circuit is not null)
            {
                a.Circuit = b.Circuit;
                b.Circuit.JunctionBoxes.Add(a);
            }
            else
            {
                var circuit = new Circuit();
                circuits.Add(circuit);
                
                a.Circuit = circuit;
                b.Circuit = circuit;
                
                circuit.JunctionBoxes.Add(a);
                circuit.JunctionBoxes.Add(b);
            }

            yield return (a, b);
        }
    }

    private List<JunctionBox> ParseInput(string input)
    {
        var regex = new Regex(@"(?<x>\d+),(?<y>\d+),(?<z>\d+)");
        var junctionBoxes = new List<JunctionBox>();
        
        foreach (var line in input.Split(Environment.NewLine))
        {
            var match = regex.Match(line);

            var x = int.Parse(match.Groups["x"].Value);
            var y = int.Parse(match.Groups["y"].Value);
            var z = int.Parse(match.Groups["z"].Value);
            
            junctionBoxes.Add(new(x, y, z));
        }
        
        return junctionBoxes;
    }

    private static int GetDistance(JunctionBox a, JunctionBox b) => 
        (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y) + (a.Z - b.Z) * (a.Z - b.Z);

    private record Circuit
    {
        public HashSet<JunctionBox> JunctionBoxes { get; set; } = new();
    };

    private record JunctionBox(int X, int Y, int Z)
    {
        public Circuit? Circuit { get; set; }
    };
}