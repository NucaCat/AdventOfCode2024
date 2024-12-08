namespace AdventOfCode2024.Common;

public static class Extensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        => source.OrderBy(u => u);
    public static string JoinToString<T>(this IEnumerable<T> source, string separator = " ")
        => string.Join(separator, source);
    public static IEnumerable<T> SelectNotNull<T>(this IEnumerable<T?> source) where T : struct
        => source.Where(u => u.HasValue).Select(u => u!.Value);
    public static IEnumerable<T> SelectNotNull<T>(this IEnumerable<T?> source) where T : class
        => source.Where(u => u is not null)!;
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
    public static IEnumerable<T> SkipOrEmpty<T>(this IEnumerable<T> source, int count)
        => count < 0 ? Enumerable.Empty<T>() : source.Skip(count);
    
    public static int SumOrEmpty(this IEnumerable<int> source)
        => source.DefaultIfEmpty().Sum();
    public static decimal SumOrEmpty(this IEnumerable<decimal> source)
        => source.DefaultIfEmpty().Sum();
    public static double SumOrEmpty(this IEnumerable<double> source)
        => source.DefaultIfEmpty().Sum();
    public static float SumOrEmpty(this IEnumerable<float> source)
        => source.DefaultIfEmpty().Sum();
    
    public static ReadOnlySpan<char> PartialSlice(this ReadOnlySpan<char> rowSpan, string searchValue, int index)
    {
        var length = index + searchValue.Length >= rowSpan.Length
            ? rowSpan.Length - index
            : searchValue.Length;
        return rowSpan.Slice(index, length);
    }
}