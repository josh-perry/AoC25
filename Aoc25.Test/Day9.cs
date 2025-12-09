namespace Aoc25.Test;

public class Day9
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    7,1
                    11,1
                    11,7
                    9,7
                    9,5
                    2,5
                    2,3
                    7,3
                    """;

        // Act
        var day = new AoC25.Days.Day9();

        // Assert
        Assert.Equal("50", day.Part1(input));
    }
}