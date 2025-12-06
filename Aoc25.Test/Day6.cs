namespace Aoc25.Test;

public class Day6
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    123 328  51 64 
                     45 64  387 23 
                      6 98  215 314
                    *   +   *   +  
                    """;

        // Act
        var day = new AoC25.Days.Day6();

        // Assert
        Assert.Equal("4277556", day.Part1(input));
    }
    
    [Fact]
    public void should_give_expected_result_for_mini_input_part2()
    {
        // Arrange
        var input = """
                    123 328  51 64 
                     45 64  387 23 
                      6 98  215 314
                    *   +   *   +  
                    """;

        // Act
        var day = new AoC25.Days.Day6();

        // Assert
        Assert.Equal("3263827", day.Part2(input));
    }
}