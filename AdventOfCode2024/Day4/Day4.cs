using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day4;

public sealed class Day4
{
    public const string SearchValue = "XMAS";
    public const string ReverseSearchValue = "SAMX";

    public int CountInAllDirections(IEnumerable<string>? overrideValues = null)
    {
        var values = InitialParse(overrideValues);
        
        var countInRows = CountInRows(values, SearchValue);
        var reverseCountInRows = CountInRows(values, ReverseSearchValue);
        var countInDiagonals = CountInDiagonals(values, SearchValue);
        var reverseCountInDiagonals = CountInDiagonals(values, ReverseSearchValue);
        var countInColumns = CountInColumns(values, SearchValue);
        var reverseCountInColumns = CountInColumns(values, ReverseSearchValue);

        return countInRows + reverseCountInRows 
            + countInDiagonals + reverseCountInDiagonals 
            + countInColumns + reverseCountInColumns;
    }

    private int CountInColumns(List<string> text, string searchValue)
        => CountInDiagonalsWithShift(text, searchValue, shift: 0);

    private int CountInDiagonals(List<string> text, string searchValue)
        => CountInDiagonalsWithShift(text, searchValue, shift: 1) + CountInDiagonalsWithShift(text, searchValue, shift: -1);

    private int CountInDiagonalsWithShift(List<string> text, string searchValue, int shift)
    {
        var count = 0;
        foreach (var (lineIndex, line) in text.Index())
        {
            foreach (var (indexInLine, _) in line.Index())
            {
                var diagonal = GetDiagonalFromWithShift(text, lineIndex, indexInLine, searchValue.Length, shift);
                if (diagonal.AsSpan().SequenceEqual(searchValue))
                    count++;
            }
        }
        return count;
    }

    private char[] GetDiagonalFromWithShift(List<string> text, int lineIndex, int indexInLine, int symbolsCount, int incrementalShift)
    {
        var list = text.Skip(lineIndex)
            .Take(symbolsCount)
            .Select(u =>
            {
                var character = u.SkipOrEmpty(indexInLine).Cast<char?>().FirstOrDefault();
                indexInLine += incrementalShift;
                return character;
            })
            .SelectNotNull()
            .ToArray();

        return list;
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