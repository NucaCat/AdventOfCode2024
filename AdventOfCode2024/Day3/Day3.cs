using AdventOfCode2024.Common;
using AdventOfCode2024.Day3.Parsers;

namespace AdventOfCode2024.Day3;

public sealed class Day3
{
    public int SumOfCorrectMultiplications(IEnumerable<string>? overrideValues = null)
    {
        var lines = overrideValues ?? File.ReadLines("Day3/input.txt");
        var totalSum = lines.Select(SumForLine).DefaultIfEmpty().Sum();
        return totalSum;
    }

    private int SumForLine(string line)
    {
        var mults = new List<int>();
        foreach (var (index, character) in line.Index())
        {
            var mutableIndex = index;
            if (character is 'm')
            {
                var mult = MultiplicationParser.TryParseMultiplication(line, ref mutableIndex);
                if (mult is not null)
                    mults.Add(mult.Value);
            }
        }
        return mults.DefaultIfEmpty().Sum();
    }
}