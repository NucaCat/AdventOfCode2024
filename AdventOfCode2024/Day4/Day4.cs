using System.Buffers;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day4;

public sealed class Day4
{
    private const string _xmasSearchValue = "XMAS";
    private const string _xmasReverseSearchValue = "SAMX";

    private readonly string[] _masSearchValues = ["MAS", "SAM"]; 

    public int CountInAllDirections(IEnumerable<string>? overrideValues = null)
    {
        var values = InitialParse(overrideValues);
        
        var countInRows = CountInRows(values, _xmasSearchValue);
        var reverseCountInRows = CountInRows(values, _xmasReverseSearchValue);
        var countInDiagonals = CountInDiagonals(values, _xmasSearchValue);
        var reverseCountInDiagonals = CountInDiagonals(values, _xmasReverseSearchValue);
        var countInColumns = CountInColumns(values, _xmasSearchValue);
        var reverseCountInColumns = CountInColumns(values, _xmasReverseSearchValue);

        return countInRows + reverseCountInRows 
            + countInDiagonals + reverseCountInDiagonals 
            + countInColumns + reverseCountInColumns;
    }

    public int CountInXShape(IEnumerable<string>? overrideValues = null)
    {
        var values = InitialParse(overrideValues);

        var countInXes = CountInXes(values);

        return countInXes;
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
    private int CountInXes(List<string> text)
    {
        var xLength = _masSearchValues[0].Length;
        
        var count = 0;
        foreach (var (lineIndex, line) in text.Index())
        {
            foreach (var (indexInLine, _) in line.Index())
            {
                var forwardDiagonal = GetDiagonalFromWithShift(text, lineIndex, indexInLine, xLength, incrementalShift: 1);
                var backDiagonal = GetDiagonalFromWithShift(text, lineIndex, indexInLine + 2, xLength, incrementalShift: -1);
                if (_masSearchValues.Contains(new string(forwardDiagonal)) && _masSearchValues.Contains(new string(backDiagonal)))
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