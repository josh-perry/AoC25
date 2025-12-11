namespace Aoc25.Test;

public class Day11
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input = """
                    aaa: you hhh
                    you: bbb ccc
                    bbb: ddd eee
                    ccc: ddd eee fff
                    ddd: ggg
                    eee: out
                    fff: out
                    ggg: out
                    hhh: ccc fff iii
                    iii: out
                    """;

        // Act
        var day = new AoC25.Days.Day11();

        // Assert
        Assert.Equal("5", day.Part1(input));
    }
}