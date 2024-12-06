using AdventOfCode2024.Day2;
using AdventOfCode2024.Common;
using AdventOfCode2024.Day3;
using Xunit;
// ReSharper disable Xunit.XunitTestWithConsoleOutput

namespace Tests;

public sealed class Day3Tests
{
    [Fact]
    public void SumOfCorrectMultiplications()
    {
        var sumOfMultiplications = new Day3().SumOfCorrectMultiplications();
        Assert.Equal(161_289_189, sumOfMultiplications);
    }
    [Theory]
    [InlineData("mul(873,602)", 525_546)]
    [InlineData("mul(2,4)", 8)]
    [InlineData("mul(873,602)mul(2,4)", 525_546 + 8)]
    [InlineData("mul(873,6mul(3,4)", 12)]
    public void CaseForSumOfCorrectMultiplications(string values, int result)
    {
        var safeCount = new Day3().SumOfCorrectMultiplications(new [] { values });
        Assert.Equal(result, safeCount);
    }
}