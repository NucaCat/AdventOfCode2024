﻿using AdventOfCode2024.Day2;
using AdventOfCode2024.Common;
using Xunit;
// ReSharper disable Xunit.XunitTestWithConsoleOutput

namespace Tests;

public sealed class Day2Tests
{
    [Fact]
    public void SafeReportsCountV1()
    {
        var safeCount = new Day2().SafeReportsCountV1();
        Assert.Equal(486, safeCount);
    }
    [Fact]
    public void SafeReportsCountV1WithMethodFromV2()
    {
        var safeCount = new Day2().SafeReportsCountV1WithMethodFromV2();
        Assert.Equal(486, safeCount);
    }
    [Fact]
    public void SafeReportsCountV2()
    {
        var safeCount = new Day2().SafeReportsCountV2();
        // incorrect
        Assert.Equal(539, safeCount);// -> 540
    }
}