using AdventOfCode2024.Day2;
using AdventOfCode2024.Common;
using AdventOfCode2024.Day3;
using AdventOfCode2024.Day4;
using Xunit;
// ReSharper disable Xunit.XunitTestWithConsoleOutput

namespace Tests;

public sealed class Day4Tests
{
    [Fact]
    public void SumOfCorrectMultiplications()
    {
        var sumOfMultiplications = new Day3().SumOfCorrectMultiplications();
        Assert.Equal(161_289_189, sumOfMultiplications);
    }
    [Theory]
    [InlineData("PXMASPPPSAMXP", 2)]
    [InlineData("XMASPPPSAMX", 2)]
    [InlineData("SAMX", 1)]
    public void CaseForSumOfCorrectMultiplications(string values, int result)
    {
        var countOfXmases = new Day4().Count(new [] { values } );
        Assert.Equal(result, countOfXmases);
    }
}