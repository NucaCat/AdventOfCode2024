using AdventOfCode2024.Day1;
using AdventOfCode2024.Day5;
using Xunit;

namespace Tests;

public sealed class Day5Tests
{
    [Fact]
    public void SumOfMiddlePartOfCorrectUpdates()
    {
        var middleParts = new Day5().SumOfMiddlePartOfCorrectUpdates();
        Assert.Equal(4957, middleParts);
    }
    [Fact]
    public void SumOfMiddlePartOfCorrectedIncorrectUpdates()
    {
        var middleParts = new Day5().SumOfMiddlePartOfCorrectedIncorrectUpdates();
        Assert.Equal(6938, middleParts);
    }

    [Theory]
    [InlineData(new []
    {
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13",
        "",
        "75,47,61,53,29",
        "97,61,53,29,13",
        "75,29,13",
        "75,97,47,61,53",
        "61,13,29",
        "97,13,75,29,47",
    }, 143)]
    public void TestSumOfMiddlePartOfCorrectUpdates(string[] overrideValues, int result)
    {
        var middleParts = new Day5().SumOfMiddlePartOfCorrectUpdates(overrideValues);
        Assert.Equal(result, middleParts);
    }

    [Theory]
    [InlineData(new []
    {
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13",
        "",
        "97,13,75,29,47",
    }, 47)]
    public void TestCorrect(string[] overrideValues, int result)
    {
        var middleParts = new Day5().SumOfMiddlePartOfCorrectedIncorrectUpdates(overrideValues);
        Assert.Equal(result, middleParts);
    }
}