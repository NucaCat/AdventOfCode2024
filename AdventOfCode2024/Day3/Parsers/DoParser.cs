namespace AdventOfCode2024.Day3.Parsers;

internal sealed class DoParser
{
    public static bool TryParse(string line, ref int index)
    {
        var doTemplate = "do()".AsSpan();
        var doPart = line.SliceWithMove(ref index, doTemplate.Length);
        return doPart.SequenceEqual(doTemplate);
    }
}