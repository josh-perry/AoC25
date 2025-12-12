using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AoC25.Days;

public class Day11 : IDay
{
    public int Day => 11;
    
    public string Part1(string input)
    {
        var devices = ParseInput(input);
        var totalPaths = FindPathToDevice(devices, "you", "out");
        
        return totalPaths.ToString();
    }
    
    public string Part2(string input)
    {
        var devices = ParseInput(input);

        var svrToFft = FindPathToDevice(devices, "svr", "fft");
        var fftToDac = FindPathToDevice(devices, "fft", "dac");
        var dacToOut =  FindPathToDevice(devices, "dac", "out");
        
        var svrToDac = FindPathToDevice(devices, "svr", "dac");
        var dacToFft = FindPathToDevice(devices, "dac", "fft");
        var fftToOut = FindPathToDevice(devices, "fft", "out");

        var svrFftDacOutPaths = svrToFft * fftToDac * dacToOut;
        var svrDacFftOutPaths = svrToDac * dacToFft * fftToOut;
        var totalPaths = svrFftDacOutPaths + svrDacFftOutPaths;
        
        return totalPaths.ToString();
    }

    private long FindPathToDevice(Dictionary<string, List<string>> devices, string device, string endDevice, HashSet<string>? visited = null, Dictionary<string, long>? deviceScores = null)
    {
        visited ??= new();
        deviceScores ??= new();
        
        if (device == endDevice)
        {
            return 1;
        }

        if (visited.Contains(device) || !devices.TryGetValue(device, out var connectedDevices))
        {
            return 0;
        }

        if (deviceScores.TryGetValue(device, out var deviceScore))
        {
            return deviceScore;
        }

        visited.Add(device);
        var total = connectedDevices.Sum(connectedDevice => FindPathToDevice(devices, connectedDevice, endDevice, visited, deviceScores));
        visited.Remove(device);
        
        deviceScores[device] = total;
        return total;
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