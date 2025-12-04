namespace Aoc25.Test;

public class Day4
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    ..@@.@@@@.
                    @@@.@.@.@@
                    @@@@@.@.@@
                    @.@@@@..@.
                    @@.@@@@.@@
                    .@@@@@@@.@
                    .@.@.@.@@@
                    @.@@@.@@@@
                    .@@@@@@@@.
                    @.@.@@@.@.
                    """;

        // Act
        var day = new AoC25.Days.Day4();

        // Assert
        Assert.Equal("13", day.Part1(input));
    }
}