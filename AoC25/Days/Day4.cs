namespace AoC25.Days;

public class Day4 : IDay
{
    public int Day => 4;

    private List<List<bool>> ParseInput(string input)
    {
        var map = new List<List<bool>>();
        
        foreach (var line in input.Split('\n'))
        {
            map.Add([..line.Select(c => c == '@')]);
        }
        
        return map;
    }
    
    public string Part1(string input)
    {
        var map = ParseInput(input);
        var totalRolls = 0;
        var neighbourOffsets = new List<(int y, int x)>
        {
            ( -1, -1 ),
            ( 0, -1 ),
            ( 1, -1 ),
            ( -1, 0 ),
            ( 1, 0 ),
            ( -1, 1),
            ( 0, 1 ),
            ( 1, 1 )
        };

        for (var line = 0; line < map.Count; line++)
        {
            for (var column = 0; column < map[line].Count; column++)
            {
                if (!map[line][column]) continue;

                var neighbours = 0;
                
                foreach (var neighbourOffset in neighbourOffsets)
                {
                    var neighbourY  = line + neighbourOffset.y;
                    var neighbourX  = column + neighbourOffset.x;
                    
                    if (neighbourY >= 0 && neighbourY <= map.Count - 1 && neighbourX >= 0 && neighbourX <= map[line].Count - 1)
                    {
                        var neighbour = map[neighbourY][neighbourX];
                        
                        if (neighbour) neighbours++;
                    }
                }
                
                if (neighbours < 4) totalRolls++;
            }
        }

        return totalRolls.ToString();
    }

    public string Part2(string input)
    {
        throw new NotImplementedException();
    }
}