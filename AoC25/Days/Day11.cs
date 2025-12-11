using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day11 : IDay
{
    public int Day => 11;
    
    public string Part1(string input)
    {
        var devices = ParseInput(input);
        var totalPaths = FindPathToStart(devices, "you");

        return totalPaths.ToString();
    }
    
    public string Part2(string input)
    {
        throw new NotImplementedException();
    }

    private int FindPathToStart(Dictionary<string, List<string>> devices, string startingDevice)
    {
        var queue = new Queue<(string device, HashSet<string> visited)>();
        var paths = 0;
        
        queue.Enqueue((startingDevice, []));

        while (queue.Count > 0)
        {
            var (currentDevice, visited) = queue.Dequeue();
            var connectedDevices = devices[currentDevice];

            foreach (var connectedDevice in connectedDevices)
            {
                if (connectedDevice == "out")
                {
                    paths++;
                    continue;
                }
                else
                {
                    var newVisited = new HashSet<string>(visited);

                    if (newVisited.Add(connectedDevice))
                    {
                        queue.Enqueue((connectedDevice, visited));
                    }
                }
            }
        }

        return paths;
    }

    private Dictionary<string, List<string>> ParseInput(string input)
    {
        var devices = new Dictionary<string, List<string>>();
        var deviceRegex = new Regex(@"(?<id>\w+)\:|(?<connection>\w+)");

        foreach (var line in input.Split(Environment.NewLine))
        {
            var matches = deviceRegex.Matches(line);
            var deviceId = string.Empty;
            var connectingDevices = new List<string>();

            foreach (Match match in matches)
            {
                if (match.Groups["id"].Success)
                {
                    deviceId = match.Groups["id"].Value;
                }
                else if (match.Groups["connection"].Success)
                {
                    foreach (Capture capture in match.Groups["connection"].Captures)
                    {
                        connectingDevices.Add(capture.Value);
                    }
                }
            }
            
            devices.Add(deviceId, connectingDevices);
        }

        return devices;
    }
}