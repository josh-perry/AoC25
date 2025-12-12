namespace Aoc25.Test;

public class Day12
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    0:
                    ###
                    ##.
                    ##.
                    
                    1:
                    ###
                    ##.
                    .##
                    
                    2:
                    .##
                    ###
                    ##.
                    
                    3:
                    ##.
                    ###
                    ##.
                    
                    4:
                    ###
                    #..
                    ###
                    
                    5:
                    ###
                    .#.
                    ###
                    
                    4x4: 0 0 0 0 2 0
                    12x5: 1 0 1 0 2 2
                    12x5: 1 0 1 0 3 2
                    """;

        // Act
        var day = new AoC25.Days.Day12();

        // Assert
        Assert.Equal("2", day.Part1(input));
    }
}