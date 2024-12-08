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
        var countOfXmases = new Day4().CountInAllDirections();
        Assert.Equal(2434, countOfXmases);
    }
    [Theory]
    [InlineData("PXMASPPPSAMXP", 2)]
    [InlineData("XMASPPPSAMX", 2)]
    [InlineData("SAMX", 1)]
    public void OneLineCount(string values, int result)
    {
        var countOfXmases = new Day4().CountInAllDirections(new [] { values } );
        Assert.Equal(result, countOfXmases);
    }
    [Theory]
    [InlineData(new []
    {
        "XOOO",
        "OMOO",
        "OOAO",
        "OOOS"
    }, 1)]
    [InlineData(new []
    {
        "SOOO",
        "OAOO",
        "OOMO",
        "OOOX"
    }, 1)]
    [InlineData(new []
    {
        "SAMX",
        "OAOO",
        "OOMO",
        "OOOX"
    }, 2)]
    [InlineData(new []
    {
        "XOOO",
        "MOOO",
        "AOOO",
        "SOOO"
    }, 1)]
    [InlineData(new []
    {
        "OOOS",
        "OOOA",
        "OOOM",
        "OOOX"
    }, 1)]
    [InlineData(new []
    {
        "SOOX",
        "AOOM",
        "MOOA",
        "XOOS"
    }, 2)]
    [InlineData(new []
    {
        "SAMX",
        "AOOM",
        "MOOA",
        "XOOS"
    }, 3)]
    [InlineData(new []
    {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX",
    }, 18)]
    [InlineData(new []
    {
        "....XXMAS.",
        ".SAMXMS...",
        "...S..A...",
        "..A.A.MS.X",
        "XMASAMX.MM",
        "X.....XA.A",
        "S.S.S.S.SS",
        ".A.A.A.A.A",
        "..M.M.M.MM",
        ".X.X.XMASX",
    }, 18)]
    public void MultipleLineCount(string[] values, int result)
    {
        var countOfXmases = new Day4().CountInAllDirections(values);
        Assert.Equal(result, countOfXmases);
    }
}