namespace Aoc25.Test;

public class Day1
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    L68
                    L30
                    R48
                    L5
                    R60
                    L55
                    L1
                    L99
                    R14
                    L82
                    """;

        // Act
        var day = new AoC25.Days.Day1();

        // Assert
        Assert.Equal("3", day.Part1(input));
    }
    
    [Fact]
    public void should_give_expected_result_for_mini_input_part2()
    {
        // Arrange
        var input = """
                    L68
                    L30
                    R48
                    L5
                    R60
                    L55
                    L1
                    L99
                    R14
                    L82
                    """;

        // Act
        var day = new AoC25.Days.Day1();

        // Assert
        Assert.Equal("6", day.Part2(input));
    }
}