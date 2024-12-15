using AdventOfCode2024.Day6;
using Xunit;

namespace Tests;

public sealed class Day6Tests
{
    [Fact]
    public void SumOfMiddlePartOfCorrectUpdates()
    {
        var middleParts = new Day6().CountOfDistinctPositionsOfPatrolPath();
        Assert.Equal(4647, middleParts);
    }

    [Theory]
    [InlineData(new []
    {
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#...",
    }, 41)]
    public void TestCorrect(string[] overrideValues, int result)
    {
        var middleParts = new Day6().CountOfDistinctPositionsOfPatrolPath(overrideValues);
        Assert.Equal(result, middleParts);
    }
}