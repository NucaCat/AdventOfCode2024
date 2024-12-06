namespace AdventOfCode2024.Common;

public static class Extensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        => source.OrderBy(u => u);
    public static string JoinToString<T>(this IEnumerable<T> source, string separator = " ")
        => string.Join(separator, source);
    public static bool IsEmpty<T>(this IEnumerable<T> source)
        => !source.IsNotEmpty();
    public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        => source.Any();
    public static bool IsEmpty<T>(this T[] source)
        => source.Length == 0;
    public static bool IsNotEmpty<T>(this T[] source)
        => !source.IsEmpty();
    public static bool IsEmpty<T>(this List<T> source)
        => source.Count == 0;
    public static bool IsNotEmpty<T>(this List<T> source)
        => !source.IsEmpty();
}