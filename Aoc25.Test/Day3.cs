namespace Aoc25.Test;

public class Day3
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    987654321111111
                    811111111111119
                    234234234234278
                    818181911112111
                    """;

        // Act
        var day = new AoC25.Days.Day3();

        // Assert
        Assert.Equal("357", day.Part1(input));
    }
}