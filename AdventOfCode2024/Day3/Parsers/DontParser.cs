namespace AdventOfCode2024.Day3.Parsers;

internal sealed class DontParser
{
    public static bool TryParse(string line, ref int index)
    {
        var dontTemplate = "don't()".AsSpan();
        var dontPart = line.SliceWithMove(ref index, dontTemplate.Length);
        return dontPart.SequenceEqual(dontTemplate);
    }
}