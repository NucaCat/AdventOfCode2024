namespace AdventOfCode2024.Common;

public static class Extensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        => source.OrderBy(u => u);
    public static string JoinToString<T>(this IEnumerable<T> source, string separator = " ")
        => string.Join(separator, source);
}