using AdventOfCode2024.Day1;
using Xunit;

namespace Tests;

public sealed class Day1Tests
{
    [Fact]
    public void Distance()
    {
        var distance = Day1.Distance();
        Assert.Equal(2970687, distance);
    }
    [Fact]
    public void SimilarityScore()
    {
        var similarityScore = Day1.SimilarityScore();
        Assert.Equal(23963899, similarityScore);
    }
}