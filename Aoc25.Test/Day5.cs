namespace Aoc25.Test;

public class Day5
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    3-5
                    10-14
                    16-20
                    12-18
                    
                    1
                    5
                    8
                    11
                    17
                    32
                    """;

        // Act
        var day = new AoC25.Days.Day5();

        // Assert
        Assert.Equal("3", day.Part1(input));
    }
    
    [Fact]
    public void should_give_expected_result_for_mini_input_part2()
    {
        // Arrange
        var input = """
                    3-5
                    10-14
                    16-20
                    12-18

                    1
                    5
                    8
                    11
                    17
                    32
                    """;

        // Act
        var day = new AoC25.Days.Day5();

        // Assert
        Assert.Equal("14", day.Part2(input));
    }
}