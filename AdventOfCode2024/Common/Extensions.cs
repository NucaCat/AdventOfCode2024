namespace AdventOfCode2024.Common;

internal static class Extensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        => source.OrderBy(u => u);
}