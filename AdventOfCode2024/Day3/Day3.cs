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
    public int SumOfCorrectMultiplicationsWithEnabling(IEnumerable<string>? overrideValues = null)
    {
        var lines = overrideValues ?? File.ReadLines("Day3/input.txt");

        var enabled = true;
        var totalSum = lines.Select(u => SumForLineWithEnabling(u, ref enabled)).DefaultIfEmpty().Sum();
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
                var mult = MultiplicationParser.TryParse(line, ref mutableIndex);
                if (mult is not null)
                    mults.Add(mult.Value);
            }
        }
        return mults.DefaultIfEmpty().Sum();
    }

    private int SumForLineWithEnabling(string line, ref bool enabled)
    {
        var mults = new List<(int Value, bool Enabled)>();
        foreach (var (index, character) in line.Index())
        {
            if (character is 'm')
            {
                var mutableIndex = index;
                var mult = MultiplicationParser.TryParse(line, ref mutableIndex);
                if (mult is not null)
                    mults.Add((mult.Value, enabled));
                continue;
            }

            if (character is 'd')
            {
                var doMutableIndex = index;
                var doParsing = DoParser.TryParse(line, ref doMutableIndex);
                if (doParsing)
                {
                    enabled = true;
                    continue;
                }

                var dontMutableIndex = index;
                var dontParsing = DontParser.TryParse(line, ref dontMutableIndex);
                if (dontParsing)
                {
                    enabled = false;
                    continue;
                }
            }
        }
        return mults.Where(u => u.Enabled).Select(u => u.Value).DefaultIfEmpty().Sum();
    }
}