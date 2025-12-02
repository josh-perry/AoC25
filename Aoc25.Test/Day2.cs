namespace Aoc25.Test;

public class Day2
{
    [Fact]
    public void should_give_expected_result_for_mini_input_part1()
    {
        // Arrange
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224," +
            "1698522-1698528,446443-446449,38593856-38593862,565653-565659," +
            "824824821-824824827,2121212118-2121212124";

        // Act
        var day = new AoC25.Days.Day2();

        // Assert
        Assert.Equal("1227775554", day.Part1(input));
    }
}