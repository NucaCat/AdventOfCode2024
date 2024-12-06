namespace AdventOfCode2024.Day3.Parsers;

internal static class Extensions
{
    public static ReadOnlySpan<char> SliceWithMove(this string source, ref int index, int length)
    {
        var slice = source.AsSpan().Slice(index, length);
        index += length;
        return slice;
    }
    public static char? GetWithMove(this string source, ref int index)
    {
        if (index >= source.Length)
            return null;

        var character = source[index];
        index++;
        return character;
    }
    public static char? GetWithoutMove(this string source, int index)
    {
        if (index >= source.Length)
            return null;

        var character = source[index];
        return character;
    }
} 