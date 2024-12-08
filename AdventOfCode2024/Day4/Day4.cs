using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day4;

public sealed class Day4
{
    public const string SearchValue = "XMAS";
    public const string ReverseSearchValue = "SAMX";

    public int Count(IEnumerable<string>? overrideValues)
    {
        var values = InitialParse(overrideValues);
        
        var countInRows = CountInRows(values, SearchValue);
        var reverseCountInRows = CountInRows(values, ReverseSearchValue);
        var countInDiagonals = CountInDiagonals(values, SearchValue);
        var reverseCountInDiagonals = CountInDiagonals(values, ReverseSearchValue);

        return countInRows + reverseCountInRows + countInDiagonals + reverseCountInDiagonals;
    }

    private int CountInDiagonals(List<string> matrix, string searchValue)
    {
        return 0;
    }

    private int CountInRows(List<string> text, string searchValue)
        => text.Select(u => CountInRow(u, searchValue)).SumOrEmpty();

    private int CountInRow(string row, string searchValue)
    {
        var count = 0;

        var rowSpan = row.AsSpan();

        foreach (var (index, _) in row.Index())
        {
            var potentialFit = rowSpan.PartialSlice(searchValue, index);
            if (potentialFit.SequenceEqual(searchValue))
                count++;
        }

        return count;
    }

    private List<string> InitialParse(IEnumerable<string>? overrideValues)
    {
        var result = (overrideValues ?? File.ReadLines("Day4/input.txt"))
            .ToList();

        return result;
    }
}