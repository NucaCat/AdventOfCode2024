namespace AdventOfCode2024.Day2;

public sealed class Day2
{
    private const int _minDifference = 1;
    private const int _maxDifference = 3;

    public int SafeReportsCountV1()
    {
        var reports = InitialPopulateReports();

        var safeReportsCount = reports.Count(IsSafe);
        return safeReportsCount;
    }

    public int SafeReportsCountV2BruteForce(IEnumerable<string>? overrideValues = null)
    {
        var reports = InitialPopulateReports(overrideValues);

        var safeReportsCount = reports.Where(u =>
        {
            var isSafe = IsSafe(u);
            if (isSafe)
                return true;

            return reports.Index().Any(v => IsSafe(new Report(u.Levels
                .Index()
                .Where(x => x.Index != v.Index)
                .Select(x => x.Item)
                .ToList())));
        }).Count();
        return safeReportsCount;
    }

    private bool IsSafe(Report report)
    {
        var difference = report.Levels.Zip(report.Levels.Skip(1))
            .Select(u => u.Second - u.First);
        var isSafe = difference.All(u => u < 0 && Math.Abs(u) is >= _minDifference and <= _maxDifference)
            || difference.All(u => u > 0 && Math.Abs(u) is >= _minDifference and <= _maxDifference);
        return isSafe;
    }

    private static List<Report> InitialPopulateReports(IEnumerable<string>? overrideValues = null)
    {
        var lines = overrideValues ?? File.ReadLines("Day2/input.txt");

        var reports = new List<Report>(1000);
        foreach (var line in lines)
        {
            var splitted = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var levels = splitted.Select(u => Convert.ToInt32(u)).ToList();
            reports.Add(new Report(levels));
        }

        return reports;
    }
}